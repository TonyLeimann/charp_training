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
    public class ContactRemoveTests: AuthTestBase
    {

        [Test]

        public void ContactRemoveTest()
        {
            app.Contact.FindContactAnotherCreate();

            List<ContactData> oldContacts = app.Contact.GetContactList();

            app.Contact.Remove(0);

            Assert.AreEqual(oldContacts.Count - 1, app.Contact.GetContactCount());

            List<ContactData> newContacts = app.Contact.GetContactList();

            ContactData toBeRevoved = oldContacts[0];

            oldContacts.RemoveAt(0);

            Assert.AreEqual(oldContacts, newContacts);//сравниваем списки

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.ID, toBeRevoved.ID);
            }
        }
    }
}
