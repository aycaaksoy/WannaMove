using Microsoft.AspNetCore.Mvc;

namespace WannaMove.Controllers
{
    public class CriteriaComparison : Controller
    {
        public IActionResult PairwiseComparison()
        {
            return View();
        }
    }
}
