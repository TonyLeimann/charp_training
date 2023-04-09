using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tests.Tests
{
    [TestFixture]
    public class GroupCreationTests:TestBase// наследник TestBase
    {
       

        [Test]
        public void GroupCreationTest()
        {

            
            GroupData group = new GroupData("sd");
             group.Header = "das";
             group.Footer = "weq";

            app.Groups.Create(group);

            app.Navigator.InitLogout();
        }

        [Test]
        public void EmptyGroupCreationTest()
        {

            
            GroupData group = new GroupData("");
             group.Header = "";
             group.Footer = "";

            app.Groups.Create(group);
            
            app.Navigator.InitLogout();
        }



    }
}
