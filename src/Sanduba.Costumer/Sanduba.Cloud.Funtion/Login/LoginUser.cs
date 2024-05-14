using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Text.Json;
using Sanduba.Cloud.Funtion.Login.Models;
using Sanduba.Core.Application.Abstraction.Costumers.RequestModel;
using Sanduba.Core.Application.Abstraction.Costumers;

namespace Sanduba.Cloud.Funtion.Login
{
    public class LoginUser(ILogger<LoginUser> logger, CostumerController<IActionResult> costumerController)
    {
        private readonly ILogger<LoginUser> _logger = logger;
        private readonly CostumerController<IActionResult> _costumerController = costumerController;

        [Function("LoginUser")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            LoginCostumerRequestModel request;

            try
            {

                if (string.IsNullOrEmpty(requestBody))
                    request = new LoginCostumerRequestModel(LoginType.Anonymous);
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

                        request = new LoginCostumerRequestModel(LoginType.Identified, identifiedUser.Username, identifiedUser.Password);
                    }
                    catch
                    {
                        _logger.LogError($"Invalid request: {requestBody}");
                        return new BadRequestObjectResult("Invalid username/password!");
                    }
                }

                return _costumerController.LoginCostumer(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
