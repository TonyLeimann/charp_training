using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests
{
    [Table(Name = "group_list")]
    public class GroupData:IEquatable<GroupData>,IComparable<GroupData>
    // IEquatable<GroupData> для этих объектов определена функция сравнения, его можно сравнивать с другими объектами типа GroupDate 
    // IComparable<GroupData>  
    {
        public GroupData (string name)
        
        { 
            Name = name;
        }
        public GroupData()

        {

        }

        public bool Equals(GroupData other)// функция сравнения
        {
            if (Object.ReferenceEquals(other, null))// тот объект с которым мы сравниваем это null
            {
            return false;
            }
            if (Object.ReferenceEquals(this, other)) 
            { 
                return true; 
            }
             
            return Name == other.Name;  
        }

        public override int GetHashCode() // сначала сравниваются хэшкоды и если разные, то не равны (оптимизация сравнения)
        {
           // return 0;// если не нужна оптимизация
           return Name.GetHashCode();

        }

        public override string ToString()
        {
            return "name = " + Name + "\nfooter = " + Footer + "\nheader = " + Header;
        }

        public int CompareTo(GroupData other)
        {
            if(Object.ReferenceEquals(other, null)) 
            {
                return 1;
            }
            return Name.CompareTo(other.Name);

        }
        [Column(Name = "group_name"),NotNull]
        public string Name { get; set; }
        [Column(Name = "group_header"),NotNull]
        public string Header { get; set; }
        [Column(Name = "group_footer"), NotNull]
        public string Footer { get; set; }
        [Column(Name = "group_id"),PrimaryKey,Identity]
        public string ID { get; set; }
 
        public static List<GroupData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB()) // установка соединения
            {
                return (from g in db.Groups select g).ToList();
            }
        }

        public List<ContactData> GetContacts() 
        {
            using (AddressBookDB db = new AddressBookDB()) // установка соединения
            {
                return (from contact in db.Contacts
                             from gcr in db.GCR.Where(p => p.GroupID == ID && p.ContactID == contact.ID && contact.Deprecaed == "0000-00-00 00:00:00")
                        select contact).Distinct().ToList();
            }
        }


    }
}
