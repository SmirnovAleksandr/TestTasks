using MobileTestTask.Tests.IOS;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTestTask.PageObjects.IOSPageObjects
{
    public class User : CorePageObject
    {
        public static MainPageObject MainPageObject;
        private static MorePageObject MorePageObject;
        private static LoginPageObject LoginPageObject;
        private static MyProfilePageObject MyProfilePageObject;
        


        public User(AppiumDriver<IWebElement> driver) : base(driver)
        {          

            MainPageObject = new MainPageObject(driver);
            MorePageObject = new MorePageObject(driver);
            LoginPageObject = new LoginPageObject(driver);
            MyProfilePageObject = new MyProfilePageObject(driver);
        }

        public  bool IsLoggined() {                   

            MainPageObject.ClickOnMenuItem("Ещё");
            var userIsLoginned = MorePageObject.ElementExistInMenu("Мой профиль");
            
            return userIsLoginned;                 
        }


        public void Authorize() {

            MainPageObject.ClickOnMenuItem("Ещё");
            MorePageObject.ClickOnMenuItem("Регистрация и вход");

            LoginPageObject.WaitForElementAndSendKeys(LoginPageObject.Login, "9110985699", 5);
            LoginPageObject.WaitForElementAndSendKeys(LoginPageObject.Pass, "12345678", 5);
            LoginPageObject.WaitForElementAndClick(LoginPageObject.Enter, 5);
        }


        public void LogOut() {

            MainPageObject.ClickOnMenuItem("Ещё");
            MorePageObject.ClickOnMenuItem("Мой профиль");
            MyProfilePageObject.Exit.Click();
            MyProfilePageObject.ContextMenu.ClickExitInContextMenu();

            var userIsLoginned = MorePageObject.ElementExistInMenu("Мой профиль");
            Assert.That(!userIsLoginned, "После логаута юзер остал залогиненым");

        }
    }
}
