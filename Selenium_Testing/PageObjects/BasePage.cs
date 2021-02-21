using OpenQA.Selenium;

namespace Selenium_Testing.PageObjects
{
    public class BasePage
    {
        IWebDriver driver;
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement PageTitle => driver.FindElement(By.CssSelector("body > header > h1"));
        public IWebElement BtnHomePage => driver.FindElement(By.CssSelector("body > aside > ul > li:nth-child(1) > a"));
        public IWebElement BtnContactsPage => driver.FindElement(By.CssSelector("body > aside > ul > li:nth-child(2) > a"));
        public IWebElement BtnCreateContactPage => driver.FindElement(By.CssSelector("body > aside > ul > li:nth-child(3) > a"));
        public IWebElement BtnSearchPage => driver.FindElement(By.CssSelector("body > aside > ul > li:nth-child(4) > a"));

    }
}
