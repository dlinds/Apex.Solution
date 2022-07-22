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
      bool wasResolved = false;
      try
      {
        if (searchTerm == null || appName == null) throw new Exception("You must include an application and search term");
        List<Application> applications = await _db.Applications.Where(x => x.Name == appName).ToListAsync();
        if (applications.Count == 0) throw new Exception("Did not find any applications by that name");
        Command commandOutput = await _db.Commands.FirstOrDefaultAsync(x => x.Keyword == searchTerm && x.Application == applications[0]);
        if (commandOutput.Shortcut == null) throw new Exception("Did not find a matching command");
        wasResolved = true;
        commandOutput.CallCount++;
        _db.Entry(commandOutput).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return Ok($"Apex says {commandOutput.Shortcut}");
      }
      catch (Exception e)
      {
        return NotFound(e.Message.ToString());
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

    [HttpGet("AppId")]
    public async Task<ActionResult<List<Command>>> GetCommandsByAppId(string id)
    {
      Guid applicationId = Guid.Parse(id);
      try
      {
        Application application = await _db.Applications.FirstOrDefaultAsync(x => x.ApplicationId == applicationId);
        if (application.Name == null) throw new Exception("No application was found");
        List<Command> commands = await _db.Commands.Where(x => x.Application == application).ToListAsync();
        return commands;
      }
      catch (Exception e)
      {
        return NotFound(e.Message);
      }
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Command>> GetCommandById(string id)
    {
      Guid commandId = Guid.Parse(id);

      Command command = await _db.Commands.FirstOrDefaultAsync(x => x.CommandId == commandId);
      try
      {
        return command;
      }
      catch
      {
        return NotFound("Command not found");
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

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCommand(Command command)
    {
      _db.Entry(command).State = EntityState.Modified;
      await _db.SaveChangesAsync();
      return Ok("Success");
    }
  }

}
#pragma warning restore CS1591
