using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Apex.Models;
using System;

namespace Apex.Controllers
{
#pragma warning disable CS1591
  [Route("api/[controller]")]
  [ApiController]
  public class ApplicationsController : ControllerBase
  {
    private readonly ApexContext _db;
    public ApplicationsController(ApexContext db)
    {
      _db = db;
    }

    [HttpGet("ReturnAll")]
    public async Task<ActionResult<IEnumerable<Application>>> ReturnAllApplications()
    {
      try
      {
        List<Application> applicationsWithJoins = await _db.Applications.ToListAsync();
        if (applicationsWithJoins.Count == 0) throw new Exception("No applications were found");
        List<Application> applications = new List<Application>();
        foreach (Application application in applicationsWithJoins)
        {
          Application tempApplication = new Application
          {
            ApplicationId = application.ApplicationId,
            Manufacturer = application.Manufacturer,
            Name = application.Name,
            Version = application.Version,
            Administrator = application.Administrator
          };
          applications.Add(tempApplication);
        }
        return applications;
      }
      catch
      {
        return NotFound("No applications were found");
      }
    }

    [HttpGet("Find")]
    public async Task<ActionResult<IEnumerable<Application>>> Find(string name, string manufacturer, string version)
    {
      var query = _db.Applications.AsQueryable();
      try
      {
        if (name != null)
        {
          query = query.Where(x => x.Name == name).OrderByDescending(x => x.Name);
        }
        if (manufacturer != null)
        {
          query = query.Where(x => x.Manufacturer == manufacturer).OrderByDescending(x => x.Manufacturer);
        }
        if (version != null)
        {
          query = query.Where(x => x.Version == version).OrderByDescending(x => x.Version);
        }
        List<Application> appsWithJoins = await query.ToListAsync();
        if (appsWithJoins.Count > 0)
        {
          List<Application> apps = new List<Application>();
          foreach (Application application in appsWithJoins)
          {
            apps.Add(
              new Application
              {
                ApplicationId = application.ApplicationId,
                Manufacturer = application.Manufacturer,
                Name = application.Name,
                Version = application.Version,
                Administrator = application.Administrator
              });
          }
          return apps;
        }
        else
        {
          throw new Exception("No applications were found");
        }

      }
      catch
      {
        return NotFound("No applications were found");
      }
    }

    [HttpPost("Add")]
    public async Task<IActionResult> AddApplication(string name, string version, string manufacturer)
    {
      Application newApplication = new Application
      {
        Name = name,
        Version = version,
        Manufacturer = manufacturer
      };
      await _db.Applications.AddAsync(newApplication);
      await _db.SaveChangesAsync();
      return Ok("Success");
    }

    [HttpPut("Update")]
    public async Task<IActionResult> UpdateApplication(Application application)
    {
      _db.Entry(application).State = EntityState.Modified;
      await _db.SaveChangesAsync();
      return Ok("Success");
    }

#pragma warning restore CS1591
  }

}
