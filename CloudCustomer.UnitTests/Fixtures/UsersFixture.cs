using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomer.UnitTests.Fixtures
{
    public static class UsersFixture
    {
        public static List<User> GetTestUsers() => new()
        {
            new User {
                    Id=1,
                    Name="Archie",
                    Email="archiefan@gmail.com",
                    Address = new Address
                    {
                        PostCode = "",
                        Street = "",
                        City = ""
                    }
                },
            new User {
                    Id=2,
                    Name="GuiZhen",
                    Email="wife201206@gmail.com",
                    Address = new Address
                    {
                        PostCode = "",
                        Street = "",
                        City = ""
                    }
                }
        };
    }
}
