using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SugarTracker.Web.DataContext;
using SugarTracker.Web.Entities;

namespace SugarTracker.Web.Services.Repositories
{
    public interface IUserRepository
    {
      User GetUser(string userId);
    }

    public class UserRepository : IUserRepository
    {
      private readonly SugarTrackerDbContext _context;

      public UserRepository(SugarTrackerDbContext context)
      {
        _context = context;
      }

      public User GetUser(string userId)
      {
        return _context.Users.FirstOrDefault(u => u.Id == userId);
      }
    }
}
