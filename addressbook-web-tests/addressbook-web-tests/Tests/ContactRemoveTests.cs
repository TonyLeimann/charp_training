using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests.Tests
{
    [TestFixture]
    public class ContactRemoveTests: ContactTestBase
    {

        [Test]

        public void ContactRemoveTest()
        {
            app.Contact.FindContactAnotherCreate();

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData toBeRevoved = oldContacts[0];


            app.Contact.Remove(toBeRevoved);

            Assert.AreEqual(oldContacts.Count - 1, app.Contact.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();


            oldContacts.RemoveAt(0);

            Assert.AreEqual(oldContacts, newContacts);//сравниваем списки

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.ID, toBeRevoved.ID);
            }
        }
    }
}
