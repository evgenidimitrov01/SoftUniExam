using OpenQA.Selenium;
using System.Linq;
using System.Xml.Linq;

namespace Selenium_Testing.PageObjects
{
    public class HomePage : BasePage
    {
        private readonly By SectionContactsSelector = By.CssSelector("body > main > section");
        private readonly By BtnViewContactsSelector = By.CssSelector("body > main > div > a:nth-child(1)");
        private readonly By BtnCreateContactSelector = By.CssSelector("body > main > div > a:nth-child(2)");
        private readonly By BtnSearchContactSelector = By.CssSelector("body > main > div > a:nth-child(3)");
        private readonly By BtnRestFullApiSelector = By.CssSelector("body > main > div > a:nth-child(4");

        public HomePage() { }

        public IWebElement SectionContacts => driver.FindElement(SectionContactsSelector);
        public IWebElement BtnViewContacts => driver.FindElement(BtnViewContactsSelector);
        public IWebElement BtnCreateContact => driver.FindElement(BtnCreateContactSelector);
        public IWebElement BtnSearchContacts => driver.FindElement(BtnSearchContactSelector);
        public IWebElement BtnRestFullApi => driver.FindElement(BtnRestFullApiSelector);

        public void Open()
        {
            driver.Navigate().GoToUrl(Helpers.baseUrl);
        }

        public bool IsOpen()
        {
            return driver.Url == Helpers.baseUrl;
        }

        public int GetContactsNumber()
        {
            int res;

            string contacts = SectionContacts.Text.Split(':').Last();
            res = int.Parse(contacts);

            return res;
        }
    }
}
