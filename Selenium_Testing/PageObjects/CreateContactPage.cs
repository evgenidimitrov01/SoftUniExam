using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium_Testing.PageObjects
{
    public class CreateContactPage
    {
        IWebDriver driver;

        public CreateContactPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement TxtFirstName => driver.FindElement(By.Id("firstName"));
        public IWebElement TxtLasrName => driver.FindElement(By.Id("lastName"));
        public IWebElement TxtEmail => driver.FindElement(By.Id("email"));
        public IWebElement TxtPhone => driver.FindElement(By.Id("phone"));
        public IWebElement TxtComments => driver.FindElement(By.Id("comments"));
        public IWebElement BtnCreate => driver.FindElement(By.Id("create"));
        public IWebElement DivErr => driver.FindElement(By.CssSelector("div.err"));

        public void CreateNewContact(ContactData contact)
        {
            //Clear if there is some data
            TxtFirstName.Clear();
            TxtLasrName.Clear();
            TxtEmail.Clear();
            TxtPhone.Clear();
            TxtComments.Clear();

            //Send keys to fields
            TxtFirstName.SendKeys(contact.FirstName);
            TxtLasrName.SendKeys(contact.LastName);
            TxtEmail.SendKeys(contact.Email);
            TxtPhone.SendKeys(contact.Phone);
            TxtComments.SendKeys(contact.Comments);

            //Click on the submit button to create new contacts
            BtnCreate.Click();
        }


    }
}
