using MobileTestTask.Configuration;
using MobileTestTask.Utils;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using SeleniumExtension.Support.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTestTask.Tests.IOS
{
    [SetUpFixture]
    public class CoreTestCase
    {
        protected AppiumDriver<IWebElement> Driver;
        private static string SeleniumHubURL;
        protected IWait Wait;
        protected static Config config;

        [OneTimeSetUp]
        public void SetUp()
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var configFile = File.ReadAllText(Path.Combine(currentDirectory, "Configuration.json"));       
            config = JsonConvert.DeserializeObject< Config>(configFile);
            

            SeleniumHubURL = config.IOS.Capabilities.Hub;
            var hubUri = new Uri(SeleniumHubURL);
            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, config.IOS.Capabilities.DeviceName);
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, config.IOS.Capabilities.PlatformVersion);
            capabilities.AddAdditionalCapability(CapabilityType.PlatformName, config.IOS.Capabilities.PlatformName);           
            capabilities.AddAdditionalCapability(MobileCapabilityType.Udid, config.IOS.Capabilities.Udid);        
            capabilities.AddAdditionalCapability("bundleId", config.IOS.Capabilities.BundleId);
            capabilities.AddAdditionalCapability(MobileCapabilityType.AutomationName, config.IOS.Capabilities.AutomationName);
            capabilities.AddAdditionalCapability(MobileCapabilityType.NoReset, config.IOS.Capabilities.NoReset);
            capabilities.AddAdditionalCapability("useNewWDA", config.IOS.Capabilities.UseNewWDA);
            //capabilities.AddAdditionalCapability("xcodeOrgId", "3V72XTL4S5");
            capabilities.AddAdditionalCapability(MobileCapabilityType.NewCommandTimeout, 2400);
            capabilities.AddAdditionalCapability(MobileCapabilityType.Language, config.IOS.Capabilities.Language);                     

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
