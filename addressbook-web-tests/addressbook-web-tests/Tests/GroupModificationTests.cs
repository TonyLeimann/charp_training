using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests.Tests
{
    [TestFixture]
    public class GroupModificationTests:TestBase
    {

        [Test]

        public void GroupModificationTest()
        {

            GroupData newData = new GroupData("qqq");
            newData.Header = "www";
            newData.Footer = "ee";


            app.Groups.Modify(3,newData);


        }



    }
}
