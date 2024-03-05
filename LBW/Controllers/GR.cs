using LBW.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace LBW.Controllers
{
    public class GRController : Controller
    {
        private LbwContext _context;
        public GRController(LbwContext context)
        {
            _context = context;
        }
        public IActionResult Analisis()
        {
            @ViewBag.gr = "active";
            @ViewBag.analisis = "active";
            return View();
        }
        public IActionResult Autorizar()
        {
            @ViewBag.gr = "active";
            @ViewBag.autorizar = "active";
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
