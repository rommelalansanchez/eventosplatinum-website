using Eventos.Platinum.Azure.Functions.Models;
using EventosPlatinumWebsiteApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RammarTech.Tools;

namespace Eventos.Platinum.Azure.Functions
{
    public class Reservar
    {
        private readonly ILogger<Reservar> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public Reservar(ILogger<Reservar> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [Function("Reservar")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var model = JsonConvert.DeserializeObject<Reservacion>(body);

            // var client = _httpClientFactory.CreateClient("EventosPlatinumApi");
            // var authToken = await Common.AuthenticateAndGetToken(client);

            var email = new Email
            {
                Destinatarios = new() { _configuration.GetSection("DestinatarioNotificaciones")?.Value?.ToString() ?? "" },
                Asunto = "Nueva Solicitud de Reservación",
                Mensaje = Common.PlantillaReservacion(model.Nombre, model.Correo, model.Telefono, model.EventoTipo, model.NumeroPersonas, model.Horario)
            };
                
            EmailSender emailSender = new EmailSender();
            emailSender.SetSmtpServer(Environment.GetEnvironmentVariable("EmailSmtpServer")!);
            emailSender.SetPort(Convert.ToInt16(Environment.GetEnvironmentVariable("EmailPort")!));
            emailSender.SetAccountSending(Environment.GetEnvironmentVariable("EmailAccount")!);
            emailSender.SetPassword(Environment.GetEnvironmentVariable("EmailPassword")!);

            emailSender.Send(email.Destinatarios, email.Asunto, email.Mensaje, "Notificaciones Eventos Platinum");
            // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken?.Replace("\"", ""));
            // var response = await client.PostAsJsonAsync($"email/enviar", email) ?? throw new Exception("Response is empty.");
            // var responseContent = response.Content;
            // var content2 = await responseContent.ReadFromJsonAsync<ServiceResponse<List<object>>>();
            // var salasDisponibles = content2?.ProcessResponse(response.StatusCode, await response.Content.ReadAsStringAsync());

            return new OkObjectResult(model);
        }
    }
}
