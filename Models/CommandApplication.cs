using System.Collections.Generic;
using System;

namespace Apex.Models
{
#pragma warning disable CS1591
  public class CommandApplication
  {
    public CommandApplication()
    {
      this.CommandApplicationId = new Guid();
    }
    public Guid CommandApplicationId { get; set; }
    public Guid CommandId { get; set; }
    public Guid ApplicationId { get; set; }

    public virtual Command Command { get; set; }
    public virtual Application Application { get; set; }
  }
#pragma warning restore CS1591
}
