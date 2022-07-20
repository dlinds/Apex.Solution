using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Apex.Models
{
#pragma warning disable CS1591
  public class ApexContext : IdentityDbContext<Administrator>
  {
    public DbSet<Application> Applications { get; set; }
    public DbSet<Command> Commands { get; set; }
    // public DbSet<CommandApplication> CommandApplications { get; set; }
    public DbSet<UserQuery> UserQueries { get; set; }
    // public DbSet<UserQueryApplication> UserQueryApplications { get; set; }
    // public DbSet<UserQueryCommand> UserQueryCommands { get; set; }
    public DbSet<Administrator> Administrators { get; set; }

    public ApexContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
#pragma warning restore CS1591
}
