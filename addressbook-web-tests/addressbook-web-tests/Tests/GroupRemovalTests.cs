using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tests.Tests
{
    [TestFixture]
    public class GroupRemovalTests: AuthTestBase 
    {
        
        [Test]
        public void GroupRemovalTest()
        {
            app.Groups.FindGroupAnotherCreate();
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Remove(0);

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.RemoveAt(0);// удалить первый элемент в списке
            Assert.AreEqual(oldGroups, newGroups);// сравниваниваем списки
        }
                 


    }
}
