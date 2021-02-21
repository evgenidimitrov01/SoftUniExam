using OpenQA.Selenium.Appium.Android;

namespace Appium_Testing.ActivityObjects
{
    public class ConnectActivity
    {
        AndroidDriver<AndroidElement> driver;
        public ConnectActivity(AndroidDriver<AndroidElement> driver)
        {
            this.driver = driver;
        }

        public AndroidElement BtnConnect => driver.FindElementById(Helpers.appPackage + ":id/buttonConnect");

    }
}
