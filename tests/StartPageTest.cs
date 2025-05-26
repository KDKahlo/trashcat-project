using System;
using System.Threading;
using AltTester.AltTesterUnitySDK.Driver;
using trashcat_automation.pages;
using NUnit.Framework;

namespace trashcat_automation.tests
{

    public class StartPageTest
    {
        AltDriver Driver;
        MainMenuPage startPage;

        //the Setup method tells the system this is needed first
        [SetUp]

        public void SetUp()
        {
            Driver = new AltDriver(port: 13000);
            startPage = new MainMenuPage(Driver);

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

        public void VerifyStartScene()
        {
            string sceneName = Driver.GetCurrentScene();
            Assert.That(sceneName, Is.EqualTo("Start"));
        }
    }
}