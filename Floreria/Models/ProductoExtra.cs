using System;
using System.Collections.Generic;

namespace Floreria.Models
{
    public partial class ProductoExtra
    {
        public int IdProducto { get; set; }
        public string? NombreProduct { get; set; }
        public string? DescripcionProd { get; set; }
        public decimal? PrecioProd { get; set; }
    }
}
