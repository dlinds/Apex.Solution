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
  public class AdministratorController : ControllerBase
  {
    private readonly ApexContext _db;
    public AdministratorController(ApexContext db)
    {
      _db = db;
    }
#pragma warning restore CS1591
    //Get api/Applications
    //Get api/Commands
    /// <summary>
    /// Gets list of Applications based on search criteria
    /// </summary>
    /// <remarks>
    /// Get all Applications:
    ///
    ///     GET /Applications
    ///     {
    ///     }
    ///
    /// Sort by Country:
    ///
    ///     GET /Applications?country={insert country name}
    ///     {
    ///     }
    ///
    /// Sort by City:
    ///
    ///     GET /Commands?city={insert city name}
    ///     {
    ///     }
    ///
    ///
    ///     GET /Commands?name={insert Command name}
    ///     {
    ///     }
    ///
    ///
    /// </remarks>
    ///
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Administrator>>> Get()
    {
      var query = _db.Administrators.AsQueryable();
      // var dQuery = _db.Commands.AsQueryable();
      // if (name != null)
      // {
      //   var nameJoinQuery = from Command in _db.Commands
      //                       where Command.Name == name
      //                       join Application in _db.Applications on Command.CommandId equals Application.CommandId
      //                       select Application;
      //   return await nameJoinQuery.ToListAsync();
      // }
      // // var innerJoinQuery = _db.Applications.AsQueryable();
      // if (country != null && city != null)
      // {
      //   var countryJoinQuery = from Command in _db.Commands
      //                          where Command.Country == country
      //                          where Command.City == city
      //                          join Application in _db.Applications on Command.CommandId equals Application.CommandId
      //                          select Application;
      //   return await countryJoinQuery.ToListAsync();
      // }
      // if (country != null)
      // {
      //   var countryJoinQuery = from Command in _db.Commands
      //                          where Command.Country == country
      //                          join Application in _db.Applications on Command.CommandId equals Application.CommandId
      //                          select Application;
      //   return await countryJoinQuery.ToListAsync();
      // }
      // if (city != null)
      // {
      //   var cityJoinQuery = from Command in _db.Commands
      //                       where Command.City == city
      //                       join Application in _db.Applications on Command.CommandId equals Application.CommandId
      //                       select Application;
      //   return await cityJoinQuery.ToListAsync();
      // }
      return await query.ToListAsync();

    }

    // //Post api/Applications
    // /// <summary>
    // /// Creates new Application
    // /// </summary>
    // /// <remarks>
    // /// Sample request:
    // ///
    // ///     Post /Applications
    // ///     {
    // ///       "CommandId": integer id of country,
    // ///       "ApplicationText": "text of Application",
    // ///       "Rating": integer rating,
    // ///       "UserName": "a name"
    // ///     }
    // ///
    // ///
    // /// </remarks>
    // ///
    // /// <param name="Application">A Application</param>
    // /// <response code="201">Returns a newly created Application</response>
    // [HttpPost]
    // public async Task<ActionResult<Application>> Post(Application Application)
    // {

    //   Command Command = _db.Commands.FirstOrDefault(a => a.CommandId == Application.CommandId);
    //   Command.CalculateAverage(Application.Rating);
    //   _db.Entry(Command).State = EntityState.Modified;
    //   _db.Applications.Add(Application);
    //   await _db.SaveChangesAsync();
    //   return CreatedAtAction("Post", new { id = Application.ApplicationId }, Application);
    // }

    // //Get api/Applications/3
    // /// <summary>
    // /// Retrieve Application based on Id
    // /// </summary>
    // /// <remarks>
    // /// Sample request:
    // ///
    // ///     Get /Applications/4
    // ///     {
    // ///     }
    // ///
    // /// </remarks>
    // ///
    // /// <param name="id">Application Id</param>
    // /// <response code="404">No Application with that Id exists</response>
    // [HttpGet("{id}")]
    // public async Task<ActionResult<Application>> GetApplication(int id)
    // {
    //   var Application = await _db.Applications.FindAsync(id);
    //   if (Application == null)
    //   {
    //     return NotFound();
    //   }
    //   return Application;
    // }

    // //Put api/Application/4
    // /// <summary>
    // /// Edits a Application
    // /// </summary>
    // /// <remarks>
    // /// Sample request:
    // ///
    // ///     Put /Applications/id
    // ///     {
    // ///       "ApplicationId": id,
    // ///       "CommandId": id,
    // ///       "Application Text": "Updated Application Text",
    // ///       "rating": Updated Application Rating,
    // ///       "userName": "Updated Command Name",
    // ///     }
    // ///
    // ///
    // /// </remarks>
    // ///
    // /// <param name="id">Id of the Application being updated</param>
    // /// <param name="Application"></param>
    // /// <param name="userName">Username of person who originally submitted Application</param>
    // /// <response code="204">Application updated successfully</response>
    // /// <response code="400">Application ID doesn't match ID that is passed.</response>
    // [HttpPut("{id}")]
    // public async Task<IActionResult> Put(int id, Application Application, string userName)
    // {
    //   if (id != Application.ApplicationId || Application.UserName != userName)
    //   {
    //     return BadRequest();
    //   }
    //   var thisApplication = _db.Applications.FirstOrDefault(r => r.ApplicationId == id);
    //   _db.Entry(thisApplication).State = EntityState.Detached;
    //   if (thisApplication.UserName != userName)
    //   {
    //     return BadRequest("Please use accurate user name!");
    //   }
    //   _db.Entry(Application).State = EntityState.Modified;
    //   // thisApplication = null;
    //   try
    //   {
    //     Command Command = _db.Commands.FirstOrDefault(a => a.CommandId == Application.CommandId);
    //     Command.ReCalculateAverage(Application.Rating);
    //     _db.Entry(Command).State = EntityState.Modified;
    //     await _db.SaveChangesAsync();
    //   }
    //   catch (DbUpdateConcurrencyException)
    //   {
    //     if (!ApplicationExists(id))
    //     {
    //       return NotFound();
    //     }
    //     else
    //     {
    //       throw;
    //     }

    //   }
    //   return NoContent();

    // }
    // /// <summary>
    // /// Removes a Application
    // /// </summary>
    // /// <remarks>
    // /// Sample request:
    // ///
    // ///     Delete /Applications/id
    // ///     {
    // ///     }
    // ///
    // ///
    // /// </remarks>
    // ///
    // /// <param name="id"></param>
    // /// <response code="204">Deletes Application</response>
    // /// <response code="404">Application ID doesn't exist.</response>
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeleteApplication(int id)
    // {
    //   var Application = await _db.Applications.FindAsync(id);
    //   if (Application == null)
    //   {
    //     return NotFound();
    //   }

    //   _db.Applications.Remove(Application);
    //   Command Command = _db.Commands.FirstOrDefault(a => a.CommandId == Application.CommandId);
    //   Command.DeCalculateAverage(Application.Rating);
    //   _db.Entry(Command).State = EntityState.Modified;
    //   await _db.SaveChangesAsync();

    //   return NoContent();
    // }

    // private bool ApplicationExists(int id)
    // {
    //   return _db.Applications.Any(r => r.ApplicationId == id);
    // }
  }

}
