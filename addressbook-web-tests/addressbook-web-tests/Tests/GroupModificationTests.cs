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

            GroupData group = new GroupData("qwerty");
            group.Header = "qwerty";
            group.Footer = "qwerty";


            GroupData newData = new GroupData("text");
            newData.Header = "text";
            newData.Footer = "text";


            app.Groups.Modify(1,newData,group);


        }



    }
}
