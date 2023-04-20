using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests.Tests
{
    [TestFixture]
    public class GroupModificationTests: AuthTestBase
    {

        [Test]

        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("text");
            newData.Header = "text";
            newData.Footer = "text";

            app.Groups.FindGroupAnotherCreate();

            List<GroupData> oldGroups = app.Groups.GetGroupList();
            GroupData oldData = oldGroups[0];

            app.Groups.GroupModify(0,newData);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
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
