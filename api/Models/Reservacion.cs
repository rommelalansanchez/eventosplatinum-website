namespace Eventos.Platinum.Azure.Functions.Models
{
    public class Reservacion
    {
        public int ReservacionId { get; set; }
        public int? SalaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public DateTime FechaEvento { get; set; } = DateTime.Now;
        public int NumeroPersonas { get; set; }
        public string EventoTipo { get; set; } = string.Empty;
        public string Horario { get; set; } = string.Empty;
    }
}
