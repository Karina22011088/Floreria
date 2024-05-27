using System;
using System.Collections.Generic;

namespace Floreria.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int IdCliente { get; set; }
        public string? NombreCliente { get; set; }
        public long? Telefono { get; set; }
        public string? MedioContacto { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
