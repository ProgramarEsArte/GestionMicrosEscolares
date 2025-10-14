using System;

namespace CompraGamer.Api.Models
{
    // Mapea la tabla `microescolar` (PK: patente, FK: chofer_dni)
    public class MicroEscolar
    {
        [System.ComponentModel.DataAnnotations.Key]
        [System.ComponentModel.DataAnnotations.StringLength(7)]
        public string Patente { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.StringLength(8)]
        public string? ChoferDni { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int CantidadChicos { get; set; }
    }
}
