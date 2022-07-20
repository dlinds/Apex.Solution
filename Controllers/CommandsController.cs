using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Apex.Models;
using System;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net.Http;
using System.IdentityModel.Tokens.Jwt;


namespace Apex.Controllers
{
#pragma warning disable CS1591
  [Route("api/[controller]")]
  [ApiController]
  public class CommandsController : ControllerBase
  {
    private readonly ApexContext _db;
    public CommandsController(ApexContext db)
    {
      _db = db;
    }

    [HttpGet("Find")]
    // [Authorize(Roles = "Administrator,Viewer")]
    public async Task<IActionResult> Controller(string searchTerm, string appName, string amazonEmail, string submissionText)
    {
      Application application = await _db.Applications.FirstOrDefaultAsync(x => x.Name == appName);
      if (application == null) throw new Exception("Did not find application by that name");
      bool wasResolved = false;
      try
      {
        Command commandOutput = await _db.Commands.FirstOrDefaultAsync(x => x.Keyword == searchTerm && x.Application == application);
        if (commandOutput.Shortcut == null) throw new Exception("Did not find a matching command");
        wasResolved = true;
        commandOutput.CallCount++;
        _db.Entry(commandOutput).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return Ok($"Apex says {commandOutput.Shortcut}");
      }
      catch
      {
        return NotFound("A shortcut could not be found. We have logged your query");
      }
      finally
      {
        UserQuery query = new UserQuery
        {
          Resolved = wasResolved,
          Content = submissionText,
          AmazonEmail = amazonEmail,
          Timestamp = DateTime.Now
        };

        await _db.UserQueries.AddAsync(query);
        await _db.SaveChangesAsync();
      }
    }

    [HttpPost("Add")]
    public async Task<IActionResult> AddCommand(string keyword, string shortcut, string applicationId)
    {
      // keyword, shortcut, adminID = e7f406ad-56af-433c-a20f-28e354524489, appId = 15bd7f44-c56e-4fb2-81ca-6b5b120ec16b (Windows)
      Application application = await _db.Applications.FirstOrDefaultAsync(x => x.ApplicationId == new Guid(applicationId));
      Command newCommand = new Command
      {
        Keyword = keyword,
        Shortcut = shortcut,
        Application = application
      };
      await _db.Commands.AddAsync(newCommand);

      await _db.SaveChangesAsync();
      return Ok("Success");
    }
  }

}
#pragma warning restore CS1591
