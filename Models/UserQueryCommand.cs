using System.Collections.Generic;
using System;

namespace Apex.Models
{
#pragma warning disable CS1591
  public class UserQueryCommand
  {
    public UserQueryCommand()
    {
      this.UserQueryCommandId = new Guid();
    }
    public Guid UserQueryCommandId { get; set; }
    public Guid UserQueryId { get; set; }
    public Guid CommandId { get; set; }

    public virtual UserQuery UserQuery { get; set; }
    public virtual Command Command { get; set; }
  }
#pragma warning restore CS1591
}
