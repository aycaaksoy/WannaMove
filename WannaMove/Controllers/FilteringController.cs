using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using WannaMove.Data;

namespace WannaMove.Controllers
{
    public class FilteringController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FilteringController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Home
        public IActionResult FilterOptions()
        {
            var c = _context.UaScoresDataFrame.ToList();
            return View(c);  
        }

        

    }
}
