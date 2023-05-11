using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests.Tests
{
    public class RemovingConatactFromGroup : AuthTestBase
    {
        [Test]

        public void TestRemovingConatactFromGroup()


        {
            app.Groups.FindSomeGroup();
            app.Contact.FindSomeContact();

            GroupData foundGroup =  app.Groups.FindGroupWithContact();
            ContactData toBeRevoved = new ContactData();

            if (foundGroup == null)
            {
                toBeRevoved = ContactData.GetAll()[0];
                foundGroup = GroupData.GetAll()[0];
                app.Contact.AddContactToGroup(toBeRevoved, foundGroup);
            }

            List<ContactData> oldList = foundGroup.GetContacts();
            toBeRevoved = oldList[0];

            app.Contact.RemoveContactFromGroup(foundGroup, toBeRevoved);
            List<ContactData> newList = foundGroup.GetContacts();

            oldList.RemoveAt(0);
            newList.Sort();
            oldList.Sort();
            Assert.AreEqual(oldList, newList);

        }

    }
}
