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
    public class GroupRemovalTests: GroupTestBase
    {
        
        [Test]
        public void GroupRemovalTest()
        {
            app.Groups.FindGroupAnotherCreate();
            // List<GroupData> oldGroups = app.Groups.GetGroupList();
            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData toBeRemoved = oldGroups[0];


            //app.Groups.Remove(0);
            app.Groups.Remove(toBeRemoved);

            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());

            //List<GroupData> newGroups = app.Groups.GetGroupList();
            List<GroupData> newGroups = GroupData.GetAll();

           

            oldGroups.RemoveAt(0);// удалить первый элемент в списке
            Assert.AreEqual(oldGroups, newGroups);// сравниваниваем списки

            foreach (GroupData group in newGroups)
            {

                Assert.AreNotEqual(group.ID, toBeRemoved.ID);
            }
        }
                 


    }
}
