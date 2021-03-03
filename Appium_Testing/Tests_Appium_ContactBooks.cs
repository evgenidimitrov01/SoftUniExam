using Appium_Testing.ActivityObjects;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Appium_Testing
{
    [TestFixture]
    public class Tests_Appium_ContactBooks
    {
        [OneTimeSetUp]
        public void Setup()
        {
            Driver.InitializeDriver();
        }

        [Test, Category("Appium Tests")]
        public void Test_FindByKeywordAndAssertFirstNameIsSteve()
        {
            ConnectActivity connectActivity = new ConnectActivity(Driver.driver);
            connectActivity.BtnConnect.Click();

            SearchContactsActivity seachActivity = new SearchContactsActivity(Driver.driver);
            seachActivity.SearchAction("steve");

            List<ContactData> listFoundedRes = seachActivity.GetAllFoundedContacts();

            Assert.Multiple(() =>
            {
                Assert.That(listFoundedRes.Any(cont => cont.FirstName == "Steve"));
                Assert.That(listFoundedRes.Any(cont => cont.LastName == "Jobs"));
            });
        }


        [OneTimeTearDown]
        public void ShutDown()
        {
            Driver.driver.Quit();
        }
    }
}