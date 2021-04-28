using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Support.PageObjects;

namespace MobileTestTask.PageObjects.IOSPageObjects
{
    public class DepartmentPageObject : CorePageObject
    {
        protected AppiumDriver<IWebElement> _driver;

        public DepartmentPageObject(AppiumDriver<IWebElement> driver) : base(driver)
        {
            _driver = driver;
        }

        [FindsBy(How = How.XPath, Using = "//XCUIElementTypeTable[@name='postOffices_tableView']/XCUIElementTypeOther[1]")]
        public IWebElement smallYandexMap;

        [FindsBy(How = How.XPath, Using = "//XCUIElementTypeOther[@name='Почтовые отделения на карте']/XCUIElementTypeOther")]
        public IWebElement bigYandexMap;

        public void ClickOnYandexMap(IWebElement map) {

            WaitForElementAndClick(map, 20);
        }

        public void  ClickOnYandexLogoByCoordinates(IWebElement map, double x, double y) {
            TouchAction action = new TouchAction(_driver);
            action.Tap(map, x, y).Perform();

        }

    }
}
