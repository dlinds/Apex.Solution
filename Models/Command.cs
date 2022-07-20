using System.Collections.Generic;
using System;

namespace Apex.Models
{
#pragma warning disable CS1591
  public class Command
  {
    public Command()
    {
      this.CommandId = new Guid();
      this.CallCount = 0;
    }
    public Guid CommandId { get; set; }
    public string Keyword { get; set; }
    public string Shortcut { get; set; }
    public int CallCount { get; set; }
    public virtual Application Application { get; set; }

    public virtual Administrator Administrator { get; set; }

  }
#pragma warning restore CS1591
}
