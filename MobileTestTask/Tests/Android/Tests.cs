using MobileTestTask.PageObjects.AndroidPageObjects;
using MobileTestTask.Utils;
using NUnit.Framework;
using System;
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

        [TestCase(1, TestName = "01.To make a shoot by Camera")]
        public void Test1( int n)
        {
            var PostTitle = config.Android.AndroidCapabilitiesList[CapabilitiesItem].DeviceName + " " +
                            config.Android.AndroidCapabilitiesList[CapabilitiesItem].Udid + " " +
                            TestContext.CurrentContext.Test.Name + 
                            DateTime.Now.ToString() ;

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

            // делаем снимок
            CameraPage cameraPage = new CameraPage(_driver);
            WaitAndClick(cameraPage.DoShootBtn, cameraPage);

            // применяем фильтр - самый первый - Clarendon
            FiltersPage filtersPage = new FiltersPage(_driver);
            WaitAndClick(filtersPage.ClarendonFilter);
            WaitAndClick(filtersPage.NextBtn);

            //
            NewPostPage newPostPage = new NewPostPage(_driver);
            Wait.WaitElement(newPostPage.Caption);            
            newPostPage.Caption.SendKeys(PostTitle);
            WaitAndClick(newPostPage.NextBtn);

            mainActivityPage = new MainActivityPage(_driver);
            Wait.WaitElement(mainActivityPage.PostTitle);
            var CreatedPostTitle = mainActivityPage.PostTitle.Text;
            Assert.That(CreatedPostTitle.Contains(PostTitle));

        }


        [TestCase(2, TestName = "02. Select Image from Galery")]
        public void Test2(int n)
        {
            var PostTitle = config.Android.AndroidCapabilitiesList[CapabilitiesItem].DeviceName + " " +
                config.Android.AndroidCapabilitiesList[CapabilitiesItem].Udid + " " +
                TestContext.CurrentContext.Test.Name +
                DateTime.Now.ToString();

            MainActivityPage mainActivityPage = new MainActivityPage(_driver);            
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
            newPostPage.Caption.SendKeys(PostTitle);
            WaitAndClick(newPostPage.NextBtn);


            mainActivityPage = new MainActivityPage(_driver);
            Wait.WaitElement(mainActivityPage.PostTitle);
            var CreatedPostTitle = mainActivityPage.PostTitle.Text;
            Assert.That(CreatedPostTitle.Contains(PostTitle));

        }
    }
}
