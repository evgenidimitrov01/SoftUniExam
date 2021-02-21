using NUnit.Framework;
using Selenium_Testing.PageObjects;
using System.Collections.Generic;
using System.Linq;

namespace Selenium_Testing
{
    [TestFixture]
    public class Test_Selenium_ContactBooks
    {

        [OneTimeSetUp]
        public void Setup()
        {
            Driver.InitializeDriver();
            Driver.driver.Navigate().GoToUrl(Helpers.baseUrl);
        }

        [Test, Category("Selenium Tests")]
        public void Test_FirstContactIsSteveJobs()
        {
            BasePage basePage = new BasePage(Driver.driver);
            basePage.BtnContactsPage.Click();

            ContactsPage contactsPage = new ContactsPage(Driver.driver);
            List<ContactData> listContacts = contactsPage.GetAllContactsInTable();

            ContactData firstContact = listContacts[0];

            Assert.AreEqual("Steve", firstContact.FirstName);
            Assert.AreEqual("Jobs", firstContact.LastName);
        }

        [Test, Category("Selenium Tests")]
        public void Test_NumberOfContacts()
        {
            Driver.driver.Navigate().GoToUrl(Helpers.baseUrl);
            HomePage homePage = new HomePage(Driver.driver);

            int contactsNumber = homePage.GetContactsNumber();
            
            Assert.IsTrue(contactsNumber >= 0);
        }

        [Test, Category("Selenium Tests")]
        public void Test_SearchByKeyword()
        {
            Driver.driver.Navigate().GoToUrl(Helpers.baseUrl + "contacts/search");
            SearchPage searchPage = new SearchPage(Driver.driver);
            searchPage.SearchForElement("albert");
            List<ContactData> listSearchedItems = searchPage.GetAllSearchedResultsInTable();

            ContactData firstContact = listSearchedItems[0];

            Assert.AreEqual("Albert", firstContact.FirstName);
            Assert.AreEqual("Einstein", firstContact.LastName);
        }

        [Test, Category("Selenium Tests")]
        public void Test_SearchByInvalidKeyword()
        {
            Driver.driver.Navigate().GoToUrl(Helpers.baseUrl + "contacts/search");
            SearchPage searchPage = new SearchPage(Driver.driver);

            searchPage.SearchForElement("invalid2635");

            Assert.AreEqual("No contacts found.", searchPage.DivSearchResult.Text);
        }

        [Test, Category("Selenium Tests")]
        [TestCase("", "lastname", "email@email.bg", "phone", "comment", "Error: First name cannot be empty!", TestName = "Empty First Name")]
        [TestCase("firstname", "", "email@email.bg", "phone", "comment", "Error: Last name cannot be empty!", TestName = "Empty Last Name")]
        [TestCase("firstname", "lastname", "", "phone", "comment", "Error: Invalid email!", TestName = "Empty Email")]
        public void Test_CreateContactInvalidData(string fName, string lname, string email, string phone, string comment, string expected)
        {
            Driver.driver.Navigate().GoToUrl(Helpers.baseUrl + "contacts/create");
            CreateContactPage createContactPage = new CreateContactPage(Driver.driver);

            ContactData newContact = new ContactData();
            newContact.FirstName = fName;
            newContact.LastName = lname;
            newContact.Email = email;
            newContact.Phone = phone;
            newContact.Comments = comment;

            createContactPage.CreateNewContact(newContact);

            Assert.AreEqual(expected, createContactPage.DivErr.Text);
        }

        [Test, Category("Selenium Tests")]
        public void Test_CreateContactValidData()
        {
            Driver.driver.Navigate().GoToUrl(Helpers.baseUrl + "contacts/create");
            CreateContactPage createContactPage = new CreateContactPage(Driver.driver);

            ContactData newContact = new ContactData();
            newContact.FirstName = Helpers.GetRandomString(10);
            newContact.LastName = Helpers.GetRandomString(10);
            newContact.Email = "evgeni@abv.bg";
            newContact.Phone = "+1234567890";
            newContact.Comments = "Random comment: " + Helpers.GetRandomString(20);

            createContactPage.CreateNewContact(newContact);

            ContactsPage contactsPage = new ContactsPage(Driver.driver);
            List<ContactData> listContacts = contactsPage.GetAllContactsInTable();

            Assert.That(listContacts.Any(cont => cont.FirstName == newContact.FirstName));
        }

        [OneTimeTearDown]
        public void ShutDown()
        {
            Driver.driver.Quit();
        }
    }
}