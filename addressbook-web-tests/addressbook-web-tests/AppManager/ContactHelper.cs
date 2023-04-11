using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("middlename"), contact.Middlename);
            Type(By.Name("lastname"), contact.Lastname);
            Type(By.Name("nickname"), contact.Nick);
            Type(By.Name("company"), contact.Company);
            Type(By.Name("mobile"), contact.Phone_mobile);
            return this;
        }

        public ContactHelper EnterContact()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[21]")).Click();
            return this;
        }

        public ContactHelper ModifyContact(int tr,ContactData contact, ContactData edit_contact)
        {
            GoToHomePage();
            EditContact(tr,contact);
            FillContactForm(edit_contact);
            UpdateContact();
            manager.Navigator.ReturnToHomePage();

            return this;
        }

        public ContactHelper Remove(int Line, ContactData contact)
        {
            GoToHomePage();
            SelectContact(Line,contact);
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

      

        public ContactHelper SelectContact(int Line,ContactData contact)
        {
            if (!IsElementPresent(By.Name("selected[]")))
            {
                Create(contact);
            }

            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr["+ Line +"]/td/input")).Click();
            return this;
                      
        }

        public ContactHelper GoToHomePage()
        {
                  
          driver.FindElement(By.LinkText("home")).Click();
          return this;
            

        }

 



        public ContactHelper EditContact(int tr,ContactData contact)
        {
            if (!IsElementPresent(By.Name("selected[]")))
            {
                Create(contact);
            }

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
