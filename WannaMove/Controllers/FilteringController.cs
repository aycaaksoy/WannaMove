using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using WannaMove.Data;
using WannaMove.Models;

namespace WannaMove.Controllers
{
    public class FilteringController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FilteringController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IActionResult Filter()
        {
            var c = _context.UaScoresDataFrame.ToList();
            return View(c);
        }

        public IActionResult Filter1()
        {
            var c = _context.UaScoresDataFrame.ToList();
            return View(c);
        }








    }
}
