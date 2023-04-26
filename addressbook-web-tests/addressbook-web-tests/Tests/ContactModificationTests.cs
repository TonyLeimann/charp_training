using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
            edit_contact.Mphone = "+ 7 999 234 23 23";
            edit_contact.Nick = "Kipper";
            edit_contact.Middlename = "Sanchez";

            app.Contact.FindContactAnotherCreate();

            List<ContactData> oldContacts = app.Contact.GetContactList();
            ContactData oldData = oldContacts[0];

            app.Contact.ModifyContact(0,edit_contact);

            Assert.AreEqual(oldContacts.Count, app.Contact.GetContactCount());

            List<ContactData> newContacts = app.Contact.GetContactList();
            oldContacts[0].Firstname = edit_contact.Firstname;
            oldContacts[0].Lastname = edit_contact.Lastname;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.ID == oldData.ID)
                {
                    Assert.AreEqual(edit_contact.Lastname, contact.Lastname);// ожидаемый с фактическим\
                    Assert.AreEqual(edit_contact.Firstname, contact.Firstname);
                }

            }

        }



    }
}
