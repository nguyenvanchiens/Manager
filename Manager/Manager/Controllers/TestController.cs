using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
