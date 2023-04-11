using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests.Tests
{
    [TestFixture]
    public class LoginTests:TestBase
    {
        [Test]
        public void LoginWithValidCredentials()
        {
            //подготовка
            app.Auth.Logout();
            //Действие
            AccountData account = new AccountData("admin", "secret");
            app.Auth.Login(account);
            //Проверка
            Assert.IsTrue(app.Auth.IsLoggedIn(account));
        }
        [Test]
        public void LoginWithInalidCredentials()
        {
            //подготовка
            app.Auth.Logout();
            //Действие
            AccountData account = new AccountData("admin", "gdhfg");
            app.Auth.Login(account);
            //Проверка
            Assert.IsFalse(app.Auth.IsLoggedIn(account));
        }





    }
}
