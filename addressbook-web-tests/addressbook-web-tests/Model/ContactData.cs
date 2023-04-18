using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests
{
    public class ContactData:IEquatable<ContactData>,IComparable<ContactData>   
    {
        private string firstname;
        private string middlename = "";
        private string lastname;
        private string nickname = "";
        private string company = "";
        private string phone_mobile = "";


        public ContactData(string firstname, string lastname)
        {
            this.firstname = firstname;
            this.lastname = lastname;
        }


        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; }

        }
        public string Middlename
        {
            get { return middlename; }
            set { middlename = value; }

        }

        public string Lastname
        {
            get { return lastname; }
            set { lastname = value; }

        }

        public string Company
        {
            get { return company; }
            set { company = value; }

        }
        public string Phone_mobile
        {
            get { return phone_mobile; }
            set { phone_mobile = value; }


        }

        public string Nick
        {
            get { return nickname; }
            set { nickname = value; }
        }

        public int CompareTo(ContactData other)
        {
            if(Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Firstname.CompareTo(other.Firstname) + Lastname.CompareTo(other.Lastname);    
                
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
            return Firstname + " " + Lastname;
        }
    }
}
