using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class UserRepository
    {
        public static List<User> Users = new List<User>()
        {
          new User() { UserName = "admin", Password= "admin", Role="Administrator", EmailAddress = "admin@test.com", IsActive = true },
          new User() { UserName = "test", Password= "test", Role="Standard", EmailAddress = "test@test.com", IsActive = true }
        };
    }
}
