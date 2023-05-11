using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;



namespace addressbook_web_tests.Tests
{
    public class AddingContactToGroupTests: AuthTestBase
    {
        [Test]

        public void TestAddingConatactToGroup()

        {
            app.Groups.FindSomeGroup();
            app.Contact.FindSomeContact();

            GroupData someGroup = app.Contact.FindingSpecificPair();

            if(someGroup == null) 
            {
              someGroup = app.Contact.CheckAvailabilityGroup(someGroup);
            }

            List<ContactData> oldList = someGroup.GetContacts();
            ContactData contact = ContactData.GetAll().Except(oldList).First();

            app.Contact.AddContactToGroup(contact, someGroup);

            List<ContactData> newList = someGroup.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();
            Assert.AreEqual(oldList, newList);

        }

    }
}
