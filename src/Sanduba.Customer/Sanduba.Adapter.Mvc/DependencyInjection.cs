using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sanduba.Adapter.Mvc.Customers;
using Sanduba.Core.Application.Abstraction.Customers;

namespace Sanduba.Adapter.Mvc
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers the necessary services with the DI framework.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The same service collection.</returns>
        public static IServiceCollection AddMvcAdapter(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<CustomerController<IActionResult>, CustomerApiController>();
            services.AddTransient<CustomerPresenter<IActionResult>, CustomerApiPresenter>();

            return services;
        }
    }
}
