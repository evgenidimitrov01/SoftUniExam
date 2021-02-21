using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appium_Testing
{
    public static class Driver
    {
        public static AndroidDriver<AndroidElement> driver;

        public static void InitializeDriver()
        {
            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");

            appiumOptions.AddAdditionalCapability("app", Helpers.appPath);

            driver = new AndroidDriver<AndroidElement>(new Uri(Helpers.appiumServerUrl), appiumOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Helpers.WaitSecsForElements);
        }

        public static AndroidElement ScrollToElement(string element)
        {
            return driver.FindElementByAndroidUIAutomator(
                "new UiScrollable(new UiSelector().scrollable(true))" +
                ".scrollIntoView(new UiSelector().resourceIdMatches(" +
                "\"" + element + "\"))");
        }
    }
}
