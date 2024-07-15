using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Sanduba.Core.Application.Abstraction.Customers.RequestModel;
using Sanduba.Core.Application.Abstraction.Customers;
using System.Threading.Tasks;
using System;

namespace Sanduba.Cloud.Funtion.Admin
{
    public class AdminEndpoint(ILogger<AdminEndpoint> logger, CustomerController<IActionResult> customerController)
    {
        private readonly ILogger<AdminEndpoint> _logger = logger;
        private readonly CustomerController<IActionResult> _customerController = customerController;

        [Function("GetCustomerByIdentifier")]
        public async Task<IActionResult> GetCustomerByIdentifier([HttpTrigger(AuthorizationLevel.Admin, "get")] HttpRequest req)
        {
            try
            {
                if (req.Query.Count == 0)
                {
                    return new BadRequestObjectResult("Invalid request!");
                }
                else
                {
                    var identifier = req.Query["cpf"].ToString();
                    if (string.IsNullOrEmpty(identifier)) return new BadRequestObjectResult("Invalid customer!");

                    GetCustomerRequestModel request = new GetCustomerRequestModel(identifier);

                    return _customerController.GetCustomer(request);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [Function("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers([HttpTrigger(AuthorizationLevel.Admin, "get")] HttpRequest req)
        {
            try
            {
                if (req.Query.Count == 0)
                {
                    return _customerController.GetAllCustomers();
                }
                else
                {
                    return new BadRequestObjectResult("Invalid request!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
