using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTestTask.PageObjects.IOSPageObjects
{
    public class MainPageObject
    //    public class BasePage:Page
    {
        protected AppiumDriver<IWebElement> _driver;

        public MainPageObject(AppiumDriver<IWebElement> driver)
        {
            _driver = driver;
#pragma warning disable CS0618 // Type or member is obsolete
            PageFactory.InitElements(driver, this);
#pragma warning restore CS0618 // Type or member is obsolete
        }


        public bool WaitForListOfElementsPresent(IList<IWebElement> elemList)
        {
            return WaitForListOfElementsPresent(elemList, 30);
        }

        public bool WaitForListOfElementsPresent(IList<IWebElement> elemList, int timeOutInSecond)
        {
            var currentTime = new SystemClock();
            var timeOut = currentTime.LaterBy(TimeSpan.FromSeconds(timeOutInSecond));

            while (currentTime.IsNowBefore(timeOut))
            {
                try
                {
                    if (elemList.Count > 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Возникло исключение при поиске списка: " + ex.Message);
                }
            }
            throw new TimeoutException("Наступил таймаут при поиске элемента. Необходимо проверить присутствует ли список" + nameof(elemList) + "на странице.");
        }

        public IWebElement WaitForElementPresent(IWebElement webElement, int timeoutnSeconds)
        {
            return new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutnSeconds)).Until(ExpectedConditions.ElementToBeClickable(webElement));

        }

        public IWebElement WaitForElementPresent(IWebElement webElement)
        {
            return WaitForElementPresent(webElement, 10);

        }

        public IWebElement WaitForElementAndClick(IWebElement webElement, int timeoutnSeconds)
        {

            var element = WaitForElementPresent(webElement, timeoutnSeconds);
            element.Click();
            return element;
        }

        public IWebElement LookForElementByNameInList(IList<IWebElement> elemsList, string name)
        {
            WaitForListOfElementsPresent(elemsList, 20);

            var expectedItem = elemsList.FirstOrDefault(el => el.Text.Equals(name));
            if (expectedItem != null && expectedItem.Displayed)
            {
                //System.Diagnostics.Debug.WriteLine("Found and returned: " + name);
                return expectedItem;
            }

            return SwipeUpToFindElement(elemsList, "Не удалось свапнуть к элементу", 10, name); ;
        }

        protected IWebElement SwipeUpToFindElement(IList<IWebElement> elementsList, string errorMessage, int maxSwipes, string name)
        {

            int alreadySwiped = 0;
            var locationElementForSearch = elementsList.FirstOrDefault(el => el.Text.Equals(name));

            while (locationElementForSearch == null || !locationElementForSearch.Displayed)
            {

                if (alreadySwiped > maxSwipes)
                {
                    throw new Exception("Элемент не нашелся по истечению максимального количества свайпов");
                }
                SwipeUpQuick();
                locationElementForSearch = elementsList.FirstOrDefault(el => el.Text.Contains(name));
                ++alreadySwiped;
            }

            return locationElementForSearch;
        }


        protected void SwipeUpQuick()
        {
            Swipe(0.5, 0.8, 0.5, 0.2, 1000);
        }

        protected void Swipe(double startX, double startY, double stopX, double stopY, int delay)
        {
            int Height = _driver.Manage().Window.Size.Height; //2392
            int Width = _driver.Manage().Window.Size.Width;  //1440
            int Starty = (int)(Height * startY);                   //886.5
            int Endy = (int)(Height * stopY);                 //1243
            int Startx = (int)(Width * startX);                   // 540
            int EndX = (int)(Width * stopX);                  // 540

            new TouchAction(_driver).Press(Startx, Starty).Wait(delay).MoveTo(Startx, Endy)
                        .Release().Perform();
        }

    }
}
