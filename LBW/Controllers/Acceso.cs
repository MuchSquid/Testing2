﻿using Microsoft.AspNetCore.Mvc;
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
            var usuario = _UsuarioDatos.ValidarUsuario(_usuario.Nombre, ConvertirSha256(_usuario.Email));

            try
            {
                if (true)
                {
                    var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, usuario.Nombre)
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

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Acceso");
        }

        public static string ConvertirSha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));
                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }
    }
}
