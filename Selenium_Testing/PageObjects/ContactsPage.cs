using OpenQA.Selenium;
using System.Collections.Generic;
namespace Selenium_Testing.PageObjects
{
    public class ContactsPage
    {
        IWebDriver driver;
        public ContactsPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement ContactsGrid => driver.FindElement(By.CssSelector("div.contacts-grid"));

        public List<ContactData> GetAllContactsInTable()
        {
            List<ContactData> listTableRows = new List<ContactData>();
            var table = driver.FindElements(By.CssSelector("table.contact-entry"));
            foreach (var row in table)
            {
                IWebElement fName = row.FindElement(By.CssSelector("tbody > tr.fname > td"));
                IWebElement lName = row.FindElement(By.CssSelector("tbody > tr.lname > td"));
                IWebElement email = row.FindElement(By.CssSelector("tbody > tr.email > td"));
                IWebElement phone = row.FindElement(By.CssSelector("tbody > tr.phone > td"));
                IWebElement comment = row.FindElement(By.CssSelector("tbody > tr.comments > td"));
                listTableRows.Add(new ContactData() { 
                   FirstName = fName.Text,
                   LastName = lName.Text,
                   Email = email.Text,
                   Phone = phone.Text,
                   Comments = comment.Text
                });
            }
            return listTableRows;
        }
    }
}
