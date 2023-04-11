using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests.Tests
{
    [TestFixture]
    public class ContactRemoveTests: AuthTestBase
    {

        [Test]

        public void ContactRemoveTest()
        {

            app.Contact.Remove(2);

        }








    }
}
