using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Text.Json;
using Sanduba.Cloud.Funtion.Costumer.Models;
using Sanduba.Core.Application.Abstraction.Costumers.RequestModel;
using Sanduba.Core.Application.Abstraction.Costumers;



namespace Sanduba.Cloud.Funtion.Costumer
{
    public class CreateCostumer(ILogger<CreateCostumer> logger, CostumerController<IActionResult> costumerController)
    {
        private readonly ILogger<CreateCostumer> _logger = logger;
        private readonly CostumerController<IActionResult> _costumerController = costumerController;

        [Function("CreateCostumer")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            if (string.IsNullOrEmpty(requestBody)) return new BadRequestObjectResult("Invalid costumer!");

            var createCostumerRequest = JsonSerializer.Deserialize<CreateCostumerRequest>(requestBody);
            if (createCostumerRequest is null) return new BadRequestObjectResult("Invalid costumer!");

            CreateCostumerRequestModel request = new CreateCostumerRequestModel
                (
                    createCostumerRequest.CPF,
                    createCostumerRequest.Name,
                    createCostumerRequest.Email,
                    createCostumerRequest.Password
                );

            try
            {
                return _costumerController.CreateCostumer(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
