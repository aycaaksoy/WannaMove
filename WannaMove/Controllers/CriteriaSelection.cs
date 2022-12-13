using Microsoft.AspNetCore.Mvc;

namespace WannaMove.Controllers
{
    public class CriteriaSelection : Controller
    {
        public IActionResult DragAndDrop()
        {
            return View();
        }
        public IActionResult SelectCriteria()
        {
            return View();
        }
    }
}
