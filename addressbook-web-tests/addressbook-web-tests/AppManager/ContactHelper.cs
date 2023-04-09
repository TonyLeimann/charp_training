using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace addressbook_web_tests
{
    public class ContactHelper:HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager) { }

        public ContactHelper Create(ContactData contact)
        {
            AddContact();
            FillContactForm(contact);
            EnterContact();
            manager.Navigator.ReturnToHomePage();
            return this;
        }
              
      

        public ContactHelper AddContact()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            driver.FindElement(By.Name("firstname")).Click();
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys(contact.Firstname);
            driver.FindElement(By.Name("middlename")).Click();
            driver.FindElement(By.Name("middlename")).Clear();
            driver.FindElement(By.Name("middlename")).SendKeys(contact.Middlename);
            driver.FindElement(By.Name("lastname")).Click();
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(contact.Lastname);
            driver.FindElement(By.Name("nickname")).Click();
            driver.FindElement(By.Name("nickname")).Clear();
            driver.FindElement(By.Name("nickname")).SendKeys(contact.Nick);
            driver.FindElement(By.Name("company")).Click();
            driver.FindElement(By.Name("company")).Clear();
            driver.FindElement(By.Name("company")).SendKeys(contact.Company);
            driver.FindElement(By.Name("mobile")).Click();
            driver.FindElement(By.Name("mobile")).Clear();
            driver.FindElement(By.Name("mobile")).SendKeys(contact.Phone_mobile);
            return this;
        }

        public ContactHelper EnterContact()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[21]")).Click();
            return this;
        }

        public ContactHelper ModifyContact(int tr, ContactData edit_contact)
        {
            GoToHomePage();
            EditContact(tr);
            FillContactForm(edit_contact);
            UpdateContact();
            manager.Navigator.ReturnToHomePage();

            return this;
        }

        public ContactHelper Remove(int Line)
        {
            GoToHomePage();
            SelectContact(Line);
            DeleteContact();
            ConfirmDeleteContact();
            return this;


        }

        public ContactHelper ConfirmDeleteContact()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        public ContactHelper DeleteContact()
        {

            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;

        }

        public ContactHelper SelectContact(int Line)
        {
                        
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr["+ Line +"]/td/input")).Click();
            return this;
                      
        }

        public ContactHelper GoToHomePage()
        {
                  
          driver.FindElement(By.LinkText("home")).Click();
          return this;
            

        }

 



        public ContactHelper EditContact(int tr)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + tr +"]/td[8]/a/img")).Click();
            return this;
        }
        public ContactHelper UpdateContact()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[22]")).Click();
            return this;
        }


    }
}
