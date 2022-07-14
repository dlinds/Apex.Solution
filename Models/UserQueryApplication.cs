using System.Collections.Generic;
using System;

namespace Apex.Models
{
#pragma warning disable CS1591
  public class UserQueryApplication
  {
    public UserQueryApplication()
    {
      this.UserQueryApplicationId = new Guid();
    }
    public Guid UserQueryApplicationId { get; set; }
    public Guid UserQueryId { get; set; }
    public Guid ApplicationId { get; set; }

    public virtual UserQuery UserQuery { get; set; }
    public virtual Application Application { get; set; }
  }
#pragma warning restore CS1591
}
