using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WannaMove.Controllers
{
    public class DashboardsController : Controller
    {
        public IActionResult Dashboards()
        {
            return View();
        }
    }
}