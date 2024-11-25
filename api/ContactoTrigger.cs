using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace EventosPlatinumWebsiteApi.Function;

public class ContactoTrigger
{
    private readonly ILogger<ContactoTrigger> _logger;

    public ContactoTrigger(ILogger<ContactoTrigger> logger)
    {
        _logger = logger;
    }

    [Function("ContactoTrigger")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}
