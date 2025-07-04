using System;
using System.Reflection.PortableExecutable;
using AltTester.AltTesterUnitySDK.Driver;

namespace trashcat_automation.pages
{
    public class GamePlayPage : BasePage
    {
        public GamePlayPage(AltDriver driver) : base(driver)
        {
        }
        //Find player Pivot Character AltObject
        public AltObject PlayerPivot => Driver.WaitForObject(By.NAME, "PlayerPivot");

        //Component method cheat invincible
        public void CheatInvincibleComponentMethod()
        {
            PlayerPivot.CallComponentMethod<string>("PlayerPivotInputController","CheatInvincible", "Assembly-CSharp", new object[] {"true"});
        }
    }
}

        