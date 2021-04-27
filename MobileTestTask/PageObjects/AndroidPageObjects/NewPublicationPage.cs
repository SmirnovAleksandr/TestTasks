using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;


namespace MobileTestTask.PageObjects.AndroidPageObjects
{
    public class NewPublicationPage : BasePage
    {
        public NewPublicationPage(AndroidDriver<AppiumWebElement> androidDriver) : base(androidDriver)
        {
        }

        [FindsBy(How = How.XPath, Using = ".//*[@resource-id='com.instagram.android:id/tab_icon']")]
        public IWebElement PLusBtn;

        


        [FindsBy(How = How.XPath, Using = "//*[@resource-id='com.instagram.android:id/unified_camera_button']")]
        public IWebElement CamneraBtn;

    }
}
