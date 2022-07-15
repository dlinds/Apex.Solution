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

#pragma warning restore CS1591
  }

}
