using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;
using System.Text.Json;
using Sanduba.Core.Application;
using Sanduba.Infrastructure.Persistence.SqlServer;
using Sanduba.Infrastructure.Broker.ServiceBus.Configurations;
using System;
using Sanduba.Adapter.Mvc;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((context, services) =>
    {
        var entryAssemblies = AppDomain.CurrentDomain.GetAssemblies();

        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddApplication(context.Configuration);
        services.AddServiceBusInfrastructure(context.Configuration); 
        services.AddMvcAdapter(context.Configuration);
        services.AddSqlServerInfrastructure(context.Configuration);
        services.Configure<JsonSerializerOptions>(options =>
        {
            options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
            options.PropertyNameCaseInsensitive = true;
            options.WriteIndented = true;
            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        });
        services.AddAutoMapper(entryAssemblies);
    })
    .Build();

host.Run();
