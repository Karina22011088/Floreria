using Floreria.Models.ViewModels;
using Floreria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace Floreria.Controllers
{
    public class GestionPedidoController : Controller
    {
        private readonly FloreriaContext _DBContext;
        public GestionPedidoController(FloreriaContext context)
        {
            _DBContext = context;
        }

        public IActionResult Pedido()
        {
            List<Pedido> listaP = _DBContext.Pedidos.Include(c => c.oCliente)
                                                    .Include(e => e.oEntrega)
                                                    .Include(p => p.oPago)
                                                    .ToList();

            return View(listaP);
        }



        [HttpGet]
        public IActionResult DetallePedido(int idPedido)
        {
            PedidoVM oPedidoVM = new PedidoVM
            {
                oPedido = idPedido != 0 ? _DBContext.Pedidos
                    .Include(p => p.oEntrega)
                    .Include(p => p.oPago)
                    .FirstOrDefault(p => p.IdPedido == idPedido) : new Pedido(),
                oListaClientes = _DBContext.Clientes.Select(c => new SelectListItem
                {
                    Text = c.NombreCliente,
                    Value = c.IdCliente.ToString()
                }).ToList()
            };

            return View(oPedidoVM);
        }

        [HttpPost]
        public IActionResult DetallePedido(PedidoVM oPedidoVM)
        {
            if (ModelState.IsValid)
            {
                if (oPedidoVM.oPedido.IdPedido == 0)
                {
                    // Crear nuevo pedido

                    // Obtener el último ID de pedido registrado
                    int ultimoIdPedido = _DBContext.Pedidos.Max(p => p.IdPedido);

                    // Incrementar el ID para el nuevo pedido
                    int nuevoIdPedido = ultimoIdPedido + 1;

                    // Asignar el mismo ID a la entidad Entrega y Pago
                    oPedidoVM.oPedido.IdPedido = nuevoIdPedido;
                    oPedidoVM.oPedido.oEntrega.IdEntrega = nuevoIdPedido;
                    oPedidoVM.oPedido.oPago.IdPago = nuevoIdPedido;

                    _DBContext.Pedidos.Add(oPedidoVM.oPedido);
                    _DBContext.SaveChanges();

                    return RedirectToAction("Pedido");
                }
                else
                {
                    // Actualizar pedido existente
                    var pedidoExistente = _DBContext.Pedidos
                        .Include(p => p.oEntrega)
                        .Include(p => p.oPago)
                        .FirstOrDefault(p => p.IdPedido == oPedidoVM.oPedido.IdPedido);

                    if (pedidoExistente == null)
                    {
                        // Pedido no encontrado, puedes manejar el escenario aquí
                        return RedirectToAction("Pedido");
                    }

                    pedidoExistente.IdCliente = oPedidoVM.oPedido.IdCliente;
                    pedidoExistente.oEntrega.DireccionEntrega = oPedidoVM.oPedido.oEntrega.DireccionEntrega;
                    pedidoExistente.oEntrega.ReferenciaDomicilio = oPedidoVM.oPedido.oEntrega.ReferenciaDomicilio;
                    pedidoExistente.oEntrega.FechaHoraEntrega = oPedidoVM.oPedido.oEntrega.FechaHoraEntrega;
                    pedidoExistente.oPago.TotalPedido = oPedidoVM.oPedido.oPago.TotalPedido;
                    pedidoExistente.FechaPedido = oPedidoVM.oPedido.FechaPedido;
                    pedidoExistente.EstadoPedido = oPedidoVM.oPedido.EstadoPedido;

                    _DBContext.SaveChanges();

                    return RedirectToAction("Pedido");
                }
            }

            // Si llegamos a este punto, significa que hubo un error de validación
            // Puedes manejar el escenario según tus necesidades, como mostrar un mensaje de error o volver a cargar la vista con los errores de validación.

            oPedidoVM.oListaClientes = _DBContext.Clientes.Select(c => new SelectListItem
            {
                Text = c.NombreCliente,
                Value = c.IdCliente.ToString()
            }).ToList();

            return View(oPedidoVM);
        }

        [HttpGet]
        public IActionResult EliminarPedido(int idPedido)
        {
            Pedido oPedido = _DBContext.Pedidos.Include(p => p.oCliente)
                                               .Include(p => p.oEntrega)
                                               .Include(p => p.oPago)
                                               .FirstOrDefault(p => p.IdPedido == idPedido);


            // Crear un objeto PedidoVM y asignar los valores correspondientes
            PedidoVM pedidoVM = new PedidoVM
            {
                oPedido = oPedido,
                // Asignar otras propiedades necesarias del modelo PedidoVM
            };

            return View(pedidoVM);
        }

        [HttpPost]
        public IActionResult EliminarPedido(Pedido oPedido)
        {
            Pedido pedido = _DBContext.Pedidos
                .Include(p => p.oEntrega)
                .Include(p => p.oPago)
                .FirstOrDefault(p => p.IdPedido == oPedido.IdPedido);

            if (pedido == null)
            {
                // Pedido no encontrado, puedes manejar el escenario aquí
                return RedirectToAction("Pedido", "GestionPedido");
            }

            // Eliminar las entidades de entrega y pago asociadas al pedido
            _DBContext.Remove(pedido.oEntrega);
            _DBContext.Remove(pedido.oPago);

            // Eliminar el pedido
            _DBContext.Pedidos.Remove(pedido);
            _DBContext.SaveChanges();

            return RedirectToAction("Pedido", "GestionPedido");
        }


        ///////////////////////////////////////////////////////////////////////////////////
        ///

        [HttpGet]
        public IActionResult DetalleCliente(int idCliente)
        {
            ClienteVM oClienteVM = new ClienteVM();

            // Obtén el último ID de paciente en la base de datos y genera el nuevo ID sumándole 1
            int nuevoIdCliente = _DBContext.Clientes.Max(p => p.IdCliente) + 1;

            oClienteVM.oCliente = new Cliente()
            {
                IdCliente = nuevoIdCliente
            };

            return View(oClienteVM);
        }


        [HttpPost]
        public IActionResult DetalleCliente(ClienteVM oClienteVM)
        {
            if (ModelState.IsValid)
            {
                // Agrega el nuevo cliente al contexto de la base de datos
                _DBContext.Clientes.Add(oClienteVM.oCliente);
                _DBContext.SaveChanges();

                return RedirectToAction("Pedido");
            }

            return View(oClienteVM);
        }


        public List<Pedido> ListaPedidos()
        {
            List<Pedido> lista = _DBContext.Pedidos.Include(c => c.oCliente)
                                                    .Include(e => e.oEntrega)
                                                    .Include(p => p.oPago)
                                                    .ToList();
            return lista;
        }


        public IActionResult GenerarPDFPedidos()
        {
            List<Pedido> pedidos = ListaPedidos();


            var document = new Document(PageSize.A4, 50, 50, 25, 25);
            using (var memoryStream = new MemoryStream())
            {
                var writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                var titulo = new Paragraph("Lista de pedidos", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD));
                titulo.Alignment = Element.ALIGN_CENTER;
                document.Add(titulo);

                var espacio = new Paragraph(" "); // Párrafo vacío para agregar un salto de línea
                document.Add(espacio);

                // Crear la tabla con anchos de columna personalizados
                var tabla = new PdfPTable(new float[] { 1f, 1.5f, 1.5f, 1.5f, 1.5f, 1.2f, 1.5f, 1.2f });
                tabla.DefaultCell.Padding = 5;
                tabla.WidthPercentage = 100;
                tabla.HorizontalAlignment = Element.ALIGN_LEFT;

                // Fuente y tamaño de la fuente
                var font = new iTextSharp.text.Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL);

                tabla.AddCell(new Phrase("No. Pedido", font));
                tabla.AddCell(new Phrase("Nombre o alias del cliente", font));
                tabla.AddCell(new Phrase("Dirección de entrega", font));
                tabla.AddCell(new Phrase("Referencia de la dirección", font));
                tabla.AddCell(new Phrase("Fecha y hora de entrega", font));
                tabla.AddCell(new Phrase("Total de pedido", font));
                tabla.AddCell(new Phrase("Fecha del pedido", font));
                tabla.AddCell(new Phrase("Estado del pedido", font));

                foreach (var item in pedidos)
                {
                    tabla.AddCell(new Phrase(item.IdPedido.ToString(), font));
                    tabla.AddCell(new Phrase(item.oCliente?.NombreCliente ?? "", font));
                    tabla.AddCell(new Phrase(item.oEntrega?.DireccionEntrega ?? "", font));
                    tabla.AddCell(new Phrase(item.oEntrega?.ReferenciaDomicilio ?? "", font));
                    if (item.oEntrega?.FechaHoraEntrega != null)
                    {
                        string fechaHoraEntrega = item.oEntrega.FechaHoraEntrega.Value.ToString("dd/MM/yyyy HH:mm:ss");
                        tabla.AddCell(new Phrase(fechaHoraEntrega, font));
                    }
                    else
                    {
                        tabla.AddCell(new Phrase("", font));
                    }
                    if (item.oPago?.TotalPedido != null)
                    {
                        string totalFormateado = item.oPago.TotalPedido.Value.ToString("C");
                        tabla.AddCell(new Phrase(totalFormateado, font));
                    }
                    else
                    {
                        tabla.AddCell(new Phrase("", font));
                    }
                    
                    string fechaFormateada = string.Format("{0:dd/MM/yyyy}", item.FechaPedido);
                    tabla.AddCell(new Phrase(fechaFormateada, font));

                    tabla.AddCell(new Phrase(item.EstadoPedido, font));
                }

                document.Add(tabla);

                document.Close();

                var fileContents = memoryStream.ToArray();
                return File(fileContents, "application/pdf", "Pedidos.pdf");
            }
        }


    }
}
