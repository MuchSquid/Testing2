using Microsoft.AspNetCore.Mvc;
using LBW.Data;
using LBW.Models;
using System.Text;
using System.Security.Cryptography;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using LBW.Models.Entity;
using Usuario = LBW.Models.Usuario;

namespace LBW.Controllers
{
    public class Acceso : Controller
    {

        UsuarioDatos _UsuarioDatos = new UsuarioDatos();
        public IActionResult Login()
        {
            return View();
        }

        private LbwContext _context;

        public Acceso(LbwContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuario _usuario)
        {
            Console.WriteLine("Pass 1234 😍", _usuario);
            var usuario = _UsuarioDatos.ValidarUsuario(_usuario.UsuarioID);
            try
            {
                if (usuario != null)
                {
                    var claims = new List<Claim>
                    {
                     new Claim(ClaimTypes.Name, usuario.NombreCompleto)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    var currentDateTime = DateTime.UtcNow;
                    var dateWithoutTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day);

                    await _context.SaveChangesAsync();

                    return RedirectToAction("Bienvenida", "Inicio");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al iniciar sesión: " + ex.Message;
                return View();
            }
            if(usuario == null)
            {
                TempData["Error"] = "No hay usuario";
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Acceso");
        }

    }
}
