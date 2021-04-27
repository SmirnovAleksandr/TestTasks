using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using SeleniumExtras.PageObjects;

namespace MobileTestTask.PageObjects.AndroidPageObjects
{
    public class LoginActivityPage : BasePage
    {
        public LoginActivityPage(AndroidDriver<AppiumWebElement> androidDriver) : base(androidDriver)
        {
        }

        //[FindsBy(How = How.XPath, Using = ".//*[contains(@text, 'Номер телефона')]")]
        [FindsBy(How = How.XPath, Using = ".//*[@resource-id='com.instagram.android:id/login_username']")]
        public IWebElement Login;

        //[FindsBy(How = How.XPath, Using = ".//*[contains(@text, 'Пароль')] ")]
        [FindsBy(How = How.XPath, Using = ".//*[@resource-id='com.instagram.android:id/password']")]
        public IWebElement Password;

        //[FindsBy(How = How.XPath, Using = ".//*[contains(@text, 'Вход')]")]
        [FindsBy(How = How.XPath, Using = ".//*[@resource-id='com.instagram.android:id/button_text']")]
        public IWebElement GoInBtn;

/*
        [FindsBy(How = How.XPath, Using = ".//*[@text='Войти']")]
        public IWebElement GoInBtn;
*/
       

        public bool Displayed()
        {
            try
            {

                if (GoInBtn.Displayed)
                {
                    return true;
                }

            }
            catch (NoSuchElementException nsee)
            {
                return false;
            }

            return false;
        }


    }
}
