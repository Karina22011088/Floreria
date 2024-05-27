using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Floreria.Models
{
    public partial class Pago
    {
        public Pago()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int IdPago { get; set; }
        public string? TipoPago { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal? TotalPedido { get; set; }
        public string? EstadoPago { get; set; }


        
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
