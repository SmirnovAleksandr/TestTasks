using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.PageObjects;

namespace MobileTestTask.PageObjects.IOSPageObjects
{
    public class ProductsPageObject : CorePageObject
    {
        public ProductsPageObject(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }


        [FindsBy(How = How.XPath, Using = "//XCUIElementTypeSearchField[@name='Ищите товары']")]
        public IWebElement SearchField;


        [FindsBy(How = How.XPath, Using = "//XCUIElementTypeCollectionView//XCUIElementTypeCell//XCUIElementTypeOther[descendant::XCUIElementTypeStaticText and XCUIElementTypeButton]")]
        public IList<IWebElement> SearchResults;

        //XCUIElementTypeSearchField[@name="Ищите товары"]


        public void InitSearchInput() {

            WaitForElementAndClick(SearchField, 5);
            WaitForElementPresent(SearchField, 5);
            
        }

        public void TypeSearchLine(string searchLine) {
            
                WaitForElementAndSendKeys(SearchField, "Nutella", 5);                   
        }


        public int NumberOfProductsFound() {

            return SearchResults.Count;
        }

    }
}
