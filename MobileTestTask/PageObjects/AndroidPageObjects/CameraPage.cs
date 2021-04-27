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
    public class CameraPage : BasePage
    {
        public CameraPage(AndroidDriver<AppiumWebElement> androidDriver) : base(androidDriver)
        {
        }

        [FindsBy(How = How.XPath, Using = "//*[@resource-id='com.instagram.android:id/camera_shutter_button']")]
        public IWebElement DoShootBtn;
    }
}
