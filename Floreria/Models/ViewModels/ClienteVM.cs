using Microsoft.AspNetCore.Mvc.Rendering;

namespace Floreria.Models.ViewModels
{
    public class ClienteVM
    {
        public Cliente oCliente { get; set; }
        public List<SelectListItem> oListaClientes { get; set; }

        public ClienteVM()
        {
            oCliente = new Cliente();
            oListaClientes = new List<SelectListItem>();
        }

    }
}
