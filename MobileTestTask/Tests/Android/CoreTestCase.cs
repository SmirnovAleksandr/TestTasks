using MobileTestTask.PageObjects.AndroidPageObjects;
using MobileTestTask.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using SeleniumExtension.Support.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MobileTestTask.Tests.Android
{
   public class CoreTestCase
    {
        //protected AppiumDriver<IWebElement> Driver;

        //Creating instance for Appium driver
        protected static AndroidDriver<AppiumWebElement> _driver;
        AppiumOptions appiumOptions;


        private static string AppiumServerURL;
        //protected IWait Wait;
        protected Wait Wait;
        protected Wait PermitRequestWait;

        // Time parameters for waiteElement and WaitElementList
        TimeSpan timeout;
        TimeSpan timeStep;

        // для тестовых целей есть возможность менять устройства и соответсвенно appiumOptions  ( см SetAppiumOptions )
        string usedDevices = "Samsung j3";// "Emulator-5554"; // 


        public void SetAppiumOptions()
        {
            switch (usedDevices)
            {
                case "Samsung j3":
                    appiumOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Samsung j3");
                    appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
                    appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "8.0");
                    appiumOptions.AddAdditionalCapability(MobileCapabilityType.NewCommandTimeout, 2400);
                    appiumOptions.AddAdditionalCapability(MobileCapabilityType.Udid, "4200bdd2cc28a4e9");
                    appiumOptions.AddAdditionalCapability("automationName", "UiAutomator2");
                    appiumOptions.AddAdditionalCapability("adbExecTimeout", "60000");
                    appiumOptions.AddAdditionalCapability("appPackage", "com.instagram.android");
                    appiumOptions.AddAdditionalCapability("appActivity", ".activity.MainTabActivity");
                    appiumOptions.AddAdditionalCapability("testName", TestContext.CurrentContext.Test.Name);
                    appiumOptions.AddAdditionalCapability("app", "D:\\apks\\Instagram_v184.0.0.30.117_armeabi-v7a.apk");
                    appiumOptions.AddAdditionalCapability("autoDismissAlerts", true);
                    
                    timeout = new TimeSpan(0, 0, 15);
                    timeStep = new TimeSpan(0, 0, 0, 0, 1000);
                    AppiumServerURL = "http://SPBPC088.artq.com:4723/wd/hub";

                    break;
                case "Emulator-5554":
                    appiumOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Emulator-5554");
                    appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
                    appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "6.0");
                    appiumOptions.AddAdditionalCapability(MobileCapabilityType.NewCommandTimeout, 2400);
                    appiumOptions.AddAdditionalCapability(MobileCapabilityType.Udid, "emulator-5554");
                    appiumOptions.AddAdditionalCapability("automationName", "UiAutomator2");
                    appiumOptions.AddAdditionalCapability("adbExecTimeout", "60000");
                    appiumOptions.AddAdditionalCapability("appPackage", "com.instagram.android");
                    appiumOptions.AddAdditionalCapability("appActivity", ".activity.MainTabActivity");
                    appiumOptions.AddAdditionalCapability("testName", TestContext.CurrentContext.Test.Name);
                    appiumOptions.AddAdditionalCapability("app", "C:\\Users\\amosovn\\Documents\\Q-Art\\AutotestTender202104\\Instagram_v184.0.0.30.117_x86.apk");
                    
                    timeout = new TimeSpan(0, 0, 45);
                    timeStep = new TimeSpan(0, 0, 0, 0, 1000);
                    AppiumServerURL = "http://SPBPC088.artq.com:4723/wd/hub";

                    break;
                default:
                    Assert.That(false, "не выбрана конфигуграция оборуования");
                    break;
            }
        }

        [SetUp]
        public void SetUp()
        {
            appiumOptions = new AppiumOptions();

            SetAppiumOptions();

            _driver = new AndroidDriver<AppiumWebElement>(
                        new Uri(AppiumServerURL),
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

           // var af = Context.

        }


        [TearDown]
        public void TearDown()
        {
            _driver?.CloseApp();
            _driver?.Quit();
            Process.Start("D:\\ADB\\adb.exe", @"-s 4200bdd2cc28a4e9 uninstall com.instagram.android");
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
                /* После принятия разрешений кнопка "сделать снимок" откатывается назад */
                //  вопрос как обойти аккуратно . 
                Elem2Click.Click();
            }


            return res;
        }


        Random rand = new Random();
        /// <summary>
        /// Оust random timeout to avoid Instaramm account blocking. 
        /// </summary>
        protected void RandomTimeout()
        {
            // just random timeout to avoid Instaramm account blocking. 
            System.Threading.Thread.Sleep(rand.Next(3000, 10000));
        }



    }
}
