using induwbd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace induwbd.Controllers
{
    public class UsuarioController : Controller
    {
        private static List<Usuario> Usuarios = new List<Usuario>();
        private static List<Role> Roles = new List<Role>
        {
            new Role { RoleId = 1, Nombre = "User" }
        };

        // GET: Usuario/Registro
        public ActionResult Registro()
        {
            return View();
        }

        // POST: Usuario/Registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registro([Bind(Include = "Email,Password")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.UserId = Usuarios.Count > 0 ? Usuarios.Max(u => u.UserId) + 1 : 1;
                usuario.RoleId = 1; // Default role
                usuario.Role = Roles.FirstOrDefault(r => r.RoleId == 1);
                Usuarios.Add(usuario);

                EnviarCorreoDeBienvenida(usuario.Email);

                return RedirectToAction("Login");
            }

            return View(usuario);
        }

        // GET: Usuario/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Usuario/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            var usuario = Usuarios.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (usuario != null)
            {
                // Set authentication cookie or session
                Session["UserId"] = usuario.UserId;
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }

        // GET: Usuario/Logout
        public ActionResult Logout()
        {
            // Clear session
            Session["UserId"] = null;
            return RedirectToAction("Login");
        }

        // GET: Usuario/Perfil
        public ActionResult Perfil()
        {
            int userId = (int)Session["UserId"];
            var usuario = Usuarios.FirstOrDefault(u => u.UserId == userId);
            if (usuario == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(usuario);
        }

        // POST: Usuario/Perfil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Perfil([Bind(Include = "UserId,Email,Password")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var existingUser = Usuarios.FirstOrDefault(u => u.UserId == usuario.UserId);
                if (existingUser != null)
                {
                    existingUser.Email = usuario.Email;
                    existingUser.Password = usuario.Password;
                }
                return RedirectToAction("Perfil");
            }
            return View(usuario);
        }

        private void EnviarCorreoDeBienvenida(string email)
        {
            // Correo electrónico de indukitchen
            string correoIndukitchen = "indukitchen8@gmail.com";
            string contraseñaIndukitchen = "indu12345"; // Tu contraseña aquí

            // Crear un mensaje de correo
            MailMessage mensaje = new MailMessage();
            mensaje.From = new MailAddress(correoIndukitchen);
            mensaje.To.Add(email);
            mensaje.Subject = "¡Bienvenido a INDUKITCHEN!";
            mensaje.Body = "¡Hola! Gracias por registrarte en INDUKITCHEN. Esperamos que disfrutes de nuestros productos y servicios.";

            // Configurar el servidor SMTP
            SmtpClient clienteSmtp = new SmtpClient("smtp.gmail.com", 587);
            clienteSmtp.EnableSsl = true;
            clienteSmtp.UseDefaultCredentials = false;
            clienteSmtp.Credentials = new System.Net.NetworkCredential(correoIndukitchen, contraseñaIndukitchen);

            // Enviar el correo
            try
            {
                clienteSmtp.Send(mensaje);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el correo: " + ex.Message);
            }
            finally
            {
                // Liberar recursos
                mensaje.Dispose();
            }
        }
    }
}
