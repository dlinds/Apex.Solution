using System.Collections.Generic;

namespace Apex.Models
{
  public class UserConstants
  {
    public static List<Administrator> Administrators = new List<Administrator>()
    {
      new Administrator()
      {
        FirstName = "Daniel", LastName = "Lindsey", Email="daniel@daniel.com", Password = "Password", Role="Administrator"
      },
      new Administrator()
      {
        FirstName = "Alexa", LastName = "Admin", Email="alexa@daniel.com", Password = "Alexa", Role="Viewer"
      }
    };
  }
}
