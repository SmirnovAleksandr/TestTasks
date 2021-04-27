using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.PageObjects;

namespace MobileTestTask.PageObjects.IOSPageObjects
{
    public class MorePageObject : CorePageObject
    {
        public MorePageObject(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }

        private const string menuItem = "//XCUIElementTypeCell//XCUIElementTypeStaticText[@name='{SUBSTRING}']";
        [FindsBy(How = How.XPath, Using = menuItem)]
        public IWebElement MenuItem;

        public IWebElement GetMenuElement(string substring)
        {

            string menuElement = menuItem.Replace("{SUBSTRING}", substring);
            var element = _driver.FindElement(By.XPath(menuElement));
            return element;
        }

        public void ClickOnMenuItem(string menuElement)
        {
            WaitForElementAndClick(GetMenuElement(menuElement), 10);
        }

        public bool ElementExistInMenu(string element) {

            var isExist = false;
            string item = menuItem.Replace("{SUBSTRING}", element);
            var elements = _driver.FindElements(By.XPath(item)).Count;

            if (elements > 0) {
                isExist = true;
             };
            
            return isExist;
        }

    }
}
