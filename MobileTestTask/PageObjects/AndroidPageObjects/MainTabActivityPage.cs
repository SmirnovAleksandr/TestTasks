using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;


namespace MobileTestTask.PageObjects.AndroidPageObjects
{
    public class MainTabActivityPage : BasePage
    {
        public MainTabActivityPage(AndroidDriver<AppiumWebElement> androidDriver) : base(androidDriver)
        {
        }

        //[FindsBy(How = How.XPath, Using = ".//*[contains(@text, 'Вход') or contains(@text, 'Log In') ]")]
        [FindsBy(How = How.XPath, Using = "//*[@resource-id='com.instagram.android:id/log_in_button']")]
        public IWebElement GoInBtn;


/*  // Похоже перепутал (( 

*/

    }
}
