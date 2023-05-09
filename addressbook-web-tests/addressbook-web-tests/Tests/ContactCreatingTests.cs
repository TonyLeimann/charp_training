using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;

namespace addressbook_web_tests.Tests
{
    [TestFixture]
    public class ContactCreatingTests: ContactTestBase
    {
       
        [Test]
        public void CreatingContactTest()
        {    
            ContactData contact = new ContactData("Raul","Gonzales");
            contact.Company = "Spain";
            contact.Mphone = "+7 777 777 77 77";
            contact.Nick = "Madridista";
            contact.Middlename = "Blanco";

            List<ContactData> oldContacts = ContactData.GetAll();

            app.Contact.Create(contact);

           
            Assert.AreEqual(oldContacts.Count + 1, app.Contact.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

        }
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();

            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomsString(10), GenerateRandomsString(10))
                {
                    Nick = GenerateRandomsString(10),
                    Company = GenerateRandomsString(10),
                    Middlename = GenerateRandomsString(10),
                });
            }
            return contacts;
        }

        public static IEnumerable<ContactData> ContactDataFromCsvFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            string[] contactData = File.ReadAllLines(@"contacts.csv");// прочитать все строчки из файла и вернуть массив строк
            foreach (string line in contactData)
            {
               string[] parts = line.Split(',');// разбиваем на куски
                contacts.Add(new ContactData(parts[0], parts[1])
                {
                    Middlename = parts[2],
                    Nick = parts[3],
                    Company = parts[4]
                      
                });
            }
            return contacts;
        }

        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            List<ContactData> contacts = new List<ContactData>();

            return (List<ContactData>)
              new XmlSerializer(typeof(List<ContactData>))
              .Deserialize(new StreamReader(@"contacts.xml"));

        }

        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(File.ReadAllText(@"contacts.json"));

        }
        public static IEnumerable<ContactData> ContactDataFromExcelFile() // метод чтения данных
        {

            List<ContactData> contacts = new List<ContactData>();
            Excel.Application app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"contacts.xlsx"));
            Excel.Worksheet worksheet = workbook.ActiveSheet;
            Excel.Range range = worksheet.UsedRange;//прямоугольник содержащий данные 
            for (int i = 1; i <= range.Rows.Count; i++)
            {
                contacts.Add(new ContactData()
                {
                    Firstname = range.Cells[i, 1].Value,
                    Middlename = range.Cells[i, 2].Value,
                    Lastname = range.Cells[i, 3].Value,
                    Nick = range.Cells[i, 4].Value,
                    Company = range.Cells[i, 5].Value
                });
            }
            workbook.Close();
            app.Visible = false;
            app.Quit();
            return contacts;

        }


        [Test, TestCaseSource("ContactDataFromJsonFile")]
        public void CreatingContactTestWithJsonFile(ContactData contact)
        {
            List<ContactData> oldContacts = ContactData.GetAll();
            app.Contact.Create(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contact.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]

        public void TestDBConnectivity()
        {
            DateTime now = DateTime.Now;
            List<ContactData> fromUI = app.Contact.GetContactList();
            DateTime after = DateTime.Now;
            Console.WriteLine("UI: " + after.Subtract(now));

            now = DateTime.Now;
            List<ContactData> fromDB = ContactData.GetAll();
            after = DateTime.Now;
            Console.WriteLine("DB: " + after.Subtract(now));
        }






    }
}
