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
    public class ContactCreatingTests: AuthTestBase// наследник TestBase
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
                     
          
        }


        [Test]
        public void EmptyCreatingContactTest()
        {

            
            ContactData contact = new ContactData("",null);
            contact.Company = null;
            contact.Phone_mobile = null;
            contact.Nick = null;
            contact.Middlename = null;


            app.Contact.Create(contact);

        }





    }
}
