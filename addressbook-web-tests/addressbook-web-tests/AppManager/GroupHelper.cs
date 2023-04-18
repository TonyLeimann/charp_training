using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace addressbook_web_tests
{
    public class GroupHelper:HelperBase

    {
          

        public GroupHelper(ApplicationManager manager):base(manager) { }
       
        public GroupHelper Create(GroupData group)
        {
            manager.Navigator.GoToGroupsPage();
            InitGroupsCreation();
            FillGroupsForm(group);
            SubmitGroupCreation();
            ReturnToGroupPage();
            return this;
        }

        public GroupHelper Remove(int index)
        {

            manager.Navigator.GoToGroupsPage();
            SelectGroup(index);
            DeleteGroup();
            ReturnToGroupPage();
            return this;
        }

        public GroupHelper GroupModify(int groupLine, GroupData group_mod)
        {
            manager.navigator.GoToGroupsPage();
            SelectGroup(groupLine);
            InitGroupModification();
            FillGroupsForm(group_mod);
            SubmitGroupModification();
            ReturnToGroupPage();

            return this;
        }


        public GroupHelper FindGroupAnotherCreate()// для случия, когда нет группы для удаления
        {
            manager.Navigator.GoToGroupsPage();

            if (!IsElementPresent(By.Name("selected[]")))
            {
                GroupData newGroup = new GroupData("NewForCorrectTest");   
                              
                Create(newGroup);
            }
            return this;

        }

        public GroupHelper InitGroupsCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }
        public GroupHelper FillGroupsForm(GroupData group)
        {  
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);
            return this;
        }

        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }



        public GroupHelper ReturnToGroupPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
            return this;
        }

        /// методы для выбора группы и удаления

        public GroupHelper SelectGroup(int index)
        {
 
            driver.FindElement(By.XPath("//div[@id='content']/form/span[" + index + 1 + "]/input")).Click();// xPath нумерация с единицы, поэтому привели к общему виду в C# + 1

            return this;
        }



        public GroupHelper DeleteGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            return this;

        }
              

        public GroupHelper InitGroupModification()
        {

            driver.FindElement(By.Name("edit")).Click();
            return this;
        }

        public GroupHelper SubmitGroupModification()
        {


            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public List<GroupData> GetGroupList()
        {
            List<GroupData> groups = new List<GroupData>();
            manager.Navigator.GoToGroupsPage();
            ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));
            foreach (IWebElement element in elements) 
            {
                groups.Add(new GroupData(element.Text));
            }
            return groups;

        }
    }
}
