using RammarTech.Tools;
using RammarTech.Tools.SharedDtos;
using System.Net.Http.Json;

namespace EventosPlatinumWebsiteApi;

public static class Common
{
    public static async Task<string> AuthenticateAndGetToken(HttpClient client)
    {
        var response = await client.PostAsJsonAsync("auth/login", new { NombreUsuario = "systemuser", Password = "3vENt4PP2024!" });
        var responseContent = response.Content;
        var serviceResponse = await responseContent.ReadFromJsonAsync<ServiceResponse<string>>();
        var authToken = serviceResponse?.ProcessResponse(response.StatusCode, await responseContent.ReadAsStringAsync());
        return authToken ?? "";
    }

    public static string PlantillaReservacion(string nombre, string correo, string telefono, string tipoEvento, int numeroPersonas, string horario)
    {
        string readText = File.ReadAllText("SolicitudReservacion.html")
            .Replace("{{nombre}}", nombre)
            .Replace("{{correo}}", correo)
            .Replace("{{telefono}}", telefono)
            .Replace("{{tipoEvento}}", tipoEvento)
            .Replace("{{numeroPersonas}}", numeroPersonas.ToString())
            .Replace("{{horario}}", horario);
        return readText;
    }
}
