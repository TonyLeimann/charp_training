using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static LinqToDB.Reflection.Methods.LinqToDB;

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
            Type(By.Name("mobile"), contact.Mphone);
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

        public ContactHelper ModifyContact(ContactData oldData, ContactData edit_contact)
        {
            manager.Navigator.GoToHomePage();
            EditContact(oldData.ID);
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

        public ContactHelper Remove(ContactData contact)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(contact.ID);
            DeleteContact();
            ConfirmDeleteContact();
            return this;
        }

        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            ClearGroupFilter();
            SelectContact(contact.ID);
            SelectGroupToAdd(group.ID);
            CommitAddingContactToGroup();

            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);

        }

        public void RemoveContactFromGroup(GroupData group, ContactData contact)
        {
            manager.Navigator.GoToHomePage();
            SelectGroupForRemoveContact(group.ID);
            SelectContact(contact.ID);
            SubmitRemoveContactFromGroup();

            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);

        }

        public ContactHelper FindContactAtGroupAnotherCreate()
        {
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAll()[0];// добавляем всегда первый контакт в группу, так как в группе нет никаких контактов

            if(oldList.Count == 0)
            {
                manager.Navigator.GoToHomePage();
                ClearGroupFilter();
                SelectContact(contact.ID);
                SelectGroupToAdd(group.ID);
                CommitAddingContactToGroup();

                new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                    .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
                
                ReturnToGroupAddressInGroups(group.ID);


                oldList.Add(contact);
            }

            return this;

        }

        public void ReturnToGroupAddressInGroups(string groupID)
        {
            driver.FindElement(By.CssSelector("div.msgbox")).FindElement(By.CssSelector("a[href*='./?group="+ groupID +"']")).Click();
        }
        private void SubmitRemoveContactFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        private void SelectGroupForRemoveContact(string groupID)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByValue(groupID);
        }

        public void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        public void SelectGroupToAdd(string groupID)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByValue(groupID);
        }

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
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


        public ContactHelper SelectContact(String id)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value = '" + id + "'])")).Click();
           
            return this;
        }
        public ContactHelper EditContact(int tr)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + (tr+2) +"]/td[8]/a/img")).Click();
            return this;
        }
        public ContactHelper EditContact(String id)
        {
            driver.FindElement(By.CssSelector("a[href*='edit.php?id="+ id +"']")).Click();
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
            }

            return new List<ContactData>(contactCache);
        }

        public int GetContactCount()
        {
            return driver.FindElements(By.Name("entry")).Count;
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells =  driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));
                      
            string lastname = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastname)
            {
                AllPhones = allPhones,               
                Address = address,
                AllEmails = allEmails
            };
        }
        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            EditContact(index);

            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string middleName = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string nickName = driver.FindElement(By.Name("nickname")).GetAttribute("value");
            string company = driver.FindElement(By.Name("company")).GetAttribute("value");
            
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstName, lastName) 
            { 
                Middlename = middleName,
                Nick = nickName,
                Company = company,
                Hphone = homePhone,
                Mphone = mobilePhone,
                Wphone = workPhone,
                Address = address,
                Email = email,
                Email2 = email2,
                Email3 = email3
            };
        }
        public string GetInfoDetails(int index)
        {
            manager.Navigator.GoToHomePage();
            GoToPageDetails(index);
            string textDetailsInfo = driver.FindElement(By.CssSelector("div#content")).Text;
            return textDetailsInfo;
        }
        public ContactHelper GoToPageDetails(int index)
        {
            driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"))[6].FindElement(By.TagName("a")).Click();
            return this;
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToHomePage();
            string number =  driver.FindElement(By.CssSelector("span#search_count")).Text;
            return Int32.Parse(number);
        }


    }
}
