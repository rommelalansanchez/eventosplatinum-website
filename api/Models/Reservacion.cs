namespace Eventos.Platinum.Azure.Functions.Models
{
    public class Reservacion
    {
        public int? ReservacionId { get; set; } = default;
        public int? SalaId { get; set; } = default;
        public string? Nombre { get; set; } = default!;
        public string? Correo { get; set; } = default!;
        public string? Telefono { get; set; } = default!;
        public DateTime? FechaEvento { get; set; } = default;
        public int? NumeroPersonas { get; set; } = default;
        public string? EventoTipo { get; set; } = default!;
        public string? Horario { get; set; } = default!;
    }
}
