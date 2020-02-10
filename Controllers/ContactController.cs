using Microsoft.AspNetCore.Mvc;

namespace Snacks.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}