using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;
using System.Text.Json;
using Sanduba.Adapter.Controller;
using Sanduba.Core.Application;
using Sanduba.Infrastructure.Persistence.SqlServer;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((context, services) =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddApplication(context.Configuration);
        services.AddApiAdapter(context.Configuration);
        services.AddSqlServerInfrastructure(context.Configuration);
        services.Configure<JsonSerializerOptions>(options =>
        {
            options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
            options.PropertyNameCaseInsensitive = true;
            options.WriteIndented = true;
            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        });
    })
    .Build();

host.Run();
