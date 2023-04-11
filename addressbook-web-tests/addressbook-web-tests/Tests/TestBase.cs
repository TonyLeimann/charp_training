﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace addressbook_web_tests.Tests
{
    public class TestBase
    {
        
        protected ApplicationManager app;    
 
        [OneTimeSetUp]
        public void SetupApplicationManager()
        {
            app = ApplicationManager.GetInstance();

        }

    }
}
