using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WannaMove.Controllers
{
    public class CriteriaComparison : Controller
    {
        public IActionResult PairwiseComparison()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CalculateAhp(string[] array1, string[] array2, string[] array3)
        {
            // Manipulate the arrays here

            // For example, you could concatenate them into a single array like this:
            var combinedArray = array1.Concat(array2).Concat(array3).ToArray();

            // Then you can return a response to the Ajax call
            return Json(new { success = true });
        }
    }
}
