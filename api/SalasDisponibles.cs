using EventosPlatinumWebsiteApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using RammarTech.Tools;
using RammarTech.Tools.SharedDtos;
using System.Net.Http.Json;

namespace Eventos.Platinum.Azure.Functions
{
    public class SalasDisponibles
    {
        private readonly ILogger<SalasDisponibles> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public SalasDisponibles(ILogger<SalasDisponibles> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [Function("SalasDisponibles")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("EventosPlatinumApi");
                var authToken = await Common.AuthenticateAndGetToken(client);

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken?.Replace("\"", ""));
                var response = client.GetAsync($"sala").Result ?? throw new Exception("Response is empty.");
                var responseContent = response.Content;
                var content2 = await responseContent.ReadFromJsonAsync<ServiceResponse<List<object>>>();
                var salasDisponibles = content2?.ProcessResponse(response.StatusCode, await response.Content.ReadAsStringAsync());

                return new OkObjectResult(salasDisponibles);
            }
            catch (CustomException cex)
            {
                Console.WriteLine($"Ha ocurrido un error: {cex.Message}");
                throw;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
