using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Floreria.Models
{
    public partial class Entrega
    {
        public Entrega()
        {
            Pedidos = new HashSet<Pedido>();
        }


        public int IdEntrega { get; set; }
        public string? DireccionEntrega { get; set; }
        public string? ReferenciaDomicilio { get; set; }
        public string? NombreRecibe { get; set; }
        public string? EstadoEntrega { get; set; }
        public DateTime? FechaHoraEntrega { get; set; }


        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
