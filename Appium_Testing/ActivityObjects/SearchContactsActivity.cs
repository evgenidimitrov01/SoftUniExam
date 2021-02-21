using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Text;

namespace Appium_Testing.ActivityObjects
{
    public class SearchContactsActivity
    {
        AndroidDriver<AndroidElement> driver;

        public SearchContactsActivity(AndroidDriver<AndroidElement> driver)
        {
            this.driver = driver;
        }

        public AndroidElement EditTextSearchBox => driver.FindElementById(Helpers.appPackage + ":id/editTextKeyword");
        public AndroidElement BtnSearch => driver.FindElementById(Helpers.appPackage + ":id/buttonSearch");

        public AndroidElement TxtContactsFound => driver.FindElementById(Helpers.appPackage + ":id/textViewSearchResult");

        public AndroidElement ListFoundedContacts => driver.FindElementById(Helpers.appPackage + ":id/recyclerViewContacts");

        public void SearchAction(string keyword)
        {
            EditTextSearchBox.Clear();
            EditTextSearchBox.SendKeys(keyword);
            BtnSearch.Click();
        }
        
        public ReadOnlyCollection<AppiumWebElement> GetSearchedResults()
        {
            return ListFoundedContacts.FindElementsByXPath("//*[@class='android.widget.TableLayout']");
        }

        public List<ContactData> GetAllFoundedContacts()
        {
            List<ContactData> listContacts = new List<ContactData>();

            var contacts = ListFoundedContacts.FindElementsByXPath("//*[@class='android.widget.TableLayout']");
            foreach (var c in contacts)
            {
                var fName = driver.FindElementById(Helpers.appPackage + ":id/textViewFirstName");
                var lName = driver.FindElementById(Helpers.appPackage + ":id/textViewLastName");
                var email = driver.FindElementById(Helpers.appPackage + ":id/textViewEmail");
                var phone = driver.FindElementById(Helpers.appPackage + ":id/textViewPhone");
                var comments = driver.FindElementById(Helpers.appPackage + ":id/textViewComments");
                listContacts.Add(new ContactData() { 
                    FirstName = fName.Text,
                    LastName = lName.Text,
                    Email = email.Text,
                    Phone = phone.Text,
                    Comments = comments.Text
                });
            }

            return listContacts;
        }
    }
}
