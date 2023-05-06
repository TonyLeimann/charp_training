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
using Excel = Microsoft.Office.Interop.Excel;

namespace addressbook_test_data_generators
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string dataType = args[0];
            int count = Convert.ToInt32(args[1]); // колличество тестовых данных, которые будем генерировать
            string filename = args[2];
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
                if(format == "excel")
                {
                    writeGroupsToExcelFile(groups, filename);
                }
                else
                {
                    StreamWriter writer = new StreamWriter(filename);

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
                if (format == "excel")
                {
                    writeContactsToExcelFile(contacts, filename);
                }
                else 
                {
                    StreamWriter writer = new StreamWriter(filename);// уже отрытый StreamWriter

                    if (format == "csv")
                    {
                        writeContactsToCsvFile(contacts, writer);
                    }
                    else if (format == "xml")
                    {
                        writeContactsToXmlFile(contacts, writer);
                    }
                    else if (format == "json")
                    {
                        writeContactsToJsonFile(contacts, writer);
                    }
                    else
                    {
                        Console.WriteLine("Непонятный формат " + format);
                    }
                    writer.Close();
                }
            }
            else 
            {
                Console.WriteLine("Непонятный тип данных " + dataType);
            }           
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

        static void writeGroupsToExcelFile(List<GroupData> groups, string filename)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true; // запуск Excel
            Excel.Workbook wb = app.Workbooks.Add(); // создание нового документа
            Excel.Worksheet sheet = wb.ActiveSheet;// страница Excel

            int row = 1;
            foreach (GroupData group in groups)
            {
                sheet.Cells[row, 1] = group.Name;
                sheet.Cells[row, 2] = group.Header;
                sheet.Cells[row, 3] = group.Footer;// вписываем текст на странице
                row++;
            }
            string fullpath = Path.Combine(Directory.GetCurrentDirectory(), filename);// путь куда сохранять
            File.Delete(fullpath);
            wb.SaveAs(fullpath);

            wb.Close();
            app.Visible = false;
            app.Quit();
        }

        static void writeContactsToExcelFile(List<ContactData> contacts, string filename)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true; // запуск Excel
            Excel.Workbook wb = app.Workbooks.Add(); // создание нового документа
            Excel.Worksheet sheet = wb.ActiveSheet;// страница Excel

            int row = 1;
            foreach (ContactData contact in contacts)
            {
                sheet.Cells[row, 1] = contact.Firstname;// вписываем текст на странице
                sheet.Cells[row, 2] = contact.Middlename;
                sheet.Cells[row, 3] = contact.Lastname;// вписываем текст на странице
                sheet.Cells[row, 4] = contact.Nick;
                sheet.Cells[row, 5] = contact.Company;
                row++;
            }
            string fullpath = Path.Combine(Directory.GetCurrentDirectory(), filename);// путь куда сохранять
            File.Delete(fullpath);
            wb.SaveAs(fullpath);

            wb.Close();
            app.Visible = false;
            app.Quit();
        }
    }
}
