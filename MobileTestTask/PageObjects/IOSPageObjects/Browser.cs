using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.PageObjects;

namespace MobileTestTask.PageObjects.IOSPageObjects
{
    public class Browser : CorePageObject
    {
        public Browser(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }

        [FindsBy(How = How.XPath, Using = "//XCUIElementTypeOther/XCUIElementTypeTextField[@name=“Address”]")]
        public IWebElement BrowserAddress;

    }
}
