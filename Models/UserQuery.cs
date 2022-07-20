using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Apex.Models
{
#pragma warning disable CS1591
  public class UserQuery
  {
    public UserQuery()
    {
      this.UserQueryId = new Guid();

    }
    public Guid UserQueryId { get; set; }
    public string AmazonEmail { get; set; }
    public DateTime Timestamp { get; set; }
    public bool Resolved { get; set; }
    public string Content { get; set; }

    public virtual Application Application { get; set; }
    public virtual Command Command { get; set; }
  }
#pragma warning restore CS1591
}
