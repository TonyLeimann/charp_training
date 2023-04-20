using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace addressbook_web_tests
{
    public class ContactHelper:HelperBase
    {
        const int OFFSET = 2;
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
            contactCache = null;
            return this;
        }

        public ContactHelper ModifyContact(int tr, ContactData edit_contact)
        {
            manager.Navigator.GoToHomePage();
            EditContact(tr);
            FillContactForm(edit_contact);
            UpdateContact();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper Remove(int Line)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(Line);
            DeleteContact();
            ConfirmDeleteContact();
            return this;
        }

        public ContactHelper ConfirmDeleteContact()
        {
            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(5000);
            return this;
        }

        public ContactHelper DeleteContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            contactCache = null;
            return this;
        }

      

        public ContactHelper SelectContact(int Line)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr["+ (Line + 2) +"]/td/input")).Click();
            return this;       
        }


        
        public ContactHelper EditContact(int tr)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + (tr+2) +"]/td[8]/a/img")).Click();
            return this;
        }
        public ContactHelper UpdateContact()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[22]")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper FindContactAnotherCreate()
        {
            manager.Navigator.GoToHomePage();

            if (!IsElementPresent(By.Name("entry")))
            {
                ContactData newContact = new ContactData("Fill", "Foden");
                Create(newContact);
            }
            return this;
                        
        }

        private List<ContactData> contactCache = null;
        

        public List<ContactData> GetContactList()
        {

            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.GoToHomePage();

                ICollection<IWebElement> elements = driver.FindElements(By.Name("entry"));

                int i = 0;
                foreach (IWebElement element in elements) 
                {
                                       
                    var id = element.FindElement(By.TagName("input")).GetAttribute("value");
                    var lastName = element.FindElement(By.XPath("//table[@id='maintable']/tbody/tr["+ (i+OFFSET) + "]/td[2]")).Text;
                    var firstName = element.FindElement(By.XPath("//table[@id='maintable']/tbody/tr["+ (i+OFFSET) +"]/td[3]")).Text;

                    contactCache.Add(new ContactData(firstName, lastName) { ID = id });
                    i++;
                }

                //for (int i = 0; i < elements.Count; i++)
                //{
                                              
                //    var lastName = driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + (i + 2) + "]/td[2]")).Text;
                //    var firstName = driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + (i + 2) + "]/td[3]")).Text;

                //    contactCache.Add(new ContactData(firstName, lastName) { ID = id});

                //}
            }

            return new List<ContactData>(contactCache);
        }

        public int GetContactCount()
        {
            return driver.FindElements(By.Name("entry")).Count;
        }

    }
}
