using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests
{
    public class HelperBase
    {
        public IWebDriver driver;
        public ApplicationManager manager;

        public HelperBase(ApplicationManager manager)
        { 
            this.manager = manager;
            driver = manager.Driver;
        
        }






    }
}
