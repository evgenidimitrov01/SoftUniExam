using OpenQA.Selenium;
using System.Collections.Generic;

namespace Selenium_Testing.PageObjects
{
    public class ContactsPage : BasePage
    {
        private readonly By GridSelector = By.CssSelector("div.contacts-grid");

        private readonly By TableFnameSelector = By.CssSelector("tbody > tr.fname > td");
        private readonly By TableLnameSelector = By.CssSelector("tbody > tr.lname > td");
        private readonly By TableEmailSelector = By.CssSelector("tbody > tr.email > td");
        private readonly By TablePhoneSelector = By.CssSelector("tbody > tr.phone > td");
        private readonly By TableCommentsSelector = By.CssSelector("tbody > tr.comments > td");
        private readonly By TableEntrySelector = By.CssSelector("table.contact-entry");
        private readonly By TableContact1Id = By.Id("contact1");

        public ContactsPage() { }

        public IWebElement ContactsGrid => driver.FindElement(GridSelector);

        public void Open()
        {
            driver.Navigate().GoToUrl(Helpers.baseUrl + "contacts");
        }

        public bool IsOpen()
        {
            return driver.Url == Helpers.baseUrl + "contacts";
        }

        public List<ContactData> GetAllContactsInTable()
        {
            List<ContactData> listTableRows = new List<ContactData>();
            var table = driver.FindElements(TableEntrySelector);
            foreach (var row in table)
            {
                IWebElement fName = row.FindElement(TableFnameSelector);
                IWebElement lName = row.FindElement(TableLnameSelector);
                IWebElement email = row.FindElement(TableEmailSelector);
                IWebElement phone = row.FindElement(TablePhoneSelector);
                IWebElement comment = row.FindElement(TableCommentsSelector);
                listTableRows.Add(Helpers.FillContact(fName.Text, lName.Text, email.Text, phone.Text, comment.Text));
            }
            return listTableRows;
        }

        public ContactData GetFirstResult()
        {
            ContactData contact;
            var table = driver.FindElement(TableContact1Id);
            contact = Helpers.FillContact(
            table.FindElement(TableFnameSelector).Text,
            table.FindElement(TableLnameSelector).Text,
            table.FindElement(TableEmailSelector).Text,
            table.FindElement(TablePhoneSelector).Text,
            table.FindElement(TableCommentsSelector).Text);
            return contact;
        }
    }
}
