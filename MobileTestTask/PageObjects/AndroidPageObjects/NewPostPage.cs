﻿using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using SeleniumExtras.PageObjects;

namespace MobileTestTask.PageObjects.AndroidPageObjects
{
    public class NewPostPage : BasePage
    {
        public NewPostPage(AndroidDriver<AppiumWebElement> androidDriver) : base(androidDriver)
        {
        }

        [FindsBy(How = How.XPath, Using = ".//*[@resource-id='com.instagram.android:id/next_button_imageview']	")]
        public IWebElement NextBtn;

    }
}
