using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests.Tests
{
    [TestFixture]
    public class GroupModificationTests: GroupTestBase
    {

        [Test]

        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("text");
            newData.Header = "text";
            newData.Footer = "text";

            app.Groups.FindGroupAnotherCreate();

            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData oldData = oldGroups[0];

            app.Groups.GroupModify(oldData, newData);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in oldGroups) 
            {
                if(group.ID == oldData.ID)
                {
                    Assert.AreEqual(newData.Name, group.Name);// ожидаемый с фактическим
                }

            }

        }



    }
}
