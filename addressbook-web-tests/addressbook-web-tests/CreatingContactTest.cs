using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tests
{
    [TestFixture]
    public class CreatingContactTests: TestBase// наследник TestBase
    {
       

        [Test]
        public void CreatingContactTest()
        {
            OpenHomePage();
            Login(new AccountData ("admin", "secret"));
            AddContact();
            ContactData contact = new ContactData("Anton","Simakhin");
            contact.Company = "Google";
            contact.Phone_mobile = "88005553535";
            contact.Nick = "sima";
            contact.Middlename = "Andrey";
            FillContactForm(contact);
            EnterContact();
            ReturnToHomePage();
            InitLogout();
        }
        
      
    }
}
