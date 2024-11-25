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
        services.AddHttpClient("EventosPlatinumApi", (provider, client)  =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var apiUrl = configuration.GetSection("AdminAppApiUrl")?.Value?.ToString() ?? "";
            client.BaseAddress = new Uri(apiUrl);
        });
    })
    .Build();

host.Run();
