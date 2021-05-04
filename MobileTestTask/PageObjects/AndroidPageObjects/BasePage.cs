using OpenQA.Selenium;

using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
//using OpenQA.Selenium.Support.PageObjects;
using SeleniumExtras.PageObjects;


namespace MobileTestTask.PageObjects.AndroidPageObjects
{
    public class BasePage
    {
        private readonly AppiumDriver<AppiumWebElement> _driver;
        public BasePage(AndroidDriver<AppiumWebElement> androidDriver)
        {
            _driver = androidDriver;
            //#pragma warning disable CS0618 // Type or member is obsolete
            PageFactory.InitElements(androidDriver, this);
            //#pragma warning restore CS0618 // Type or member is obsolete
        }


        //TODO Should Be Refactored 
        /// <summary>
        /// Current is a mock 
        /// </summary>
        /// <returns></returns>

        public bool Dispalyed()
        {
            System.Threading.Thread.Sleep(10000);
            return true;
        }

        /// <summary>
        /// Button Allow for any permition requests popup
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@resource-id='com.android.packageinstaller:id/permission_allow_button']")]
        public IWebElement PermitionAllowBtn;

        /// <summary>
        /// GoogleSmartLock popup's Cancel button on Samsung j3  
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@resource-id='com.google.android.gms:id/cancel']")]
        public IWebElement GoogleSamrtLockCancelBtn;


    }
}
