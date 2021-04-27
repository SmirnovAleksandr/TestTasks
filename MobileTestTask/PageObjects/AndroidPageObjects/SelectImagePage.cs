using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace MobileTestTask.PageObjects.AndroidPageObjects
{
    public class SelectImagePage : BasePage
    {
        public SelectImagePage(AndroidDriver<AppiumWebElement> androidDriver) : base(androidDriver)
        {
        }

/*        [FindsBy(How = How.XPath, 
            Using = "(.//*[@resource-id='com.android.documentsui:id/dir_list']//*[@resource-id='android:id/title'])[1]")]
*/
        [FindsBy(How = How.XPath, 
            Using = "(//android.widget.CheckBox[@content-desc])[1]")]
        public IWebElement FirstImage;

        [FindsBy(How = How.XPath, 
            Using = ".//*[@resource-id='com.instagram.android:id/crop_image_view']")]
        public IWebElement SelectedImageThumb;

        


    }
}
