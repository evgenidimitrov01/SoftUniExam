using OpenQA.Selenium;
using System.Linq;

namespace Selenium_Testing.PageObjects
{
    public class HomePage
    {
        IWebDriver driver;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement SectionContacts => driver.FindElement(By.CssSelector("body > main > section"));
        public IWebElement BtnViewContacts => driver.FindElement(By.CssSelector("body > main > div > a:nth-child(1)"));
        public IWebElement BtnCreateContact => driver.FindElement(By.CssSelector("body > main > div > a:nth-child(2)"));
        public IWebElement BtnSearchContacts => driver.FindElement(By.CssSelector("body > main > div > a:nth-child(3)"));
        public IWebElement BtnRestFullApi => driver.FindElement(By.CssSelector("body > main > div > a:nth-child(4)"));

        public int GetContactsNumber()
        {
            int res;

            string contacts = SectionContacts.Text.Split(':').Last();
            res = int.Parse(contacts);

            return res;
        }
    }
}
