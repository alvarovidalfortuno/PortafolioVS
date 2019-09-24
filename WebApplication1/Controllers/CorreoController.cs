using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class CorreoController : Controller
    {
        // GET: Correo
        public static async Task EnviarCorreoAdjunto(MailMessage message)
        {
            var usuarioSmtp = "portafoliosistemasduoc@gmail.com";
            var claveSmtp = "12345678n.";
            

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = usuarioSmtp,  // replace with valid value
                    Password = claveSmtp // replace with valid value                        
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                try
                {
                    await smtp.SendMailAsync(message);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            
        }
    }
}