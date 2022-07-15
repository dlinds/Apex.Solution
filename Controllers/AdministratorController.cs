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
  public class AdministratorController : ControllerBase
  {
    private readonly ApexContext _db;
    public AdministratorController(ApexContext db)
    {
      _db = db;
    }


    [HttpGet("Admins")]
    [Authorize(Roles = "Administrator")]
    public IActionResult Admins()
    {
      Administrator currentUser = GetCurrentUser();

      return Ok($"Hi {currentUser.FirstName}, you are a {currentUser.Role}");
    }

    [HttpPost("NewUser")]
    [Authorize(Roles = "Administrator")]
    public IActionResult NewUser(Administrator newUser)
    {
      try
      {
        _db.Administrators.Add(newUser);
        _db.SaveChanges();
        return Ok($"Created {newUser.FirstName}");
      }
      catch
      {
        return Unauthorized("asd");
      }
    }

    [HttpGet("Public")]
    public IActionResult Public()
    {
      return Ok("Hi you are on public");
    }

    private Administrator GetCurrentUser()
    {
      ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;

      if (identity != null)
      {
        IEnumerable<Claim> userClaims = identity.Claims;
        return new Administrator
        {
          Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
          FirstName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
          LastName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
          Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
        };
      }
      return null;
    }

  }
#pragma warning restore CS1591
}
