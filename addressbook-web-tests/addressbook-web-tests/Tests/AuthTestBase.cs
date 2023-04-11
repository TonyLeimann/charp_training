using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests.Tests
{
    public class AuthTestBase : TestBase
    {
        [OneTimeSetUp]

        public void SetupLogin()
        {
            app.Auth.Login(new AccountData("admin", "secret"));

        }
    }
}
