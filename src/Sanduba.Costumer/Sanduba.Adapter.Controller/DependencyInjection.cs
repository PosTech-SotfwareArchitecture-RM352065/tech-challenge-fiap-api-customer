using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sanduba.Adapter.Controller.Costumers;
using Sanduba.Core.Application.Abstraction.Costumers;

namespace Sanduba.Adapter.Controller
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers the necessary services with the DI framework.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The same service collection.</returns>
        public static IServiceCollection AddApiAdapter(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<CostumerController<IActionResult>, CostumerApiController>();
            services.AddTransient<CostumerPresenter<IActionResult>, CostumerApiPresenter>();

            return services;
        }
    }
}
