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
            app.Groups.GroupModify(1,newData);

        }



    }
}
