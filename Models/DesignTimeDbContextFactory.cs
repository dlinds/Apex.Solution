using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Apex.Models
{
#pragma warning disable CS1591
  public class ApexContextFactory : IDesignTimeDbContextFactory<ApexContext>
  {

    ApexContext IDesignTimeDbContextFactory<ApexContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

      var builder = new DbContextOptionsBuilder<ApexContext>();

      builder.UseMySql(configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(configuration["ConnectionStrings:DefaultConnection"]));

      return new ApexContext(builder.Options);
    }
#pragma warning restore CS1591
  }
}
