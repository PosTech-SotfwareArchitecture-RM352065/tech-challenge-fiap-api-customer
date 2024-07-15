using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Text.Json;
using Sanduba.Cloud.Funtion.Customer.Models;
using Sanduba.Core.Application.Abstraction.Customers.RequestModel;
using Sanduba.Core.Application.Abstraction.Customers;



namespace Sanduba.Cloud.Funtion.Customer
{
    public class CustomerEndPoint(ILogger<CustomerEndPoint> logger, CustomerController<IActionResult> customerController)
    {
        private readonly ILogger<CustomerEndPoint> _logger = logger;
        private readonly CustomerController<IActionResult> _customerController = customerController;

        [Function("CreateCustomer")]
        public async Task<IActionResult> Create([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            if (string.IsNullOrEmpty(requestBody)) return new BadRequestObjectResult("Invalid customer!");

            var createCustomerRequest = JsonSerializer.Deserialize<CreateCustomerRequest>(requestBody);
            if (createCustomerRequest is null) return new BadRequestObjectResult("Invalid customer!");

            CreateCustomerRequestModel request = new CreateCustomerRequestModel
                (
                    createCustomerRequest.CPF,
                    createCustomerRequest.Name,
                    createCustomerRequest.Email,
                    createCustomerRequest.Password
                );

            try
            {
                return _customerController.CreateCustomer(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [Function("DeleteCustomer")]
        public async Task<IActionResult> Delete([HttpTrigger(AuthorizationLevel.Anonymous, "delete")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            if (string.IsNullOrEmpty(requestBody)) return new BadRequestObjectResult("Invalid customer!");

            var createCustomerRequest = JsonSerializer.Deserialize<CreateCustomerRequest>(requestBody);
            if (createCustomerRequest is null) return new BadRequestObjectResult("Invalid customer!");

            CreateCustomerRequestModel request = new CreateCustomerRequestModel
                (
                    createCustomerRequest.CPF,
                    createCustomerRequest.Name,
                    createCustomerRequest.Email,
                    createCustomerRequest.Password
                );

            try
            {
                return _customerController.CreateCustomer(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [Function("GetCustomer")]
        public async Task<IActionResult> Get([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            if (string.IsNullOrEmpty(requestBody)) return new BadRequestObjectResult("Invalid customer!");

            var createCustomerRequest = JsonSerializer.Deserialize<CreateCustomerRequest>(requestBody);
            if (createCustomerRequest is null) return new BadRequestObjectResult("Invalid customer!");

            CreateCustomerRequestModel request = new CreateCustomerRequestModel
                (
                    createCustomerRequest.CPF,
                    createCustomerRequest.Name,
                    createCustomerRequest.Email,
                    createCustomerRequest.Password
                );

            try
            {
                return _customerController.CreateCustomer(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
