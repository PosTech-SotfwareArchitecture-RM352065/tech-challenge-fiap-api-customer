using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Text.Json;
using Sanduba.Cloud.Funtion.Login.Models;
using Sanduba.Core.Application.Abstraction.Customers.RequestModel;
using Sanduba.Core.Application.Abstraction.Customers;

namespace Sanduba.Cloud.Funtion.Login
{
    public class LoginUserEndpoint(ILogger<LoginUserEndpoint> logger, CustomerController<IActionResult> customerController)
    {
        private readonly ILogger<LoginUserEndpoint> _logger = logger;
        private readonly CustomerController<IActionResult> _customerController = customerController;

        [Function("LoginUser")]
        public async Task<IActionResult> Login([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            LoginCustomerRequestModel request;

            try
            {

                if (string.IsNullOrEmpty(requestBody))
                    request = new LoginCustomerRequestModel(LoginType.Anonymous);
                else
                {
                    try
                    {
                        var identifiedUser = JsonSerializer.Deserialize<IdentifiedUserRequest>(requestBody);

                        if (identifiedUser == null
                            || string.IsNullOrEmpty(identifiedUser.Username)
                            || string.IsNullOrEmpty(identifiedUser.Password))
                        {
                            _logger.LogError($"Invalid request: {requestBody}");
                            return new BadRequestObjectResult("Invalid username/password!");
                        }

                        request = new LoginCustomerRequestModel(LoginType.Identified, identifiedUser.Username, identifiedUser.Password);
                    }
                    catch
                    {
                        _logger.LogError($"Invalid request: {requestBody}");
                        return new BadRequestObjectResult("Invalid username/password!");
                    }
                }

                return _customerController.LoginCustomer(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
