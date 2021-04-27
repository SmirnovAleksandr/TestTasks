using MobileTestTask.PageObjects.IOSPageObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTestTask.Tests.IOS
{
    [TestFixture]
    public class Tests : CoreTestCase 
    {
        private MainPageObject MainPageObject;  
        private ProductsPageObject ProductsPageObject;
        private User User;

        [SetUp]
        public void Setup()
        {        
            MainPageObject = new MainPageObject(Driver);
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

            Assert.That(ProductsPageObject.NumberOfProductsFound() > 0, "Не нашлось ни одного продукта");
          
        }

        

    }
}
