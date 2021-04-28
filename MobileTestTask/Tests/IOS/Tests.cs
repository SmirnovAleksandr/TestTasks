using MobileTestTask.PageObjects.IOSPageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.MultiTouch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MobileTestTask.Tests.IOS
{
    [TestFixture]
    public class Tests : CoreTestCase 
    {
        private MainPageObject MainPageObject;  
        private ProductsPageObject ProductsPageObject;
        private MorePageObject MorePageObject;
        private User User;

        [SetUp]
        public void Setup()
        {        
            MainPageObject = new MainPageObject(Driver);
            MorePageObject = new MorePageObject(Driver);
            User = new User(Driver);
        }

        [Test]
        public void AuthorizeTest() {     

            if (User.IsLoggined()) {
                User.LogOut();
            }
            User.Authorize();
        }


        [Test]
        public void SearchProductsTest() {

            MainPageObject.WaitForElementAndClick(MainPageObject.GetMenuElement("Товары"), 10);         

            ProductsPageObject = new ProductsPageObject(Driver);
            ProductsPageObject.TypeSearchLine("Nutella");

            var keyboard = new Keyboard(Driver);
            keyboard.ClickButton(keyboard.Search);

            Assert.That(ProductsPageObject.NumberOfProductsFound() > 0, "No products found");
          
        }

        [Test]
        public void SwitchAppToBrowserAndBackToAppTest() {

            MainPageObject.ClickOnMenuItem("Отделения");
            //В приложении 2-4 баннера в разделе "Ещё". 2 из них точно не кликабельны и в какой-то момент они перестают отображаться \
            // В связи с этим переходим на страницу "Отделение и кликаем на иконку яндекс, в результате чего попадаем в браузер"

            var departmentPageObject = new DepartmentPageObject(Driver);
            departmentPageObject.ClickOnYandexMap(departmentPageObject.smallYandexMap);
            departmentPageObject.ClickOnYandexLogoByCoordinates(departmentPageObject.smallYandexMap, 360, 235);

            departmentPageObject.ClickOnYandexMap(departmentPageObject.bigYandexMap);
            departmentPageObject.ClickOnYandexLogoByCoordinates(departmentPageObject.smallYandexMap, 365, 490);
                                               
            Driver.ActivateApp("ru.russianpost.Russian-Post");
                       
        }

    }
}
