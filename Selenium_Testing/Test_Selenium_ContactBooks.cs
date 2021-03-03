using NUnit.Framework;
using Selenium_Testing.PageObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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
            ContactsPage contactsPage = new ContactsPage();
            contactsPage.Open();
            Assert.Multiple(() =>
            {
                Assert.IsTrue(contactsPage.IsOpen());
                Assert.AreEqual(contactsPage.PageTitle.Text, "View Contacts");
            });

            ContactData firstContact = contactsPage.GetFirstResult();

            Assert.Multiple(() =>
            {
                Assert.AreEqual("Steve", firstContact.FirstName);
                Assert.AreEqual("Jobs", firstContact.LastName);
            });
        }

        [Test, Category("Selenium Tests")]
        public void Test_NumberOfContacts()
        {
            HomePage homePage = new HomePage();
            homePage.Open();
            Assert.IsTrue(homePage.IsOpen());
            Assert.AreEqual(homePage.PageTitle.Text, "Welcome");

            int contactsNumber = homePage.GetContactsNumber();
            
            Assert.IsTrue(contactsNumber >= 0);
        }

        [Test, Category("Selenium Tests")]
        public void Test_SearchByKeyword()
        {
            SearchPage searchPage = new SearchPage();
            searchPage.Open();
            Assert.Multiple(() =>
            {
                Assert.IsTrue(searchPage.IsOpen());
                Assert.AreEqual(searchPage.PageTitle.Text, "Search Contacts");
            });

            searchPage.SearchForElement("albert");

            List<ContactData> listSearchedItems = searchPage.GetAllSearchedResultsInTable();
            ContactData firstContact = listSearchedItems[0];

            Assert.Multiple(() =>
            {
                Assert.AreEqual("Albert", firstContact.FirstName);
                Assert.AreEqual("Einstein", firstContact.LastName);
            });
        }

        [Test, Category("Selenium Tests")]
        public void Test_SearchByInvalidKeyword()
        {
            string invalidKeyword = "invalid" + Helpers.GetRandomNumber();
            SearchPage searchPage = new SearchPage();
            searchPage.Open();
            Assert.Multiple(() =>
            {
                Assert.IsTrue(searchPage.IsOpen());
                Assert.AreEqual(searchPage.PageTitle.Text, "Search Contacts");
            });

            searchPage.SearchForElement(invalidKeyword);

            Assert.AreEqual("No contacts found.", searchPage.DivSearchResult.Text);
        }

        [Test, Category("Selenium Tests")]
        [TestCase("", "lastname", "email@email.bg", "phone", "comment", "Error: First name cannot be empty!", TestName = "Test_EmptyFirstName")]
        [TestCase("firstname", "", "email@email.bg", "phone", "comment", "Error: Last name cannot be empty!", TestName = "Test_EmptyLastName")]
        [TestCase("firstname", "lastname", "", "phone", "comment", "Error: Invalid email!", TestName = "Test_EmptyEmail")]
        public void Test_CreateContactInvalidData(string fName, string lname, string email, string phone, string comment, string expected)
        {
            CreateContactPage createContactPage = new CreateContactPage();
            createContactPage.Open();
            Assert.Multiple(() =>
            {
                Assert.IsTrue(createContactPage.IsOpen());
                Assert.AreEqual("Create Contact", createContactPage.PageTitle.Text);
            });

            ContactData newContact = new ContactData()
            {
                FirstName = fName,
                LastName = lname,
                Email = email,
                Phone = phone,
                Comments = comment
            };

            createContactPage.CreateNewContact(newContact);

            Assert.AreEqual(expected, createContactPage.DivErr.Text);
        }

        [Test, Category("Selenium Tests")]
        public void Test_CreateContactValidData()
        {
            CreateContactPage createContactPage = new CreateContactPage();
            createContactPage.Open();
            Assert.Multiple(() =>
            {
                Assert.IsTrue(createContactPage.IsOpen());
                Assert.AreEqual("Create Contact", createContactPage.PageTitle.Text);
            });

            ContactData newContact = new ContactData()
            {
                FirstName = Helpers.GetRandomString(10),
                LastName = Helpers.GetRandomString(10),
                Email = "evgeni@abv.bg",
                Phone = "+1234567890",
                Comments = "Random comment: " + Helpers.GetRandomString(20)
            };

            createContactPage.CreateNewContact(newContact);

            ContactsPage contactsPage = new ContactsPage();
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