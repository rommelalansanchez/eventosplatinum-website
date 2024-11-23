using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
     .ConfigureAppConfiguration((hostContext, configBuilder) =>
     {
         configBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

         // Add Azure Key Vault as a configuration provider
         configBuilder.Build();
     })
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddHttpClient("EventosPlatinumApi", client =>
        {
            client.BaseAddress = new Uri("https://localhost:44345/api/");
        });
    })
    .Build();

host.Run();
