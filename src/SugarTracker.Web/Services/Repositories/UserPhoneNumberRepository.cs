using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SugarTracker.Web.DataContext;
using SugarTracker.Web.Entities;

namespace SugarTracker.Web.Services.Repositories
{
  public interface IUserPhoneNumberRepository
  {
    IEnumerable<UserPhoneNumber> GetUserPhoneNumbers(string userId);
    void AddUserPhoneNumber(UserPhoneNumber userPhoneNumber);
  }

    public class UserPhoneNumberRepository:IUserPhoneNumberRepository
    {
      private readonly SugarTrackerDbContext _context;

      public UserPhoneNumberRepository(SugarTrackerDbContext context)
      {
        _context = context;
      }

      public IEnumerable<UserPhoneNumber> GetUserPhoneNumbers(string userId)
      {
        return _context.UserPhoneNumbers.Where(u => u.UserId == userId);
      }

      public void AddUserPhoneNumber(UserPhoneNumber userPhoneNumber)
      {
         _context.Add(userPhoneNumber);
        _context.SaveChanges();
      }
    }
}
