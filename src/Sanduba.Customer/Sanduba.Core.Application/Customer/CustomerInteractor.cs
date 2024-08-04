using Microsoft.IdentityModel.Tokens;
using Sanduba.Core.Application.Abstraction.Customers;
using Sanduba.Core.Application.Abstraction.Customers.Events;
using Sanduba.Core.Application.Abstraction.Customers.RequestModel;
using Sanduba.Core.Application.Abstraction.Customers.ResponseModel;
using Sanduba.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;


namespace Sanduba.Core.Application.Customer
{
    public class CustomerInteractor(
        ICustomerPersistenceGateway customerRepository,
        ICustomerNotification customerNotification
    ) : ICustomerInteractor
    {
        private readonly ICustomerPersistenceGateway _customerRepository = customerRepository;
        private readonly ICustomerNotification _customerNotification = customerNotification;

        public CreateCustomerResponseModel CreateCustomer(CreateCustomerRequestModel request)
        {
            var customer = IdentifiedCustomer.CreateCustomer(
                id: Guid.NewGuid(),
                registrationNumber: request.CPF,
                name: request.Name,
                email: request.Email,
                password: request.Password);

            _customerRepository.SaveAsync((IdentifiedCustomer)customer);

            return new CreateCustomerResponseModel(customer.Id);
        }

        public GetCustomerResponseModel GetCustomer(GetCustomerRequestModel request)
        {
            var customer = _customerRepository.GetByIdentityNumber(new CPF(request.CPF));

            if (customer == null)
            {
                return new GetCustomerResponseModel
                (
                    Status: "Failure",
                    Message: "Usuário inválido",
                    Customers: null
                );
            }

            return new GetCustomerResponseModel
            (
                Status: "Success",
                Message: null,
                Customers: new[] { new CustomerResponseModel(
                    Id: customer.Id,
                    Name: customer.Name,
                    Email: customer.Email,
                    RegistryIdentification: customer?.RegistryIdentification?.ToString(),
                    Requests: customer?.Requests?.Select( req =>
                        new CustomerRequestResponseModel (
                            req.Id,
                            req.RequestedAt,
                            req.Type.ToString(),
                            req.Status.ToString(),
                            req.Comments
                        )).ToList()
                ) }
            );
        }

        public GetCustomerResponseModel GetAllCustomers()
        {
            var customers = _customerRepository.GetAllAsync();

            if (customers == null)
            {
                return new GetCustomerResponseModel
                (
                    Status: "Failure",
                    Message: "Usuário inválido",
                    Customers: null
                );
            }

            return new GetCustomerResponseModel
            (
                Status: "Success",
                Message: null,
                Customers: customers.Result.Select(customer => new CustomerResponseModel(
                    Id: customer.Id,
                    Name: customer.Name,
                    Email: customer.Email,
                    RegistryIdentification: customer?.RegistryIdentification?.ToString(),
                    Requests: customer?.Requests?.Select(req =>
                        new CustomerRequestResponseModel(
                            req.Id,
                            req.RequestedAt,
                            req.Type.ToString(),
                            req.Status.ToString(),
                            req.Comments
                        )).ToList()
                )).ToList()
            );
        }

        public LoginCustomerResponseModel LoginCustomer(LoginCustomerRequestModel requestModel)
        {
            Guid customerId;
            if (requestModel.LoginType == LoginType.Anonymous)
                customerId = Guid.NewGuid();
            else
            {
                var customer = _customerRepository.GetByLogin(requestModel.Username, requestModel.Password);

                if (customer is not null)
                    customerId = customer.Id;
                else
                    return new LoginCustomerResponseModel
                    (
                        Status: "Failure",
                        Message: "Usuário e/ou senha inválido",
                        Token: null
                    );
            }

            var jwt = GenerateJwt(customerId);

            return new LoginCustomerResponseModel
            (
                Status: "Success",
                Message: "Autenticado com sucesso!",
                Token: jwt
            );
        }

        public DeleteCustomerResponseModel DeleteCustomer(DeleteCustomerRequestModel requestModel)
        {
            var customer = _customerRepository.GetByIdAsync(requestModel.CustomerId).Result;

            if (customer is null)
            {
                return new DeleteCustomerResponseModel(null, "Failure", "Customer not found!");
            }

            var requestId = _customerRepository.RequestInactivation(
                Guid.NewGuid(),
                requestModel.CustomerId,
                requestModel.Name,
                requestModel.Address,
                requestModel.PhoneNumber
            );

            _customerNotification.InactivationRequested(new InactivationRequestedEvent(
                requestId, requestModel.CustomerId)
            );

            return new DeleteCustomerResponseModel(requestId, "Successful", "Customer deletion requested!");
        }

        private string GenerateJwt(Guid userId)
        {
            var claims = new Claim[]
            {
                new Claim("Sub", userId.ToString())
            };

            var jwtSecretKey = Environment.GetEnvironmentVariable("AUTH_SECRET_KEY") ?? string.Empty;
            var jwtIssuer = Environment.GetEnvironmentVariable("AUTH_ISSUER") ?? string.Empty;
            var jwtAudience = Environment.GetEnvironmentVariable("AUTH_AUDIENCE") ?? string.Empty;

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)),
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                jwtIssuer,
                jwtAudience,
                claims,
                null,
                DateTime.UtcNow.AddHours(1),
                signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
