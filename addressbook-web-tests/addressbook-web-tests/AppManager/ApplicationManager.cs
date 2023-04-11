using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace addressbook_web_tests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;

        public LoginHelper loginHelper;
        public NavigationHelper navigator;
        public GroupHelper groupHelper;
        public ContactHelper contactHelper;

        private static ThreadLocal <  ApplicationManager > app = new ThreadLocal<ApplicationManager>();

        public IWebDriver Driver { get { return driver; } } 


        public ApplicationManager()
        {
            driver = new FirefoxDriver();
            baseURL = "http://localhost/addressbook/";
           
            loginHelper = new LoginHelper(this);
            navigator = new NavigationHelper(this,baseURL);
            groupHelper = new GroupHelper(this);

            contactHelper = new ContactHelper(this);
        }


         ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {

            }

        }

        public static ApplicationManager GetInstance()// global
        {
            if (! app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.Navigator.OpenHomePage();
                app.Value = newInstance;
            }
            return app.Value;
        }


        public LoginHelper Auth { get { return loginHelper; } }
        public NavigationHelper Navigator { get { return navigator; } }
        public GroupHelper Groups { get { return groupHelper; } }
        public ContactHelper Contact { get { return contactHelper; } }





    }
}
