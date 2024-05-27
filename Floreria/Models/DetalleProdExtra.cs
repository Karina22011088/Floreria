using System;
using System.Collections.Generic;

namespace Floreria.Models
{
    public partial class DetalleProdExtra
    {
        public int? IdPedido { get; set; }
        public int? IdProducto { get; set; }
        public int? Cantidad { get; set; }
        public decimal? MontoTotal { get; set; }

        public virtual Pedido? IdPedidoNavigation { get; set; }
        public virtual ProductoExtra? IdProductoNavigation { get; set; }
    }
}
