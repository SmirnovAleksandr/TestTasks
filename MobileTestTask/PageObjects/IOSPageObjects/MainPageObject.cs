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
    public class MainPageObject : CorePageObject
    {
        public MainPageObject(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }
                
        private const string menuItem = "//XCUIElementTypeTabBar[@name='rPTabBar_tabBar']//XCUIElementTypeButton[@label='{SUBSTRING}']";
        [FindsBy(How = How.XPath, Using = menuItem)]
        public IWebElement MenuItem;

        public IWebElement GetMenuElement(string substring) {

            string menuElement = menuItem.Replace("{SUBSTRING}", substring);
            var element = _driver.FindElement(By.XPath(menuElement));
            return element;
        }


        public void ClickOnMenuItem(string menuElement)
        {            
            WaitForElementAndClick(GetMenuElement(menuElement), 15);

        }

        

    }
}
