using Microsoft.AspNetCore.Mvc;

namespace LBW.Controllers
{
    public class ReporteCliente : Controller
    {
        public IActionResult Reporte()
        {
            @ViewBag.reporte = "active";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
