using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;

namespace MobileTestTask.PageObjects.AndroidPageObjects
{
    public class FiltersPage : BasePage
    {
        public FiltersPage(AndroidDriver<AppiumWebElement> androidDriver) : base(androidDriver)
        {
        }

        //[FindsBy(How = How.XPath, Using = ".//*[contains(@text, 'Номер телефона')]")]
        [FindsBy(How = How.XPath, Using = ".//*[@resource-id='com.instagram.android:id/login_username']")]
        public IList<IWebElement> Filters;


        [FindsBy(How = How.XPath, Using = ".//android.widget.FrameLayout[@content-desc='Clarendon']")]
        public IWebElement ClarendonFilter;


        [FindsBy(How = How.XPath, Using = ".//*[@resource-id='com.instagram.android:id/next_button_imageview']	")]
        public IWebElement NextBtn;

    }
}
