using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests.Tests
{
    [TestFixture]
    public class ContactInformationTests: AuthTestBase
    {
        [Test]
        public void ContactInformationTest()
        {
            app.Navigator.GoToHomePage();
            
            ContactData fromTable = app.Contact.GetContactInformationFromTable(0);
            ContactData fromEditForm = app.Contact.GetContactInformationFromEditForm(0);
            string fromDetailsInfo = app.Contact.GetInfoDetails(0);

            Assert.AreEqual(fromTable, fromEditForm);
            Assert.AreEqual(fromTable.Address, fromEditForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromEditForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromEditForm.AllEmails);
            Assert.AreEqual(fromDetailsInfo, fromEditForm.InfoAboutDetail);
               
            Console.WriteLine(fromEditForm.InfoAboutDetail);   
            Console.WriteLine("--------------------------------");
            Console.WriteLine(fromDetailsInfo);
            Console.WriteLine("--------------------------------");
            Console.WriteLine(app.Contact.GetNumberOfSearchResults());
        }
    }
}
