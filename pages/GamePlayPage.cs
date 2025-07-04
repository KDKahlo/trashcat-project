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
        //this function will give us access to current life property of the player
        public int GetPlayerCurrentLife()
        {
            return PlayerPivot.GetComponentProperty<int>("PlayerPivotInputController", "CurrentLife", "Assembly-CSharp");
        }
        //this function will give us access to current speed property of the player
        public float GetPlayerCurrentSpeed()
        {
            return PlayerPivot.UpdateObject().GetComponentProperty<float>("PlayerPivotInputController", "CurrentSpeed", "Assembly-CSharp");
        }
        //this function will give us access to current lane property of the player
        public int GetPlayerCurrentLane()
        {
            return PlayerPivot.GetComponentProperty<int>("PlayerPivotInputController", "CurrentLane", "Assembly-CSharp");
        }
        //this function will allow the player to avoid all obstables in the game
        public void AvoidAllObstacles()
        {
            var playerPivot = PlayerPivot;
            List<AltObject> allObstacles;
            List<AltObject> continueList = new List<AltObject>();
            HashSet<string> handledObstacles = new HashSet<string>();
            allObstacles = Driver.FindObjectsWhichContain(By.NAME, "Obstacle");
            allObstacles.Sort((x, y) => x.worldZ == y.worldZ ? x.worldX.CompareTo(y.worldX) : x.worldZ.CompareTo(y.worldZ));
            allObstacles.RemoveAll(obs => obs.worldZ < playerPivot.UpdateObject().worldZ);
            continueList.AddRange(allObstacles);
            //we need to make sure only new or unhandled obstacles are in the list. no duplicates
            foreach (var obs in allObstacles)
            {
                handledObstacles.Add(Convert.ToString(obs.id));
            }
        }
    }


}