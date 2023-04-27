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
            contact.Mphone = "+7 777 777 77 77";
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
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();

            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomsString(10), GenerateRandomsString(10))
                {
                    Nick = GenerateRandomsString(10),
                    Company = GenerateRandomsString(10),
                    Middlename = GenerateRandomsString(10),
                });
            }
            return contacts;
        }
        [Test, TestCaseSource("RandomContactDataProvider")]
        public void CreatingContactTest(ContactData contact)
        {


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
