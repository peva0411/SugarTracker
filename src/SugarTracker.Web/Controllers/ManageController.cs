﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SugarTracker.Web.Entities;
using SugarTracker.Web.Services;
using SugarTracker.Web.Services.Repositories;
using SugarTracker.Web.ViewModels;

namespace SugarTracker.Web.Controllers
{
  [Authorize]
  public class ManageController : Controller
  {
    private readonly UserManager<User> _userManager;
    private readonly ISmsService _smsService;
    private readonly IUserPhoneNumberRepository _phoneNumberRepository;

    public ManageController(UserManager<User> userManager, ISmsService smsService, IUserPhoneNumberRepository phoneNumberRepository)
    {
      _userManager = userManager;
      _smsService = smsService;
      _phoneNumberRepository = phoneNumberRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      var user = await _userManager.GetUserAsync(HttpContext.User);
      var userNumbers = _phoneNumberRepository.GetUserPhoneNumbers(user.Id);
      var manageViewModel = new ManageViewModel { UserPhoneNumbers = userNumbers.ToList() };
      return View(manageViewModel);
    }

    [HttpGet]
    public IActionResult AddPhoneNumber()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
    {
      var user = await GetUserAsync();
      var token = await _userManager.GenerateChangePhoneNumberTokenAsync(user, model.PhoneNumber);

      _smsService.SendMessage(model.PhoneNumber, token);

      return RedirectToAction(nameof(VerifyPhoneNumber), new {phoneNumber = model.PhoneNumber});
    }

    private async Task<User> GetUserAsync()
    {
      return await _userManager.GetUserAsync(HttpContext.User);
    }

    [HttpGet]
    public IActionResult VerifyPhoneNumber(string phoneNumber)
    {
      
      return View(new VerifyPhoneNumberViewModel() {PhoneNumber = phoneNumber});
    }

    [HttpPost]
    public async Task<IActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
    {
      var user = await GetUserAsync();
      var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, model.PhoneNumber);
      if (code == model.Code)
      {
        _phoneNumberRepository.AddUserPhoneNumber(new UserPhoneNumber() {PhoneNumber = model.PhoneNumber, UserId = user.Id});
        return RedirectToAction("Index", "Home");
      }
      return View(model);
    } 
  }
}
