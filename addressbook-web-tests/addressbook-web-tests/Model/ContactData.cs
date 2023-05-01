using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace addressbook_web_tests
{
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

        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string Company { get; set; }
        public string Nick { get; set; }
        public string ID { get; set; }
        public string Mphone { get; set; }
        public string Hphone { get;  set; }
        public string Wphone { get;  set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string Address { get;  set; }
        public string AllPhones
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
            return "firstname = " + Firstname + "\nmiddlename = " + Middlename + "\nlastname = " + Lastname;
        }
    }
}
