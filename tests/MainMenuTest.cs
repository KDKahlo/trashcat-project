using System;
using System.Threading;
using AltTester.AltTesterUnitySDK.Driver;
using trashcat_automation.pages;
using NUnit.Framework;

namespace trashcat_automation.tests
{

    public class MainMenuTest
    {
        AltDriver Driver;
        MainMenuPage MainMenuPage;

        //the Setup method tells the system this is needed first
        [OneTimeSetUp]

        public void SetUp()
        {
            Driver = new AltDriver(port: 13000);
            MainMenuPage = new MainMenuPage(Driver);

        }
        //clean up/tear down function for when testing is complete
        //this will stop the driver connection to the game. 
        [OneTimeTearDown]

        public void Dispose()
        {
            Driver.Stop();
            //this comes from System.Threading at top of page. It is needed for Threading.
            Thread.Sleep(100);
        }
        [Test, Order(1)]

        public void GetAllTextObjects()
        {
            MainMenuPage.FindTextObjects();
        }
        [Test, Order(2)]
        public void GetObjectContainsButton()
        {
            MainMenuPage.FindObjectContainsButton();
        }
        [Test, Order(3)]
        public void GetAllObjectsConatinButton()
        {
            MainMenuPage.FindAllObjectsWhichContainsButton();
        }
        [Test, Order(4)]
        public void GetImageAtScreenCoordinates()
        {
            MainMenuPage.FindImageAtCoordinates();
        }
        [Test, Order(5)]
        public void TestGetAllElements()
        {
            MainMenuPage.GetAllEnabledObjects();
        }
        [Test, Order(6)]
        public void VerifyLeaderBoardDisplayed()
        {
            MainMenuPage.IsLeaderBoardDisplayed();
        }
        [Test, Order(7)]
        public void VerifyStoreDisplayed()
        {
            MainMenuPage.IsStoreDisplayed();
        }
        [Test, Order(8)]
        public void VerifyMissionsDisplayed()
        {
            MainMenuPage.IsMissionsDisplayed();
        }

        [Test, Order(9)]
        public void VerifySettingsDisplayed()
        {
            MainMenuPage.IsSettingsDisplayed();
        }
        [Test, Order(10)]
        public void GetObjectWhichContainsButton()
        {
            MainMenuPage.StartButton.Click();
            MainMenuPage.WaitForObjectContainsButton();
        }
    }
}