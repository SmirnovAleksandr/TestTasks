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
    public class MyProfilePageObject : CorePageObject
    {
        public MyProfilePageObject(AppiumDriver<IWebElement> driver) : base(driver)
        {
            ContextMenu = new ContextMenu(driver);

        }

        [FindsBy(How = How.XPath, Using = "//XCUIElementTypeOther/XCUIElementTypeTable/XCUIElementTypeCell[17]")]
        public IWebElement Exit;

        public ContextMenu ContextMenu;

       

    }

    public class ContextMenu: CorePageObject
    {
        [FindsBy(How = How.XPath, Using = "//XCUIElementTypeButton[@name='Выйти']")]
        public IWebElement Exit;

        [FindsBy(How = How.XPath, Using = "//XCUIElementTypeButton[@name='Отменить']")]
        public IWebElement Cancel;

        public void ClickExitInContextMenu()
        {
            WaitForElementAndClick(Exit, 10);
            
        }

        public ContextMenu(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }
    }
}
