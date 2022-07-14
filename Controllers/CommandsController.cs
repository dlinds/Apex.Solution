using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Apex.Models;
using System;
using Microsoft.AspNetCore.Cors;


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
#pragma warning restore CS1591
    //Get api/Commands
    /// <summary>
    /// Gets multiple Commands
    /// </summary>
    /// <remarks>
    /// Get all Commands:
    ///
    ///     GET /Commands
    ///     {
    ///     }
    ///
    ///
    ///
    /// </remarks>
    ///
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Command>>> Get(string sortMethod)
    {
      var query = _db.Commands.AsQueryable();
      query = _db.Commands.OrderByDescending(Command => Command.CallCount);
      // else if (sortMethod == "averageRating")
      // {
      //   query = _db.Commands.OrderByDescending(Command => Command.AverageRating);
      // }
      // else
      // {
      //   query = _db.Commands.OrderBy(Command => Command.Country);
      // }
      return await query.ToListAsync();
    }

    //Post api/Commands
    /// <summary>
    /// Creates new Command
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     Post /Commands
    ///     {
    ///       "Country": "Sample Country",
    ///       "City": "Sample City",
    ///       "Name": "Sample Name",
    ///       "AverageRating": a decimal number,
    ///       "NumOfApplications": an integer
    ///       "ImgLink": "A URL string of Command"
    ///     }
    ///
    ///
    /// </remarks>
    ///
    /// <param name="Command">A Command</param>
    /// <response code="201">Returns a newly created Command</response>
    [HttpPost]
    public async Task<ActionResult<Command>> Post(Command Command)
    {
      _db.Commands.Add(Command);
      await _db.SaveChangesAsync();
      return CreatedAtAction("Post", new { id = Command.CommandId }, Command);
    }



    // //  Get api/Commands/3
    // /// <summary>
    // /// Retrieve Command based on Id
    // /// </summary>
    // /// <remarks>
    // /// Sample request:
    // ///
    // ///     Get /Commands/4
    // ///     {
    // ///     }
    // ///
    // /// </remarks>
    // ///
    // /// <param name="id">Command Id</param>
    // /// <response code="404">No Command with that Id exists</response>
    // [EnableCors("AnotherPolicy")]
    // [HttpGet("{id}")]
    // public async Task<ActionResult<Command>> GetCommand(int id)
    // {
    //   var Command = await _db.Commands.FindAsync(id);
    //   if (Command == null)
    //   {
    //     return NotFound();
    //   }
    //   return Command;
    // }

    // //Put api/Commands/4
    // /// <summary>
    // /// Edits a Command
    // /// </summary>
    // /// <remarks>
    // /// Sample request:
    // ///
    // ///     Put /Commands/id
    // ///     {
    // ///       "CommandId": id,
    // ///       "Country": "Updated Country Name",
    // ///       "City": "Updated City Name",
    // ///       "Name": "Updated Command Name",
    // ///       "AverageRating": updated decimal number,
    // ///       "NumOfApplications": updated integer
    // ///     }
    // ///
    // ///
    // /// </remarks>
    // ///
    // /// <param name="id"></param>
    // /// <param name="Command"></param>
    // /// <response code="204">Updates Command</response>
    // /// <response code="400">Command ID doesn't match ID that is passed.</response>
    // [HttpPut("{id}")]
    // public async Task<IActionResult> Put(int id, Command Command)
    // {
    //   if (id != Command.CommandId)
    //   {
    //     return BadRequest();
    //   }
    //   _db.Entry(Command).State = EntityState.Modified;

    //   try
    //   {
    //     await _db.SaveChangesAsync();
    //   }
    //   catch (DbUpdateConcurrencyException)
    //   {
    //     if (!CommandExists(id))
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
    // /// Removes a Command
    // /// </summary>
    // /// <remarks>
    // /// Sample request:
    // ///
    // ///     Delete /Commands/id
    // ///     {
    // ///     }
    // ///
    // ///
    // /// </remarks>
    // ///
    // /// <param name="id"></param>
    // /// <response code="204">Deletes Command</response>
    // /// <response code="404">Command ID doesn't exist.</response>
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeleteCommand(int id)
    // {
    //   var Command = await _db.Commands.FindAsync(id);
    //   if (Command == null)
    //   {
    //     return NotFound();
    //   }

    //   _db.Commands.Remove(Command);
    //   await _db.SaveChangesAsync();

    //   return NoContent();
    // }

    // //Get api/Commands/x

    // /// <summary>
    // /// Gets a random Command
    // /// </summary>
    // /// <remarks>
    // /// Sample request:
    // ///
    // ///     Get /Commands/GetRandom
    // ///     {
    // ///     }
    // ///
    // ///
    // /// </remarks>
    // ///
    // /// <response code="204">Random value doesn't exist.</response>
    // [HttpGet("GetRandom")]
    // public async Task<ActionResult<Command>> GetRandomCommand()
    // {
    //   var query = _db.Commands.AsQueryable();

    //   Command newestCommand = _db.Commands
    //                   .OrderByDescending(p => p.CommandId)
    //                   .FirstOrDefault();
    //   int count = newestCommand.CommandId + 1;
    //   Random rand = new Random();
    //   int num = rand.Next(0, count);


    //   bool isFound = false;

    //   while (isFound != true)
    //   {
    //     isFound = _db.Commands.Any(d => d.CommandId == num);

    //     if (isFound == true)
    //     {
    //       query = _db.Commands.Where(d => d.CommandId == num);
    //       break;
    //     }
    //     else
    //     {
    //       num = rand.Next(0, count);
    //     }
    //   }

    //   return await query.FirstOrDefaultAsync();

    // }

    // private bool CommandExists(int id)
    // {
    //   return _db.Commands.Any(e => e.CommandId == id);
    // }
  }

}
