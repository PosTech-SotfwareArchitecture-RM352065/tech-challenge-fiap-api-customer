using Microsoft.IdentityModel.Tokens;
using Sanduba.Core.Application.Abstraction.Costumers;
using Sanduba.Core.Application.Abstraction.Costumers.RequestModel;
using Sanduba.Core.Application.Abstraction.Costumers.ResponseModel;
using Sanduba.Core.Domain.Costumers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;


namespace Sanduba.Core.Application.Costumer
{
    public class CostumerInteractor(ICostumerPersistenceGateway costumerRepository) : ICostumerInteractor
    {
        private readonly ICostumerPersistenceGateway _costumerRepository = costumerRepository;

        public CreateCostumerResponseModel CreateCostumer(CreateCostumerRequestModel request)
        {
            var costumer = IdentifiedCostumer.CreateCostumer(
                id: Guid.NewGuid(),
                registrationNumber: request.CPF,
                name: request.Name,
                email: request.Email,
                password: request.Password);

            _costumerRepository.SaveAsync(costumer);

            return new CreateCostumerResponseModel(costumer.Id);
        }

        public GetCostumerResponseModel GetCostumer(GetCostumerRequestModel request)
        {
            var costumer = _costumerRepository.GetByIdentityNumber(new CPF(request.CPF));

            return new GetCostumerResponseModel
            (
                Id: costumer.Id,
                Name: costumer.Name,
                Email: costumer.Email,
                RegistryIdentification: costumer?.RegistryIdentification?.ToString()
            );
        }

        public IEnumerable<GetCostumerResponseModel> GetAllCostumer()
        {
            var costumers = _costumerRepository.GetAllAsync();

            return costumers.Result.Select(costumer => new GetCostumerResponseModel(
                Id: costumer.Id,
                Name: costumer.Name,
                Email: costumer.Email,
                RegistryIdentification: costumer?.RegistryIdentification?.ToString()
            )).ToList();
        }

        public LoginCostumerResponseModel LoginCostumer(LoginCostumerRequestModel requestModel)
        {
            Guid costumerId;
            if (requestModel.LoginType == LoginType.Anonymous)
                costumerId = Guid.NewGuid();
            else
            {
                var costumer = _costumerRepository.GetByLogin(requestModel.Username, requestModel.Password);

                if (costumer is not null)
                    costumerId = costumer.Id;
                else
                    return new LoginCostumerResponseModel
                    (
                        Status: "Failure",
                        Message: "Usuário e/ou senha inválido",
                        Token: null
                    );
            }

            var jwt = GenerateJwt(costumerId);

            return new LoginCostumerResponseModel
            (
                Status: "Success",
                Message: "Autenticado com sucesso!",
                Token: jwt
            );
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
