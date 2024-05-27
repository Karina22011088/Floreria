using Microsoft.AspNetCore.Mvc.Rendering;

namespace Floreria.Models.ViewModels
{
    public class PedidoVM
    {
        public Pedido oPedido { get; set; }
        public List<SelectListItem> oListaClientes { get; set; }

        public List<SelectListItem> oListaEntrega { get; set; }
        public List<SelectListItem> oListaPagos { get; set; }
        public List<SelectListItem> oListaArregloFloral { get; set; }
        public List<SelectListItem> oListaProductoExtra { get; set; }

        public PedidoVM()
        {
            oPedido = new Pedido();
            oListaClientes = new List<SelectListItem>();
            oListaEntrega = new List<SelectListItem>();
            oListaPagos = new List<SelectListItem>();
            oListaArregloFloral = new List<SelectListItem>();
            oListaProductoExtra = new List<SelectListItem>();
        }


    }
}
