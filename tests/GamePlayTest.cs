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
        //this test will click the start button and test the the player is invincible and doesn't collide with any objects
        //remember that you will need the method names from the developer or AltTester to call the methods correctly.
        [Test, Order(1)]
        public void ActivateCheat()
        {
            MainMenuPage.StartButton.Click();
            gamePlayPage.CheatInvincibleComponentMethod();
            Thread.Sleep(6000);// will wait for invincibility to activate
            gamePlayPage.JumpComponentMethod(gamePlayPage.PlayerPivot);
            Thread.Sleep(5000); // wait for jump to complete
            gamePlayPage.SlideComponentMethod(gamePlayPage.PlayerPivot);
            Thread.Sleep(5000); // wait for slide to complete
            gamePlayPage.MoveRightComponentMethod(gamePlayPage.PlayerPivot);
            Thread.Sleep(5000); // wait for move right to complete
            gamePlayPage.MoveLeftComponentMethod(gamePlayPage.PlayerPivot);
            Thread.Sleep(5000); // wait for move left to complete
            Console.WriteLine("Player Current Life: " + gamePlayPage.GetPlayerCurrentLife());
            Console.WriteLine("Player Current Speed: " + gamePlayPage.GetPlayerCurrentSpeed());
            Console.WriteLine("Player Current Lane: " + gamePlayPage.GetPlayerCurrentLane());
        }
       
    }
}