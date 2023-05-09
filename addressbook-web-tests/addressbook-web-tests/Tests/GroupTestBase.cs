using addressbook_web_tests.Tests;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests
{
    
    public class GroupTestBase: AuthTestBase
    {
        [TearDown]  // будет выполняться после каждого тестового метода
        public void CompareGroupsUI_DB()
        {
            if (NEED_LONG_UI_CHECK)
            {
                List<GroupData> fromUI = app.Groups.GetGroupList();
                List<GroupData> fromDB = GroupData.GetAll();

                fromUI.Sort();
                fromDB.Sort();

                Assert.AreEqual(fromUI, fromDB);
            }
   
        }
    }
}
