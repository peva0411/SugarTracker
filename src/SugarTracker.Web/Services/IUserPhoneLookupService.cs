using System;
using SugarTracker.Web.Entities;
using SugarTracker.Web.Services.Repositories;

namespace SugarTracker.Web.Services
{
  public interface IUserPhoneLookupService
  {
    string LookupUserId(string phoneNumber);
  }

  public class UserPhoneLookupService: IUserPhoneLookupService
  {
    private readonly IUserPhoneNumberRepository _userPhoneNumberRepository;

    public UserPhoneLookupService(IUserPhoneNumberRepository userPhoneNumberRepository)
    {
      _userPhoneNumberRepository = userPhoneNumberRepository;
    }

    public string LookupUserId(string phoneNumber)
    {
      var userPhoneNumber = _userPhoneNumberRepository.FindUserByNumber(phoneNumber);
      if (userPhoneNumber == null) throw new Exception($"Phone number {phoneNumber} was not found to be associated with a user");

      return userPhoneNumber.UserId;
    }
  }
}