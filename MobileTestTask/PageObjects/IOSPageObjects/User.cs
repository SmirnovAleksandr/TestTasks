using MobileTestTask.Configuration;
using MobileTestTask.Tests.IOS;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTestTask.PageObjects.IOSPageObjects
{
    public class User : CorePageObject
    {
        private static MainPageObject MainPageObject;
        private static MorePageObject MorePageObject;
        private static LoginPageObject LoginPageObject;
        private static MyProfilePageObject MyProfilePageObject;
        private static Config config;

        public User(AppiumDriver<IWebElement> driver) : base(driver)
        {          

            MainPageObject = new MainPageObject(driver);
            MorePageObject = new MorePageObject(driver);
            LoginPageObject = new LoginPageObject(driver);
            MyProfilePageObject = new MyProfilePageObject(driver);
            var configFile = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration.json"));
            config = JsonConvert.DeserializeObject<Config>(configFile);
        }

        public  bool IsLoggined() {                   

            MainPageObject.ClickOnMenuItem(config.IOS.IOSAuthorizeTest.IsLoggined.menuElementForClick);
            var userIsLoginned = MorePageObject.ElementExistInMenu(config.IOS.IOSAuthorizeTest.IsLoggined.userIsLoginnedAttribute);
            
            return userIsLoginned;                 
        }


        public void Authorize() {

            MainPageObject.ClickOnMenuItem(config.IOS.IOSAuthorizeTest.Authorize.MenuElementForClick1);
            MorePageObject.ClickOnMenuItem(config.IOS.IOSAuthorizeTest.Authorize.MenuElementForClick2);

            LoginPageObject.WaitForElementAndSendKeys(LoginPageObject.Login, config.IOS.IOSAuthorizeTest.Authorize.Login, 5);
            LoginPageObject.WaitForElementAndSendKeys(LoginPageObject.Pass, config.IOS.IOSAuthorizeTest.Authorize.Pass, 5);
            LoginPageObject.WaitForElementAndClick(LoginPageObject.Enter, 5);
        }


        public void LogOut() {

            MainPageObject.ClickOnMenuItem(config.IOS.IOSAuthorizeTest.LogOut.MenuElementForClick1);
            MorePageObject.ClickOnMenuItem(config.IOS.IOSAuthorizeTest.LogOut.MenuElementForClick2);
            MyProfilePageObject.Exit.Click();
            MyProfilePageObject.ContextMenu.ClickExitInContextMenu();

            var userIsLoginned = MorePageObject.ElementExistInMenu(config.IOS.IOSAuthorizeTest.LogOut.ElementExistInMenu);
            Assert.That(!userIsLoginned, "После логаута юзер остал залогиненым");

        }
    }
}
