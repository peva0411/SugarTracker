using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SugarTracker.Web.Entities;
using SugarTracker.Web.ViewModels;

namespace SugarTracker.Web.Controllers
{
    public class AccountController : Controller
    {
      private readonly UserManager<User> _userManager;
      private readonly SignInManager<User> _signInManager;

      public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
      {
        _userManager = userManager;
        _signInManager = signInManager;
      }

      [HttpGet]
      public ViewResult Login()
      {
        return View();
      }

      [HttpPost]
      public async Task<IActionResult> Login(LoginViewModel model)
      {
        if (ModelState.IsValid)
        {
          var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, true, false);
          if (result.Succeeded)
          {
            return RedirectToAction("Index", "Home");
          }
        }
        return View(model);
      }

      [HttpGet]
      public ViewResult Register()
      {
        return View();
      }

      [HttpPost]
      public async Task<IActionResult> Register(RegisterViewModel model)
      {
        if (ModelState.IsValid)
        {
          var user = new User {UserName = model.Email, Email = model.Email};
          var result = await _userManager.CreateAsync(user, model.Password);

          if (result.Succeeded)
          {
            await _signInManager.SignInAsync(user, true);
            return RedirectToAction("Index", "Home");
          }
          else
          {
            foreach (var identityError in result.Errors)
            {
              ModelState.AddModelError("", identityError.Description);
            }
          }
        }

        return View();
      }
    }
}
