using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LBW.Controllers
{
    //[Authorize]
    public class InicioController : Controller
    {

        public IActionResult Bienvenida()
        {
            @ViewBag.bienvenida = "active";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
