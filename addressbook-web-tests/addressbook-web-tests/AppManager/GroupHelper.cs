using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public GroupHelper Remove(int Number,GroupData group)
        {

            manager.Navigator.GoToGroupsPage();
            SelectGroup(Number,group);
            DeleteGroup();
            ReturnToGroupPage();
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

        public GroupHelper SelectGroup(int index,GroupData group)
        {
            if (! IsElementPresent(By.Name("selected[]")))
            {
                Create(group);
            }
            driver.FindElement(By.XPath("//div[@id='content']/form/span[" + index + "]/input")).Click();

            return this;
        }



        public GroupHelper DeleteGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            return this;

        }

        public GroupHelper Modify(int LineGroup, GroupData group, GroupData newData)
        {
            manager.navigator.GoToGroupsPage();
            SelectGroup(LineGroup,group);
            InitGroupModification();
            FillGroupsForm(newData);
            SubmitGroupModification();
            ReturnToGroupPage();

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

    }
}
