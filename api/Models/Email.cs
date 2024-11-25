namespace Eventos.Platinum.Azure.Functions.Models;

public class Email
{
    public List<string> Destinatarios { get; set; } = new List<string>();
    public string Asunto { get; set; } = string.Empty; 
    public string Mensaje { get; set; } = string.Empty;
}
