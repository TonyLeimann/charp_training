using LinqToDB.Mapping;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests
{
    [Table(Name = "address_in_groups")]
    public class GroupContactRelation
    {
        [Column(Name = "group_id"), PrimaryKey]
        public string GroupID { get; set; }
        [Column(Name = "id"), PrimaryKey]
        public string ContactID { get; set; }   
    }
}
