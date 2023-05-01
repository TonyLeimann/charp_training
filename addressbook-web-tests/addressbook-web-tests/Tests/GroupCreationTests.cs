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

namespace addressbook_web_tests.Tests
{
    [TestFixture]
    public class GroupCreationTests: AuthTestBase// наследник AuthTestBase
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

        //[Test, TestCaseSource("RandomGroupDataProvider")]
        //public void Random(GroupData group)
        //{
        //    List<GroupData> oldGroups = app.Groups.GetGroupList();

        //    app.Groups.Create(group);

        //    Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

        //    List<GroupData> newGroups = app.Groups.GetGroupList();
        //    oldGroups.Add(group);
        //    oldGroups.Sort();
        //    newGroups.Sort();
        //    Assert.AreEqual(oldGroups, newGroups);
        //}

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


        [Test, TestCaseSource("GroupDataFromCsvFile")]
        public void GroupCreationTestWithCsv(GroupData group)
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Create(group);

            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        public static IEnumerable<GroupData> GroupDataFromXmlFile()
        {

            return (List<GroupData>) // приведение типа
                 new XmlSerializer(typeof(List<GroupData>)) // читает данные типа List<GroupData>
                 .Deserialize(new StreamReader(@"groups.xml")); // из указанного файла


        }

        [Test, TestCaseSource("GroupDataFromXmlFile")]
        public void GroupCreationTestWithXml(GroupData group)
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Create(group);

            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        public static IEnumerable<GroupData> GroupDataFromJsonFile() // метод чтения данных
        {

           return JsonConvert.DeserializeObject<List<GroupData>>(File.ReadAllText(@"groups.json"));


        }

        [Test, TestCaseSource("GroupDataFromJsonFile")]// включили провайдер для чтения данных из файла
        public void GroupCreationTestWithJson(GroupData group)
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Create(group);

            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }







    }
}
