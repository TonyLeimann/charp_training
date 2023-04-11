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

            ContactData edit_contact = new ContactData("Anton", "Simakhin");
            edit_contact.Company = "Google";
            edit_contact.Phone_mobile = "88005553535";
            edit_contact.Nick = "sima";
            edit_contact.Middlename = "Andrey";

            ContactData contact = new ContactData("Billy","Heringgton");


            app.Contact.ModifyContact(2,contact,edit_contact);


           
        }



    }
}
