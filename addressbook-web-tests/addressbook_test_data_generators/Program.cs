using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using addressbook_web_tests;
using addressbook_web_tests.Tests;
using System.Diagnostics.Eventing.Reader;

namespace addressbook_test_data_generators
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string dataType = args[0];
            int count = Convert.ToInt32(args[1]); // колличество тестовых данных, которые будем генерировать
            StreamWriter writer = new StreamWriter(args[2]);
            string format = args[3];

            if (dataType == "group") 
            {
                List<GroupData> groups = new List<GroupData>();

                for (int i = 0; i < count; i++)
                {
                    groups.Add(new GroupData(TestBase.GenerateRandomsString(10))
                    {
                        Header = TestBase.GenerateRandomsString(10),
                        Footer = TestBase.GenerateRandomsString(10)
                    });
                }

                if (format == "csv")
                {
                    writeGroupsToCsvFile(groups, writer);
                }
                else if (format == "xml")
                {
                    writeGroupsToXmlFile(groups, writer);
                }
                else if (format == "json")
                {
                    writeGroupsToJsonFile(groups, writer);
                }
                else
                {
                    Console.WriteLine("Непонятный формат " + format);
                }

                writer.Close();

            }
            else if(dataType == "contact") 
            {
                List<ContactData> contacts = new List<ContactData>();

                for (int i = 0; i < count; i++)
                {
                    contacts.Add(new ContactData(TestBase.GenerateRandomsString(10), TestBase.GenerateRandomsString(10)){

                        Middlename = TestBase.GenerateRandomsString(10),
                        Nick = TestBase.GenerateRandomsString(10),
                        Company = TestBase.GenerateRandomsString(10)
                    });
                }

                if (format == "csv")
                {
                    writeContactsToCsvFile(contacts, writer);
                }
                else if(format == "xml") 
                {
                    writeContactsToXmlFile(contacts, writer);
                }
                else if(format == "json")
                {
                    writeContactsToJsonFile(contacts, writer);
                }
                else
                {
                    Console.WriteLine("Непонятный формат " + format);
                }
            }
            else 
            {
                Console.WriteLine("Непонятный тип данных " + format);
            }
            writer.Close();

        }
  
        static void writeGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    group.Name,group.Header, group.Footer));
            }
        }
        static void writeGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);  
        }

        static void writeGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups,Newtonsoft.Json.Formatting.Indented));
        }

        static void writeContactsToCsvFile(List<ContactData> contacts, StreamWriter writer)
        {
            foreach(ContactData contact in contacts) 
            {
                writer.WriteLine(String.Format("${0},${1},${2},${3},${4}",
                    contact.Firstname, contact.Lastname, contact.Middlename, contact.Nick, contact.Company));
            }
        }

        static void writeContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {

          new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);

        }
        static void writeContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }
    }
}
