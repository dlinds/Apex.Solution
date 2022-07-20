using System.Collections.Generic;
using System;

namespace Apex.Models
{
#pragma warning disable CS1591
  public class Application
  {
    public Application()
    {
      this.ApplicationId = new Guid();
    }
    public Guid ApplicationId { get; set; }
    public string Manufacturer { get; set; }
    public string Name { get; set; }
    public string Version { get; set; }

    public virtual Administrator Administrator { get; set; }
  }
#pragma warning restore CS1591
}
