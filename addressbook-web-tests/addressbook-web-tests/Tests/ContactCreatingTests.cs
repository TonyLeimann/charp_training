using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace addressbook_web_tests.Tests
{
    [TestFixture]
    public class ContactCreatingTests: AuthTestBase
    {
       

        [Test]
        public void CreatingContactTest()
        {    
            ContactData contact = new ContactData("Raul","Gonzales");
            contact.Company = "Spain";
            contact.Phone_mobile = "+7 777 777 77 77";
            contact.Nick = "Madridista";
            contact.Middlename = "Blanco";

            List<ContactData> oldContacts = app.Contact.GetContactList();

            app.Contact.Create(contact);

           
            Assert.AreEqual(oldContacts.Count + 1, app.Contact.GetContactCount());

            List<ContactData> newContacts = app.Contact.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

        }


        [Test]
        public void EmptyCreatingContactTest()
        {           
            ContactData contact = new ContactData("","");


            List<ContactData> oldContacts = app.Contact.GetContactList();

            app.Contact.Create(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contact.GetContactCount());

            List<ContactData> newContacts = app.Contact.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }





    }
}
