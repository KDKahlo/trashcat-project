using System;
using System.Threading;
using AltTester.AltTesterUnitySDK.Driver;
using trashcat_automation.pages;
using NUnit.Framework;

namespace trashcat_automation.tests
{

    public class GamePlayTest
    {
        AltDriver Driver;
        MainMenuPage MainMenuPage;

        GamePlayPage gamePlayPage;

        //the Setup method tells the system this is needed first
        [OneTimeSetUp]

        public void SetUp()
        {
            Driver = new AltDriver(port: 13000);
            MainMenuPage = new MainMenuPage(Driver);
            gamePlayPage = new GamePlayPage(Driver);

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
       
    }
}