﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tests
{
    [TestFixture]
    public class GroupCreationTests:TestBase// наследник TestBase
    {
       

        [Test]
        public void GroupCreationTest()
        {
            OpenHomePage();
            Login(new AccountData("admin","secret"));
            GoToGroupsPage();
            InitGroupsCreation();
            GroupData group = new GroupData("sd");
           // group.Header = "das";
           // group.Footer = "weq";
            FillGroupsForm(group);
            SubmitGroupCreation();
            ReturnToGroupPage();
            InitLogout();
        }

    }
}
