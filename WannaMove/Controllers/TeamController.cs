using Microsoft.AspNetCore.Mvc;

namespace WannaMove.Controllers
{
    public class TeamController : Controller
    {
        public IActionResult Team()
        {
            return View();
        }
    }
}
