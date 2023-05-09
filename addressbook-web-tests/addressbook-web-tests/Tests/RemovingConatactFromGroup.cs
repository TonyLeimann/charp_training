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
            app.Contact.FindContactAtGroupAnotherCreate();

            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData toBeRevoved = oldList[0];// удаляем первый контакт в группе

            app.Contact.RemoveContactFromGroup(group, toBeRevoved);

            List<ContactData> newList = group.GetContacts();

            oldList.RemoveAt(0);
            newList.Sort();
            oldList.Sort();
            Assert.AreEqual(oldList, newList);

        }








    }
}
