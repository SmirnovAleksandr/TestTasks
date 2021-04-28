using MobileTestTask.PageObjects.AndroidPageObjects;
using MobileTestTask.Utils;
using NUnit.Framework;


namespace MobileTestTask.Tests.Android
{
    public class Tests : CoreTestCase
    {
        [SetUp]
        public void Setup()
        {
            // Login to Instagramm
            MainTabActivityPage mtap = new MainTabActivityPage(_driver);

            WaitAndClick(mtap.GoInBtn,mtap);
                
            LoginActivityPage lap = new LoginActivityPage(_driver);
            Wait.WaitElement(lap.Login);
            lap.Login.Click();
            //lap.Login.Clear();
                RandomTimeout();
            lap.Login.SendKeys("+79067348273");

            lap.Password.Click();
            //lap.Password.Clear();
                RandomTimeout();
            lap.Password.SendKeys("3728qw");

            WaitAndClick(lap.GoInBtn);                
        }

        [TestCase(TestName = "01.To make a shoot by Camera")]
        public void Test1()
        {
            MainActivityPage mainActivityPage = new MainActivityPage(_driver);
            WaitAndClick(mainActivityPage.DoShootBtn);

            NewPublicationPage newPublicationPage = new NewPublicationPage(_driver);
            WaitAndClick(newPublicationPage.CamneraBtn, newPublicationPage);


            // после разрешения доступов истаграмм выкидывает на предыдущйи экран. 
            // поэтому такой костыль. 
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

            // дальше-дaльше... 
            FiltersPage filtersPage = new FiltersPage(_driver);
            WaitAndClick(filtersPage.ClarendonFilter);
            WaitAndClick(filtersPage.NextBtn);


            NewPostPage newPostPage = new NewPostPage(_driver);
            WaitAndClick(newPostPage.NextBtn);
/*
            mainActivityPage = new MainActivityPage(_driver);
            var tt = mainActivityPage.PendingContainer.Text;
            System.Diagnostics.Debug.WriteLine(" -------------  MAPage.PendingContainer.Text  is" + tt);
*/
            System.Diagnostics.Debug.WriteLine("-----------------------------------");
            System.Diagnostics.Debug.WriteLine("------------------------ Приехали!!");
            System.Diagnostics.Debug.WriteLine("-----------------------------------");
        }


        [TestCase(TestName = "02. Select Image from Galery")]
        public void Test2()
        {
            MainActivityPage mainActivityPage = new MainActivityPage(_driver);
            WaitAndClick(mainActivityPage.DoShootBtn);

            NewPublicationPage newPublicationPage = new NewPublicationPage(_driver);
            WaitAndClick(newPublicationPage.SourceSelector, newPublicationPage);
            WaitAndClick(newPublicationPage.InstaTest, newPublicationPage);


            /*А тут нам надо выбрать фото - мы берём первое*/


            /*            WaitAndClick(newPublicationPage.CamneraBtn, newPublicationPage);


                        // после разрешения доступов истаграмм выкидывает на предыдущйи экран. 
                        // поэтому такой костыль. 
                        if (PermitRequestWait.WaitElement(newPublicationPage.PermitionAllowBtn, false))
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
            */

            SelectImagePage selectImagePage = new SelectImagePage(_driver);
            WaitAndClick(selectImagePage.SelectedImageThumb);


            CropPage cropPage = new CropPage(_driver);
            WaitAndClick(cropPage.NextBtn);


            // дальше-дaльше... 
            FiltersPage filtersPage = new FiltersPage(_driver);
            WaitAndClick(filtersPage.ClarendonFilter);
            WaitAndClick(filtersPage.NextBtn);


            NewPostPage newPostPage = new NewPostPage(_driver);
            WaitAndClick(newPostPage.NextBtn);
/*
            mainActivityPage = new MainActivityPage(_driver);
            var tt = mainActivityPage.PendingContainer.Text;
            System.Diagnostics.Debug.WriteLine(" -------------  MAPage.PendingContainer.Text  is" + tt);
*/
            System.Diagnostics.Debug.WriteLine("----------------------------------");
            System.Diagnostics.Debug.WriteLine("------------------------ Поехали!!");
            System.Diagnostics.Debug.WriteLine("----------------------------------");
        }


    }
}
