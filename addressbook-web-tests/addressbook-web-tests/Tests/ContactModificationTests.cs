using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests.Tests
{
    [TestFixture]
    public class ContactModificationTests: AuthTestBase
    {
        [Test]

        public void ContactModificationTest()
        {

            ContactData edit_contact = new ContactData("Iker", "Casilias");
            edit_contact.Company = "Real Madrid";
            edit_contact.Phone_mobile = "+ 7 999 234 23 23";
            edit_contact.Nick = "Kipper";
            edit_contact.Middlename = "Sanchez";

            app.Contact.FindContactAnotherCreate();
            app.Contact.ModifyContact(2,edit_contact);


           
        }



    }
}
