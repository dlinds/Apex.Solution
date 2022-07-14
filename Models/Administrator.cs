using Microsoft.AspNetCore.Identity;
// using RestSharp.Authenticators;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Linq;
// using System.Threading.Tasks;
// using RestSharp;
// using System;
// using System.Collections.Generic;
// using System.Data;
// using System.Diagnostics;
// using System.IO;

#pragma warning disable CS1591
namespace Apex.Models
{
  public class Administrator : IdentityUser
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }

  }
}
#pragma warning restore CS1591
