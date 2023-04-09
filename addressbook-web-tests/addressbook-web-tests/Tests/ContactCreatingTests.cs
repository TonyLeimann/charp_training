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
    public class ContactCreatingTests: TestBase// наследник TestBase
    {
       

        [Test]
        public void CreatingContactTest()
        {
            
            
            ContactData contact = new ContactData("Anton","Simakhin");
            contact.Company = "Google";
            contact.Phone_mobile = "88005553535";
            contact.Nick = "sima";
            contact.Middlename = "Andrey";


            app.Contact.Create(contact);

            
            app.Navigator.InitLogout();
        }


        [Test]
        public void EmptyCreatingContactTest()
        {

            
            ContactData contact = new ContactData("","");
            contact.Company = "";
            contact.Phone_mobile = "";
            contact.Nick = "";
            contact.Middlename = "";


            app.Contact.Create(contact);

            
            app.Navigator.InitLogout();
        }





    }
}
