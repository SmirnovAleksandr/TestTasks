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
    public class LoginPageObject : CorePageObject
    {
        public LoginPageObject(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }
                
        [FindsBy(How = How.XPath, Using = "//XCUIElementTypeCell[1]//XCUIElementTypeTextField")]
        public IWebElement Login;

        [FindsBy(How = How.XPath, Using = "//XCUIElementTypeCell[2]/XCUIElementTypeSecureTextField")]
        public IWebElement Pass;

        [FindsBy(How = How.XPath, Using = "//XCUIElementTypeButton[@name='Войти']")]
        public IWebElement Enter;


    }
}
