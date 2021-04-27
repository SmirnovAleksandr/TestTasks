using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileTestTask.PageObjects.AndroidPageObjects;
using MobileTestTask.Utils;


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


                RandomTimeout();
            LoginActivityPage lap = new LoginActivityPage(_driver);
            Wait.WaitElement(lap.Login);
            lap.Login.Click();
            lap.Login.Clear();
                RandomTimeout();
            lap.Login.SendKeys("+79067348273");

            lap.Password.Click();
            lap.Password.Clear();
                RandomTimeout();
            lap.Password.SendKeys("3728qw");


            WaitAndClick(lap.GoInBtn);                
        }


        [TestCase]
        public void Test1()
        {
            MainActivityPage MAPage = new MainActivityPage(_driver);
            WaitAndClick(MAPage.DoShootBtn);

            NewPublicationPage NPPAge = new NewPublicationPage(_driver);
            WaitAndClick(NPPAge.CamneraBtn, NPPAge);

            // /делаем снимок
            CameraPage CPage = new CameraPage(_driver);
            WaitAndClick(CPage.DoShootBtn, CPage);

            // дальше-двльше... 




            System.Diagnostics.Debug.WriteLine("----------------------------------");
            System.Diagnostics.Debug.WriteLine("------------------------ Поехали!!");
            System.Diagnostics.Debug.WriteLine("----------------------------------");
        }





    }
}
