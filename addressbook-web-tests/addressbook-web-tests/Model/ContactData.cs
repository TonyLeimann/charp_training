using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Newtonsoft.Json;
using LinqToDB.Mapping;

namespace addressbook_web_tests
{
    [Table(Name = "addressbook")]
    public class ContactData:IEquatable<ContactData>,IComparable<ContactData>   
    {
        

        private string allEmails;
        private string allPhones;
        private string infoAboutDetail;

        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        public ContactData()
        {
           
        }
        [Column(Name = "firstname"), NotNull]
        public string Firstname { get; set; }
        [Column(Name = "middlename"), NotNull]
        public string Middlename { get; set; }
        [Column(Name = "lastname"), NotNull]
        public string Lastname { get; set; }
        [Column(Name = "company"), NotNull]
        public string Company { get; set; }
        [Column(Name = "nickname"), NotNull]
        public string Nick { get; set; }
        [Column(Name = "id"), PrimaryKey, Identity]
        public string ID { get; set; }
        [Column(Name = "mobile"), NotNull]
        public string Mphone { get; set; }
        [Column(Name = "home")]
        public string Hphone { get;  set; }
        [Column(Name = "work"), NotNull]
        public string Wphone { get;  set; }
        [Column(Name = "email"), NotNull]
        public string Email { get; set; }
        [Column(Name = "email2"), NotNull]
        public string Email2 { get; set; }
        [Column(Name = "email3"), NotNull]
        public string Email3 { get; set; }
        [Column(Name = "deprecated")]
        public string Deprecaed { get; set; }   
        [Column(Name = "address"), NotNull]
        public string Address { get;  set; }
        [XmlIgnore]//The XmlSerializer ignores this field.
        //[JsonIgnore] 

        public string  AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(Hphone) + CleanUp(Mphone) + CleanUp(Wphone)).Trim();
                }
            }
            set 
            { 
                allPhones = value;
            }
        }
        public string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ -()]", "") + "\r\n";
        }
        [XmlIgnore] // The XmlSerializer ignores this field.
        //[JsonIgnore]
        public string AllEmails
        {
            get
            {
                if(allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (CleanEmail(Email) + CleanEmail(Email2) + CleanEmail(Email3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }
        public string CleanEmail(string mail)
        {
            if (mail == null || mail == "")
            {
                return "";
            }
            return mail.Replace(" ", "") + "\r\n";
        }
        public string InfoAboutDetail
        {
            get
            {
                if (infoAboutDetail != null)
                {
                    return infoAboutDetail;
                }
                else
                {
                    return CheckDetails();
                }                   
            }
            set
            {
                infoAboutDetail = value;
            }
        }
        public string CheckDetails()
        {
            if ((AllPhones == null || AllPhones == "") && (AllEmails == null || AllEmails == ""))
            {
                return (DataСhecking("", Firstname) + DataСhecking(" ", Middlename) + DataСhecking(" ", Lastname)
                   + DataСhecking("\r\n", Nick) + DataСhecking("\r\n", Company)).Trim();
            }
            else if ((AllEmails == null || AllEmails == ""))
            {
                return (DataСhecking("", Firstname) + DataСhecking(" ", Middlename) + DataСhecking(" ", Lastname)
                   + DataСhecking("\r\n", Nick) + DataСhecking("\r\n", Company) + "\r\n" + DataСhecking("\r\nH: ", Hphone) +
                   DataСhecking("\r\nM: ", Mphone) + DataСhecking("\r\nW: ", Wphone)).Trim();
            }
            else if ((AllPhones == null || AllPhones == ""))
            {
                return DataСhecking("", Firstname) + DataСhecking(" ", Middlename) + DataСhecking(" ", Lastname)
                   + DataСhecking("\r\n", Nick) + DataСhecking("\r\n", Company) + "\r\n" + DataСhecking("\r\n", Email) +
                   DataСhecking("\r\n", Email2) + DataСhecking("\r\n", Email3);
            }
            else
            {
                return DataСhecking("", Firstname) + DataСhecking(" ", Middlename) + DataСhecking(" ", Lastname)
                   + DataСhecking("\r\n", Nick) + DataСhecking("\r\n", Company) + "\r\n" + DataСhecking("\r\nH: ", Hphone) +
                   DataСhecking("\r\nM: ", Mphone) + DataСhecking("\r\nW: ", Wphone) + "\r\n" + DataСhecking("\r\n", Email) +
                   DataСhecking("\r\n", Email2) + DataСhecking("\r\n", Email3);
            }
        }
        public string DataСhecking(string literal,string data)
        {
            if (data is null || data == "")
            {
                return "";
            }
            else
            {
                return literal + data;
            }
        }
        public int CompareTo(ContactData other)
        {
            if (other == null)
            {
                return 1;
            }
            if (Lastname == other.Lastname)
            {
                return Firstname.CompareTo(other.Firstname);
            }
            return Lastname.CompareTo(other.Lastname);
        }
         public bool Equals(ContactData other)
         {
            if (ReferenceEquals(other, null)) 
            {
                return false;
            }
            if (ReferenceEquals(this, other)) 
            { 
                return true;    
            }
            return Lastname  == other.Lastname && Firstname == other.Firstname;            
         }

        public override int GetHashCode()
        {
            return Lastname.GetHashCode() + Firstname.GetHashCode();
        }

        public override string ToString()
        {
            return "firstname = " + Firstname + "\n middlename = " + Middlename + "\n lastname = " + Lastname;
        }

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB()) // установка соединения
            {
                return (from contact in db.Contacts.Where(x => x.Deprecaed == "0000-00-00 00:00:00") select contact).ToList();
            }
        }


    }
}
