using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SugarTracker.Web.Controllers
{
    public class HomeController : Controller
    {
      

      public IActionResult Index()
      {
        if (HttpContext.User.Identity.IsAuthenticated)
        {
          return RedirectToAction("index", "dashboard");
        }
        
        return View();
      }

    }
}
