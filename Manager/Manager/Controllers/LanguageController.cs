using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class LanguageController : BaseController
    {
        public LanguageController(MyDbContext dbContext) : base(dbContext)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
