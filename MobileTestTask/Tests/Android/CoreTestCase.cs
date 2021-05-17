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
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace MobileTestTask.Tests.Android
{
    [TestFixture]
    //[Parallelizable(scope: ParallelScope.All)]
    public class CoreTestCase
    {
        //Creating instance for Appium driver
        protected static AndroidDriver<AppiumWebElement> _driver;
        AppiumOptions appiumOptions;
        protected Wait Wait;
        protected Wait PermitRequestWait;

        // Time parameters for waiteElement and WaitElementList
        TimeSpan timeout;
        TimeSpan timeStep;

        protected static Config config;

        protected int CapabilitiesItem;

        public void SetAppiumOptions()
        {
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, config.Android.AndroidCapabilitiesList[CapabilitiesItem].DeviceName);
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, config.Android.AndroidCapabilitiesList[CapabilitiesItem].PlatformName);
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, config.Android.AndroidCapabilitiesList[CapabilitiesItem].PlatformVersion);
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.NewCommandTimeout, config.Android.AndroidCapabilitiesList[CapabilitiesItem].NewCommandTimeout);            
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.Udid, config.Android.AndroidCapabilitiesList[CapabilitiesItem].Udid);
            appiumOptions.AddAdditionalCapability("automationName", config.Android.AndroidCapabilitiesList[CapabilitiesItem].AutomationName);
            appiumOptions.AddAdditionalCapability("adbExecTimeout", config.Android.AndroidCapabilitiesList[CapabilitiesItem].adbExecTimeout);
            appiumOptions.AddAdditionalCapability("appPackage", config.Android.AndroidCapabilitiesList[CapabilitiesItem].appPackage);
            appiumOptions.AddAdditionalCapability("appActivity", config.Android.AndroidCapabilitiesList[CapabilitiesItem].appActivity);
            appiumOptions.AddAdditionalCapability("testName", TestContext.CurrentContext.Test.Name);
            appiumOptions.AddAdditionalCapability("app", config.Android.AndroidCapabilitiesList[CapabilitiesItem].app);
            appiumOptions.AddAdditionalCapability("autoDismissAlerts", true);

            timeout = new TimeSpan(0, 0, 15);
            timeStep = new TimeSpan(0, 0, 0, 0, 1000);     
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {

            System.Diagnostics.Debug.WriteLine("---------------OTSetup CoreTests ");
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var configFile = File.ReadAllText(Path.Combine(currentDirectory, "Configuration.json"));
            config = JsonConvert.DeserializeObject<Config>(configFile);

            appiumOptions = new AppiumOptions();
            CapabilitiesItem = config.Android.CapabilitiesItem;            

            SetAppiumOptions();

            _driver = new AndroidDriver<AppiumWebElement>(
                        new Uri(config.Android.AndroidCapabilitiesList[CapabilitiesItem].Hub),
                        appiumOptions,
                        new TimeSpan(0, 3, 0)
                        ){ };


            // Copy file from desktop to android  
            _driver.PushFile(config.Android.PhotoStorageOnAndroid + NewPublicationPage.photoStoreName + "/test.jpg",
                    new FileInfo(config.Android.PhotoSourceOnHost));


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
            _driver?.CloseApp();

            // Here we gonna uninstall Instagramm App 
            _driver.RemoveApp("com.instagram.android");

            //   Remove  added in Setup image file 
            var command2Execute = "rm -rf " + config.Android.PhotoStorageOnAndroid + NewPublicationPage.photoStoreName;

            var command = "mobile:shell";
            var param = new Dictionary<String, String>();
            param.Add("command", command2Execute);
            string ex_result = (string)_driver.ExecuteScript(command, param);

            // Try to Clean MediaStore
            var jpfToForget = config.Android.PhotoStorageOnAndroid + NewPublicationPage.photoStoreName + "/test.jpg";
            command2Execute = " am broadcast -a android.intent.action.MEDIA_SCANNER_SCAN_FILE -d \"file://" + jpfToForget + "\"";

            param = new Dictionary<String, String>();
            param.Add("command", command2Execute );
            ex_result = (string)_driver.ExecuteScript(command, param);

            System.Diagnostics.Debug.WriteLine("---------------OTTearDown CoreTests ");

            _driver?.Quit();
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
    
    public class RunDataIntem
    {
        public static IEnumerable TestCases
        {
            get
            {
                yield return 0;
                yield return 1;
            }
        }
    }

}
