using Microsoft.AspNetCore.Mvc;

namespace WannaMove.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult AboutTeam()
        {
            return View();
        }
    }
}
