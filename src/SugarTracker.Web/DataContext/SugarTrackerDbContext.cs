
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SugarTracker.Web.Entities;

namespace SugarTracker.Web.DataContext
{
  public class SugarTrackerDbContext : IdentityDbContext<User>
  {
    public SugarTrackerDbContext(DbContextOptions<SugarTrackerDbContext> contextOptions) : base(contextOptions)
    {
     
    }

    public DbSet<Reading> Readings { get; set; }
    public DbSet<RawReading> RawReadings { get; set; }
    public DbSet<UserPhoneNumber> UserPhoneNumbers { get; set; } 
  
    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<User>().HasMany(u => u.UserPhoneNumbers).WithOne(n => n.User).HasForeignKey(n => n.UserId);
      builder.Entity<UserPhoneNumber>().ToTable("UserPhoneNumber");
      base.OnModelCreating(builder);
    }
  }
}
