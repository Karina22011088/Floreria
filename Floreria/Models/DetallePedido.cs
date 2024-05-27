using System;
using System.Collections.Generic;

namespace Floreria.Models
{
    public partial class DetallePedido
    {
        public int? IdPedido { get; set; }
        public int? IdModelo { get; set; }
        public int? Cantidad { get; set; }
        public decimal? MontoTotal { get; set; }

        public virtual ArregloFloral? IdModeloNavigation { get; set; }
        public virtual Pedido? IdPedidoNavigation { get; set; }
    }
}
