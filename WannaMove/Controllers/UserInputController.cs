using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WannaMove.Models;

namespace WannaMove.Controllers
{
    public class UserInputController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

       
    }
}
