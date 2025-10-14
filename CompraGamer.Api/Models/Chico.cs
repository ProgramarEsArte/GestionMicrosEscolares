using System;

namespace CompraGamer.Api.Models
{
    // Mapea la tabla `chico` (PK: dni, FK: micro_patente)
    public class Chico
    {
        // char(8) PK
        [System.ComponentModel.DataAnnotations.Key]
        [System.ComponentModel.DataAnnotations.StringLength(8)]
        public string Dni { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string? Nombre { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string? Apellido { get; set; }

        // FK a microescolar.patente (char(7))
        [System.ComponentModel.DataAnnotations.StringLength(7)]
        public string? MicroPatente { get; set; }
    }
}
