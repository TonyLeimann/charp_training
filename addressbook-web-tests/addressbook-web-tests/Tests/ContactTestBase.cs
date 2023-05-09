using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using addressbook_web_tests.Tests;
using NUnit.Framework;

namespace addressbook_web_tests
{
    public class ContactTestBase : AuthTestBase
    {
        [TearDown]

        public void CompareContactsUI_DB()
        {
            if (NEED_LONG_UI_CHECK)
            {
                List<ContactData> fromUI = app.Contact.GetContactList();
                List<ContactData> fromDB = ContactData.GetAll();

                fromUI.Sort();
                fromDB.Sort();

                Assert.AreEqual(fromUI, fromDB);
            }

        }


    }
}
