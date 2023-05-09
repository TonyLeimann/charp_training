using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;// коллекция
using System.IO;
using NUnit.Framework.Constraints;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;
using LinqToDB;
using System.Linq;

namespace addressbook_web_tests.Tests
{
    [TestFixture]
    public class GroupCreationTests: GroupTestBase
    {
        [Test]
        public void GroupCreationTest()
        {  
            GroupData group = new GroupData("asd");
             group.Header = "asd";
             group.Footer = "asd";

            List<GroupData> oldGroups = app.Groups.GetGroupList();
            
            app.Groups.Create(group);

            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupData(GenerateRandomsString(30))
                {
                    Header = GenerateRandomsString(100),
                    Footer = GenerateRandomsString(100)
                });
            }

            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromCsvFile()
        {
            List<GroupData> groups = new List<GroupData>();
                     

            string[] lines = File.ReadAllLines(@"groups.csv");//массив строк из файла group.csv, тут их читаем
            foreach (string line in lines)
            {
               
                string[] parts = line.Split(',');
                groups.Add(new GroupData(parts[0])
                {
                    Header= parts[1],
                    Footer= parts[2]
                });
            }

            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromXmlFile()
        {
            return (List<GroupData>) // приведение типа
                 new XmlSerializer(typeof(List<GroupData>)) // читает данные типа List<GroupData>
                 .Deserialize(new StreamReader(@"groups.xml")); // из указанного файла

        }

        public static IEnumerable<GroupData> GroupDataFromJsonFile() // метод чтения данных
        {
           return JsonConvert.DeserializeObject<List<GroupData>>(File.ReadAllText(@"groups.json"));
        }

        public static IEnumerable<GroupData> GroupDataFromExcelFile() // метод чтения данных
        {

            List<GroupData> groups = new List<GroupData>();
            Excel.Application app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"groups.xlsx"));
            Excel.Worksheet worksheet = workbook.ActiveSheet;
            Excel.Range range = worksheet.UsedRange;//прямоугольник содержащий данные 
            for (int i = 1; i <= range.Rows.Count; i++)
            {
                groups.Add(new GroupData()
                {
                    Name = range.Cells[i, 1].Value,
                    Header = range.Cells[i, 2].Value,
                    Footer = range.Cells[i, 3].Value
                });
            }
            workbook.Close();
            app.Visible = false;
            app.Quit();
            return groups;
        }

        [Test, TestCaseSource("GroupDataFromJsonFile")]// включили провайдер для чтения данных из файла
        public void GroupCreationTestWithJson(GroupData group)
        {
            List<GroupData> oldGroups = GroupData.GetAll();

            app.Groups.Create(group);

            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void TestDBConnectivity()
        {   
            DateTime now = DateTime.Now;
            List<GroupData> fromUI = app.Groups.GetGroupList();
            DateTime after = DateTime.Now;
            Console.WriteLine("UI: " + after.Subtract(now));

            now = DateTime.Now;
            List<GroupData> fromDB = GroupData.GetAll();                       
            after = DateTime.Now;
            Console.WriteLine("DB: " + after.Subtract(now));
        }

        [Test]
        public void TestDBConnectivity2()
        {
            //foreach (ContactData contact in GroupData.GetAll()[0].GetContacts())
            //{
            //    Console.WriteLine(contact);
            //}

            foreach(ContactData contact in ContactData.GetAll())
            {
                Console.WriteLine(contact.Deprecaed);
            }
        }

    }
}
