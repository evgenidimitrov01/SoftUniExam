using OpenQA.Selenium;

namespace Selenium_Testing.PageObjects
{
    public class BasePage
    {
        private readonly By PageTitleSelector = By.CssSelector("body > header > h1");
        private readonly By BtnHomePageSelector = By.CssSelector("body > aside > ul > li:nth-child(1) > a");
        private readonly By BtnContactsPageSelector = By.CssSelector("body > aside > ul > li:nth-child(2) > a");
        private readonly By BtnCreateContactSelector = By.CssSelector("body > aside > ul > li:nth-child(3) > a");
        private readonly By BtnSearchPageSelector = By.CssSelector("body > aside > ul > li:nth-child(4) > a");

        protected internal IWebDriver driver { get; }
        public BasePage()
        {
            this.driver = Driver.driver;
        }

        public IWebElement PageTitle => driver.FindElement(PageTitleSelector);
        public IWebElement BtnHomePage => driver.FindElement(BtnHomePageSelector);
        public IWebElement BtnContactsPage => driver.FindElement(BtnContactsPageSelector);
        public IWebElement BtnCreateContactPage => driver.FindElement(BtnCreateContactSelector);
        public IWebElement BtnSearchPage => driver.FindElement(BtnSearchPageSelector);

    }
}
