using MobileTestTask.PageObjects.AndroidPageObjects;
using MobileTestTask.Utils;
using NUnit.Framework;
using System.Threading;

namespace MobileTestTask.Tests.Android
{
    public class Tests : CoreTestCase
    {
        [OneTimeSetUp]
        public void Setup()
        {
            System.Diagnostics.Debug.WriteLine("---------------OTSetup Tests ");
            MainTabActivityPage mtap = new MainTabActivityPage(_driver);
            WaitAndClick(mtap.GoInBtn,mtap);
                
            LoginActivityPage lap = new LoginActivityPage(_driver);
            Wait.WaitElement(lap.Login);
            lap.Login.Click(); 
                RandomTimeout();
            lap.Login.SendKeys(config.Android.InstagramCredential.login);

            lap.Password.Click();            
                RandomTimeout();
            lap.Password.SendKeys(config.Android.InstagramCredential.password);

            WaitAndClick(lap.GoInBtn);                
        }

        [TestCase(TestName = "01.To make a shoot by Camera")]
        public void Test1()
        {
            MainActivityPage mainActivityPage = new MainActivityPage(_driver);
            WaitAndClick(mainActivityPage.DoShootBtn);

            NewPublicationPage newPublicationPage = new NewPublicationPage(_driver);
            WaitAndClick(newPublicationPage.CamneraBtn, newPublicationPage);


            // после разрешения доступов истаграмм выкидывает на предыдущий экран. 
            // поэтому здесь такой костыль. 
            if( PermitRequestWait.WaitElement(newPublicationPage.PermitionAllowBtn, false))
            {
                while (PermitRequestWait.WaitElement(newPublicationPage.PermitionAllowBtn, false))
                {
                    newPublicationPage.PermitionAllowBtn.Click();
                }
                WaitAndClick(newPublicationPage.CamneraBtn, newPublicationPage);
            }

            //делаем снимок
            CameraPage cameraPage = new CameraPage(_driver);
            WaitAndClick(cameraPage.DoShootBtn, cameraPage);

            // применяем фильтр - самый первый - Clarendon
            FiltersPage filtersPage = new FiltersPage(_driver);
            WaitAndClick(filtersPage.ClarendonFilter);
            WaitAndClick(filtersPage.NextBtn);

            //
            NewPostPage newPostPage = new NewPostPage(_driver);
            Wait.WaitElement(newPostPage.Caption);
            newPostPage.Caption.SendKeys(config.Android.AndroidCapabilities.Udid + " " + TestContext.CurrentContext.Test.Name);
            WaitAndClick(newPostPage.NextBtn);

            System.Diagnostics.Debug.WriteLine("-----------------------------------");
            System.Diagnostics.Debug.WriteLine("------------------------ Приехали!!");
            System.Diagnostics.Debug.WriteLine("-----------------------------------");
        }


        [TestCase(TestName = "02. Select Image from Galery")]
        public void Test2()
        {
            MainActivityPage mainActivityPage = new MainActivityPage(_driver);
            //while (mainActivityPage.DoShootBtn == null) { Thread.Sleep(500); }
            WaitAndClick(mainActivityPage.DoShootBtn);

            NewPublicationPage newPublicationPage = new NewPublicationPage(_driver);
            WaitAndClick(newPublicationPage.SourceSelector, newPublicationPage);
            WaitAndClick(newPublicationPage.InstaTest, newPublicationPage);

            SelectImagePage selectImagePage = new SelectImagePage(_driver);
            WaitAndClick(selectImagePage.SelectedImageThumb);


            CropPage cropPage = new CropPage(_driver);
            WaitAndClick(cropPage.NextBtn);


            // дальше-дaльше... 
            FiltersPage filtersPage = new FiltersPage(_driver);
            WaitAndClick(filtersPage.ClarendonFilter);
            WaitAndClick(filtersPage.NextBtn);

            NewPostPage newPostPage = new NewPostPage(_driver);
            Wait.WaitElement(newPostPage.Caption);
            newPostPage.Caption.SendKeys(config.Android.AndroidCapabilities.Udid + " " + TestContext.CurrentContext.Test.Name);
            WaitAndClick(newPostPage.NextBtn);

            System.Diagnostics.Debug.WriteLine("----------------------------------");
            System.Diagnostics.Debug.WriteLine("------------------------ Поехали!!");
            System.Diagnostics.Debug.WriteLine("----------------------------------");
        }
    }
}
