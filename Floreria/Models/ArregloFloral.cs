using System;
using System.Collections.Generic;

namespace Floreria.Models
{
    public partial class ArregloFloral
    {
        public int IdModelo { get; set; }
        public string? NombreModelo { get; set; }
        public string? DescripcionModelo { get; set; }
        public decimal? PrecioArreglo { get; set; }
    }
}
