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
    public class NewPublicationPage : BasePage
    {
        public NewPublicationPage(AndroidDriver<AppiumWebElement> androidDriver) : base(androidDriver)
        {
        }

        [FindsBy(How = How.XPath, Using = ".//*[@resource-id='com.instagram.android:id/tab_icon']")]
        public IWebElement PLusBtn;

        [FindsBy(How = How.XPath, Using = ".//*[@resource-id='com.instagram.android:id/unified_camera_button']")]
        public IWebElement CamneraBtn;


        [FindsBy(How = How.XPath, Using = ".//*[@resource-id='com.instagram.android:id/spinner_title_text']")]
        public IWebElement SourceSelector;

        [FindsBy(How = How.XPath, 
            Using = ".//*[@resource-id='com.instagram.android:id/action_sheet_row_text_view' and contains(@text, '..' )]")]
        public IWebElement OtherChoice;

        [FindsBy(How = How.XPath,
            Using = ".//*[@resource-id='com.instagram.android:id/action_sheet_row_text_view' and contains(@text, 'InstaTest' )]")]
        public IWebElement InstaTest;
        

    }
}
