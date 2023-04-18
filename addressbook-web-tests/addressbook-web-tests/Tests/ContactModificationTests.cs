using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests.Tests
{
    [TestFixture]
    public class ContactModificationTests: AuthTestBase
    {
        [Test]

        public void ContactModificationTest()
        {

            ContactData edit_contact = new ContactData("Iker", "Casilias");
            edit_contact.Company = "Real Madrid";
            edit_contact.Phone_mobile = "+ 7 999 234 23 23";
            edit_contact.Nick = "Kipper";
            edit_contact.Middlename = "Sanchez";

            app.Contact.FindContactAnotherCreate();

            List<ContactData> oldContacts = app.Contact.GetContactList();

            app.Contact.ModifyContact(0,edit_contact);

            List<ContactData> newContacts = app.Contact.GetContactList();
            oldContacts[0].Firstname = edit_contact.Firstname;
            oldContacts[0].Lastname = edit_contact.Lastname;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

        }



    }
}
