using Floreria.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace Floreria.Controllers
{
    public class ContactController : Controller
    {
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        
        //Metodo para enviar el correo, con try catch permite controlar si hexiste algun error

        [HttpPost]
        public IActionResult Send(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Configurar los detalles del correo electrónico
                    var correo = new MailMessage();
                    correo.From = new MailAddress("FloreriaFloretti@outlook.com");
                    correo.To.Add("FloreriaFloretti@outlook.com");
                    correo.Subject = "Formulario de contacto";
                    correo.Body = $"{model.Nombre}\n{model.CorreoElectronico}\n{model.Mensaje}";

                    // Enviar el correo electrónico utilizando el servidor SMTP de Outlook.com
                    using (var smtp = new SmtpClient("smtp-mail.outlook.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("FloreriaFloretti@outlook.com", "Floretti");
                        smtp.EnableSsl = true;

                        smtp.Send(correo);
                    }
                    //Enviar un mensaje de Correo enviado exitosamente
                    TempData["MessageSend"] = "El mensaje fue enviado correctamente";
                    return RedirectToAction("Contact");
                }
                catch (Exception ex)
                {
                    // Pasar el mensaje de error a la vista Error.cshtml
                    ViewData["ErrorDetails"] = ex.Message;
                    return View("ErrorMail");
                }
            }
			return View("Contact");
		}
    }
}
