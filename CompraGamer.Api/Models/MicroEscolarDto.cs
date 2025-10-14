using System.Collections.Generic;

namespace CompraGamer.Api.Models
{
    public class MicroEscolarDto
    {
        public string Patente { get; set; } = string.Empty;
        public string? ChoferDni { get; set; }
        public int ChicosCount { get; set; }
    }
}