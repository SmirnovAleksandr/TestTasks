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
    public class MainActivityPage : BasePage
    {
        public MainActivityPage(AndroidDriver<AppiumWebElement> androidDriver) : base(androidDriver)
        {
        }

        [FindsBy(How = How.XPath, Using = ".//*[@resource-id='com.instagram.android:id/creation_tab']")]
        public IWebElement DoShootBtn;


        [FindsBy(How = How.XPath, Using = ".//*[@resource-id='com.instagram.android:id/row_pending_container']")]
        public IWebElement PendingContainer;


        [FindsBy(How = How.XPath, Using = ".//*[ @resource-id='com.instagram.android:id/row_feed_comment_textview_layout']")]
        public IWebElement PostTitle;



    }
}
