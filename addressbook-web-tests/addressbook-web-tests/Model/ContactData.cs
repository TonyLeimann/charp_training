using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests
{
    public class ContactData:IEquatable<ContactData>,IComparable<ContactData>   
    {

        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }


        public string Firstname { get; set; }

        public string Middlename { get; set; }

        public string Lastname { get; set; }

        public string Company { get; set; }

        public string Phone_mobile { get; set; }

        public string Nick { get; set; }

        public string ID { get; set; }

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
            return Firstname + " " + Lastname;
        }
    }
}
