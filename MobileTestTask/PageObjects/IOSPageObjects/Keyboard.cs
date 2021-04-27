using MobileTestTask.Tests.IOS;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTestTask.PageObjects.IOSPageObjects
{
    public class Keyboard : CorePageObject
    {
        public Keyboard(AppiumDriver<IWebElement> driver) : base(driver)
        {

        }

        [FindsBy(How = How.XPath, Using = "//XCUIElementTypeButton[@name='Search']")]
        public IWebElement Search;

        public void ClickButton(IWebElement element) {
            WaitForElementAndClick(element, 10);
        }

    }
}
