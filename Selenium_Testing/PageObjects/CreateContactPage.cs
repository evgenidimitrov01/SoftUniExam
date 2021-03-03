using OpenQA.Selenium;

namespace Selenium_Testing.PageObjects
{
    public class CreateContactPage : BasePage
    {
        private readonly By TxtFnameId = By.Id("firstName");
        private readonly By TxtLnameId = By.Id("lastName");
        private readonly By TxtEmailId = By.Id("email");
        private readonly By TxtPhoneId = By.Id("phone");
        private readonly By TxtCommId = By.Id("comments");
        private readonly By BtnCreateId = By.Id("create");
        private readonly By DivErrSelector = By.CssSelector("div.err");

        public CreateContactPage() { }

        public IWebElement TxtFirstName => driver.FindElement(TxtFnameId);
        public IWebElement TxtLasrName => driver.FindElement(TxtLnameId);
        public IWebElement TxtEmail => driver.FindElement(TxtEmailId);
        public IWebElement TxtPhone => driver.FindElement(TxtPhoneId);
        public IWebElement TxtComments => driver.FindElement(TxtCommId);
        public IWebElement BtnCreate => driver.FindElement(BtnCreateId);
        public IWebElement DivErr => driver.FindElement(DivErrSelector);

        public void Open()
        {
            driver.Navigate().GoToUrl(Helpers.baseUrl + "contacts/create");
        }

        public bool IsOpen()
        {
            return driver.Url == Helpers.baseUrl + "contacts/create";
        }

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
