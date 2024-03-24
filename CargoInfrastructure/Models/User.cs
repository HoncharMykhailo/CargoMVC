using Microsoft.AspNetCore.Identity;

namespace CargoInfrastructure
{
    public class User : IdentityUser
    {

      //  public string Email { get; set; }
        public int Year { get; set; }
      //  public string Name { get; set; }
    }
}
