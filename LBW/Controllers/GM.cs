 using LBW.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LBW.Controllers
{
    public class GMController : Controller
    {
        private LbwContext _context; 
        public GMController (LbwContext context)
        {
            _context = context;
        }
        public IActionResult IngresoMuestras()
        {
            @ViewBag.gm = "active";
            @ViewBag.ingresomuestra = "active";
            return View();
        }
        public IActionResult Recepcion()
        {
            @ViewBag.gm = "active";
            @ViewBag.recepcion = "active";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
