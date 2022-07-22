using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Apex.Models;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

    [HttpGet("{id}")]
    public async Task<ActionResult<Application>> GetApplicationById(string id)
    {
      Guid applicationId = Guid.Parse(id);

      Application application = await _db.Applications.FirstOrDefaultAsync(x => x.ApplicationId == applicationId);
      try
      {
        return application;
      }
      catch
      {
        return NotFound("Application not found");
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
          query = query.Where(x => x.Name.ToLower() == name.ToLower()).OrderByDescending(x => x.Name);
        }
        if (manufacturer != null)
        {
          query = query.Where(x => x.Manufacturer.ToLower() == manufacturer.ToLower()).OrderByDescending(x => x.Manufacturer);
        }
        if (version != null)
        {
          query = query.Where(x => x.Version.ToLower() == version.ToLower()).OrderByDescending(x => x.Version);
        }

        if (await query.CountAsync() > 0)
        {
          List<Application> appsWithoutJoins = await query.Select(x =>
            new Application
            {
              ApplicationId = x.ApplicationId,
              Manufacturer = x.Manufacturer,
              Name = x.Name,
              Version = x.Version,
              Administrator = x.Administrator
            }
          ).ToListAsync();
          return appsWithoutJoins;
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
      try
      {
        Application currentApplication = await _db.Applications.FirstOrDefaultAsync(x => x.Name == name && x.Version == version && x.Manufacturer == manufacturer);
        if (currentApplication.Name != null) throw new Exception("Already exists");
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
      catch
      {
        return Ok("Application already exists"); //fix this to return something other than OK
      }
    }

    [HttpPut()]
    public async Task<IActionResult> UpdateApplication(Application application)
    {
      _db.Entry(application).State = EntityState.Modified;
      await _db.SaveChangesAsync();
      return Ok("Success");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteApplication(string id)
    {
      Guid applicationId = Guid.Parse(id);
      Application application = await _db.Applications.FindAsync(applicationId);
      try
      {
        if (application == null) throw new Exception("Application not found");

        _db.Applications.Remove(application);
        await _db.SaveChangesAsync();
        return Ok("Deleted");
      }
      catch (Exception error)
      {
        return NotFound(error.Message);
      }
    }

#pragma warning restore CS1591
  }

}
