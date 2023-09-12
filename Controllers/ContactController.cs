using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
