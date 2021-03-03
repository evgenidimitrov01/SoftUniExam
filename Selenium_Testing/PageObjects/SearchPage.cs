using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Selenium_Testing.PageObjects
{
    public class SearchPage : BasePage
    {
        private readonly By TxtSeachBoxId = By.Id("keyword");
        private readonly By BtnSearchId = By.Id("search");
        private readonly By DivSearchResultId = By.Id("searchResult");
        private readonly By ContactsGridSelector = By.CssSelector("div.contacts-grid");

        private readonly By TableSelector = By.CssSelector("table.contact-entry");

        private readonly By TableFnameSelector = By.CssSelector("tbody > tr.fname > td");
        private readonly By TableLnameSelector = By.CssSelector("tbody > tr.lname > td");
        private readonly By TableEmailSelector = By.CssSelector("tbody > tr.email > td");
        private readonly By TablePhoneSelector = By.CssSelector("tbody > tr.phone > td");
        private readonly By TableCommentsSelector = By.CssSelector("tbody > tr.comments > td");

        public SearchPage() { }

        public IWebElement TxtSeachBox => driver.FindElement(TxtSeachBoxId);
        public IWebElement BtnSearch => driver.FindElement(BtnSearchId);
        public IWebElement DivSearchResult => driver.FindElement(DivSearchResultId);
        public IWebElement ContactsGrid => driver.FindElement(ContactsGridSelector);

        public void Open()
        {
            driver.Navigate().GoToUrl(Helpers.baseUrl + "contacts/search");
        }

        public bool IsOpen()
        {
            return driver.Url == Helpers.baseUrl + "contacts/search";
        }

        public void SearchForElement(string keyword)
        {
            TxtSeachBox.Clear();
            TxtSeachBox.SendKeys(keyword);
            BtnSearch.Click();
        }

        public List<ContactData> GetAllSearchedResultsInTable()
        {
            List<ContactData> listTableRows = new List<ContactData>();
            var table = driver.FindElements(TableSelector);
            foreach (var row in table)
            {
                if (row.Text == "Keyword")
                    continue;

                IWebElement fName = row.FindElement(TableFnameSelector);
                IWebElement lName = row.FindElement(TableLnameSelector);
                IWebElement email = row.FindElement(TableEmailSelector);
                IWebElement phone = row.FindElement(TablePhoneSelector);
                IWebElement comment = row.FindElement(TableCommentsSelector);
                listTableRows.Add(Helpers.FillContact(fName.Text, lName.Text, email.Text, phone.Text, comment.Text));
            }
            return listTableRows;
        }
    }
}
