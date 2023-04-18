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

            app.Groups.GroupModify(0,newData);

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }



    }
}
