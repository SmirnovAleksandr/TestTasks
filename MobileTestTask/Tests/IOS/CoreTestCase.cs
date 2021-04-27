using MobileTestTask.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using SeleniumExtension.Support.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTestTask.Tests.IOS
{
    [SetUpFixture]
    public class CoreTestCase
    {
        protected AppiumDriver<IWebElement> Driver;
        private static string SeleniumHubURL = $"http://172.16.6.197:4723/wd/hub";
        protected IWait Wait;

    
        [OneTimeSetUp]
        public void SetUp()
        {
            var hubUri = new Uri(SeleniumHubURL);
            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "iPhone8");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "14.4.2");
            capabilities.AddAdditionalCapability(CapabilityType.PlatformName, "IOS");           
            capabilities.AddAdditionalCapability(MobileCapabilityType.Udid, "a459266f870f38bd0c183a49ba5fbcb3ecd90079");
            capabilities.AddAdditionalCapability("bundleId", "ru.russianpost.Russian-Post");
            capabilities.AddAdditionalCapability(MobileCapabilityType.AutomationName, "XCUITest");
            capabilities.AddAdditionalCapability(MobileCapabilityType.NoReset, "true");
            capabilities.AddAdditionalCapability("useNewWDA", "false");
            //capabilities.AddAdditionalCapability("xcodeOrgId", "3V72XTL4S5");
            capabilities.AddAdditionalCapability(MobileCapabilityType.NewCommandTimeout, 2400);

            //capabilities.AddAdditionalCapability("bundleId", "by.mts.money");


            Driver = new IOSDriver<IWebElement>(new Uri(SeleniumHubURL), capabilities);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            Wait = new Wait { Timeout = new TimeSpan(0, 1, 0), PollingInterval = new TimeSpan(0, 0, 3) };
            Wait.IgnoreExceptionTypes(typeof(NotFoundException),
                                        typeof(StaleElementReferenceException),
                                        typeof(NoSuchElementException)
                                        );
        }




    }
}
