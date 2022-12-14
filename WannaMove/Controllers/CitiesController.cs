using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WannaMove.Data;

namespace WannaMove.Controllers
{
    public class CitiesController : Controller
    {

        private readonly ApplicationDbContext _context;

        public CitiesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Info()
        {
            var c = _context.UaScoresDataFrame.ToList();
            return View(c);
        }
        [HttpGet]
        public IActionResult Details(string name)
        {
            var item = _context.UaScoresDataFrame.Where(c => c.CityName == name).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }
    }
}
