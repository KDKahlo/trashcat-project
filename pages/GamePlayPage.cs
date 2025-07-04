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
            PlayerPivot.CallComponentMethod<string>("PlayerPivotInputController", "CheatInvincible", "Assembly-CSharp", new object[] { "true" });
        }
        //component method jump
        public void JumpComponentMethod(AltObject PlayerPivot)
        {
            PlayerPivot.CallComponentMethod<string>("PlayerPivotInputController", "Jump", "Assembly-CSharp", new object[] { });
        }
        //component method slide
        public void SlideComponentMethod(AltObject PlayerPivot)
        {
            PlayerPivot.CallComponentMethod<string>("PlayerPivotInputController", "Slide", "Assembly-CSharp", new object[] { });
        }
        //component method moveright
        public void MoveRightComponentMethod(AltObject PlayerPivot)
        {
            PlayerPivot.CallComponentMethod<string>("PlayerPivotInputController", "MoveRight", "Assembly-CSharp", new object[] { "1" });
        }
        //component method moveleft
        public void MoveLeftComponentMethod(AltObject PlayerPivot)
        {
            PlayerPivot.CallComponentMethod<string>("PlayerPivotInputController", "MoveLeft", "Assembly-CSharp", new object[] { "-1" });

        }
    }

}