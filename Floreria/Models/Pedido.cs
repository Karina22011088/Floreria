using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Floreria.Models
{
    public partial class Pedido
    {
        public int IdPedido { get; set; }
        public int? IdCliente { get; set; }
        public int? IdEntrega { get; set; }
        public int? IdPago { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaPedido { get; set; }
        public string? EstadoPedido { get; set; }

        public virtual Cliente? oCliente { get; set; }
        public virtual Entrega? oEntrega { get; set; }
        public virtual Pago? oPago { get; set; }


    }
}
