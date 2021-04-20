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
   public class CoreTestCase
    {
        protected AppiumDriver<IWebElement> Driver;
        private static string SeleniumHubURL = $"http://172.16.7.152:4723/wd/hub";
        protected IWait Wait;

        [SetUp]
        public void SetUp()
        {
            var hubUri = new Uri(SeleniumHubURL);
            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(CapabilityType.PlatformName, "IOS");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "12.1.3");
            capabilities.AddAdditionalCapability(MobileCapabilityType.Udid, "fa0880a9e1db48e79429023bff433e08c56ca686");
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "iPhone6");
            capabilities.AddAdditionalCapability(MobileCapabilityType.AutomationName, "XCUITest");
            capabilities.AddAdditionalCapability("useNewWDA", "true");
            capabilities.AddAdditionalCapability("xcodeOrgId", "3V72XTL4S5");
            capabilities.AddAdditionalCapability(MobileCapabilityType.NewCommandTimeout, 2400);

            capabilities.AddAdditionalCapability("bundleId", "by.mts.money");

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
