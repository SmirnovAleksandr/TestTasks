using MobileTestTask.Configuration;
using MobileTestTask.PageObjects.AndroidPageObjects;
using MobileTestTask.Utils;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using System;
using System.IO;

namespace MobileTestTask.Tests.Android
{
   public class CoreTestCase
    {
        //Creating instance for Appium driver
        protected static AndroidDriver<AppiumWebElement> _driver;
        AppiumOptions appiumOptions;
        // private static string AppiumServerURL;
        //protected IWait Wait;
        protected Wait Wait;
        protected Wait PermitRequestWait;

        // Time parameters for waiteElement and WaitElementList
        TimeSpan timeout;
        TimeSpan timeStep;


        protected static Config config;


        void CopyFileToAndroid()
        {
            // Copy file from desktop to android                 
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\ScriptFolder\\adbScript.bat", 
                 config.Android.AndroidCapabilitiesList[0].Udid + " " + 
                 config.Android.PhotoSourceOnHost + " " + 
                 config.Android.PhotoStorageOnAndroid);
            // 
            System.Threading.Thread.Sleep(5000);
        }

        public void SetAppiumOptions()
        {
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, config.Android.AndroidCapabilitiesList[0].DeviceName);
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, config.Android.AndroidCapabilitiesList[0].PlatformName);
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, config.Android.AndroidCapabilitiesList[0].PlatformVersion);
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.NewCommandTimeout, config.Android.AndroidCapabilitiesList[0].NewCommandTimeout);            
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.Udid, config.Android.AndroidCapabilitiesList[0].Udid);
            appiumOptions.AddAdditionalCapability("automationName", config.Android.AndroidCapabilitiesList[0].AutomationName);
            appiumOptions.AddAdditionalCapability("adbExecTimeout", config.Android.AndroidCapabilitiesList[0].adbExecTimeout);
            appiumOptions.AddAdditionalCapability("appPackage", config.Android.AndroidCapabilitiesList[0].appPackage);
            appiumOptions.AddAdditionalCapability("appActivity", config.Android.AndroidCapabilitiesList[0].appActivity);
            appiumOptions.AddAdditionalCapability("testName", TestContext.CurrentContext.Test.Name);
            appiumOptions.AddAdditionalCapability("app", config.Android.AndroidCapabilitiesList[0].app);
            appiumOptions.AddAdditionalCapability("autoDismissAlerts", true);

            timeout = new TimeSpan(0, 0, 15);
            timeStep = new TimeSpan(0, 0, 0, 0, 1000);     
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            System.Diagnostics.Debug.WriteLine("---------------OTSetup CoreTests ");
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var configFile = File.ReadAllText(Path.Combine(currentDirectory, "Configuration.json"));
            config = JsonConvert.DeserializeObject<Config>(configFile);

            appiumOptions = new AppiumOptions();
            SetAppiumOptions();

            CopyFileToAndroid();

            _driver = new AndroidDriver<AppiumWebElement>(
                        new Uri(config.Android.AndroidCapabilitiesList[0].Hub),
                        appiumOptions,
                        new TimeSpan(0, 3, 0)
                        )
            { };

            Wait = new Wait { Timeout = timeout, PollingInterval = timeStep };
            Wait.IgnoreExceptionTypes(typeof(NotFoundException),
                                        typeof(StaleElementReferenceException),
                                        typeof(NoSuchElementException)
                                        );

            PermitRequestWait = new Wait { Timeout = new TimeSpan(0, 0, 5), PollingInterval = timeStep };
            PermitRequestWait.IgnoreExceptionTypes(typeof(NotFoundException),
                                        typeof(StaleElementReferenceException),
                                        typeof(NoSuchElementException)
                                        );

        }


        [OneTimeTearDown]
        public void TearDown()
        {
            System.Diagnostics.Debug.WriteLine("---------------OTTearDown CoreTests ");
            _driver?.CloseApp();
            _driver?.Quit();

            // TODO
            // Waintg 5 sec to avoid conflict between Appium Server and ADB
            System.Threading.Thread.Sleep(5000);
            
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\ScriptFolder\\TearDown.bat",
                 config.Android.AndroidCapabilitiesList[0].Udid + " " + 
                 config.Android.PhotoStorageOnAndroid );
        }

        protected bool WaitAndClick(IWebElement Elem2Click, BasePage page = null )
        {
            bool res = false;
            res = Wait.WaitElement(Elem2Click, false);

            if ( page == null)
            {   
                RandomTimeout();
                Elem2Click.Click();
            }else
            {
                
                while (PermitRequestWait.WaitElement(page.GoogleSamrtLockCancelBtn, false))
                {
                    page.GoogleSamrtLockCancelBtn.Click();
                }

                if ( !res )
                {
                    // если  не дождались элемента, то смотрим не всплыло ли что-то про разрешения
                    // т.к. разрешений может быть несколько, то повторяем в цикле пока не повторятся 
                    while (PermitRequestWait.WaitElement(page.PermitionAllowBtn, false ) )
                    { 
                            page.PermitionAllowBtn.Click();
                    }
                }
                
                RandomTimeout();                
                Elem2Click.Click();
            }

            return res;
        }


        Random rand = new Random();
        /// <summary>
        /// Just random timeout to avoid Instaramm account blocking. 
        /// </summary>
        protected void RandomTimeout()
        {
            // just random timeout to avoid Instaramm account blocking. 
            System.Threading.Thread.Sleep(rand.Next(1500, 3000));
            System.Diagnostics.Debug.WriteLine("------------------------- Do Delay to avoid Insta Blocking");
        }
    }
}
