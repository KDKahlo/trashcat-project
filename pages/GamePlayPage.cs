using System;
using System.Linq.Expressions;
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

        //this function will fetch the current obstacle and the 2nd and 3rd obstacles in the list.
        public void FetchingObstacles(int k, List<AltObject> continueList, List<AltObject> allObstacles, HashSet<string> handledObstacles)
        {
            for (int i = k + 3; i >= k; i--)
            {
                try
                {
                    if (continueList[i] == null)
                    {
                        Console.WriteLine($"Obstacle at index {i} is null.");
                    }
                    else if (continueList[i] != null)
                    {
                        break;
                    }
                }
                catch (System.ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    try
                    {
                        if (i - 1 >= 0 && continueList[i - 1] == null)
                        {
                            Console.WriteLine($"Obstacle at index {i - 1} is also null and continuing to the next iteration.");
                        }
                        else if (continueList[i - 1] != null)
                        {
                            Console.WriteLine($"Obstacle at index {i - 1} is not null and fetching new obstacle.");
                            allObstacles = Driver.FindObjectsWhichContain(By.NAME, "Obstacle");
                            Console.WriteLine("Fetching Obstacles");

                            while (allObstacles.Count <= 3) ;
                            try
                            {
                                allObstacles.Sort((x, y) => x.worldZ == y.worldZ ? x.worldX.CompareTo(y.worldX) : x.worldZ.CompareTo(y.worldZ));
                                //this code will filter the null valued obstacles already present in the handledObstables list, ensuring only new obstacles are added.  
                                var filterObstacles = allObstacles.Where(obs => obs != null && !handledObstacles.Contains(Convert.ToString(obs.id))).ToList();

                                if (filterObstacles.Count > 0)
                                {
                                    continueList.AddRange(filterObstacles);
                                    Console.WriteLine($"Added {filterObstacles.Count} new obstacles to the continue list.");

                                    foreach (var obs in filterObstacles)
                                    {
                                        handledObstacles.Add(Convert.ToString(obs.id));
                                        Console.WriteLine($"Obstacle {filterObstacles.Count} added to handled obstacles.");
                                        break;
                                    }
                                }
                                else if (filterObstacles.Count == 0)
                                {
                                    Console.WriteLine("No new obstacles found to add to the continue list, need to fetch again.");
                                    FetchingObstacles(i, continueList, allObstacles, handledObstacles);
                                }
                                                                 
        
                            }
                            catch (System.Exception processingEx)
                            {
                                Console.WriteLine($"An error occurred while processing obstacles: {processingEx.Message}");
                                // Optionally, you can rethrow the exception or handle it as needed
                                throw;
                            }
                            
                        }

                    }
                    catch (System.Exception)
                    {
                       continue;
                    }
                    
                }
            }
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
            //this part of the function will loop through the current list of obstacles 
            //and track the current obstacle and the next obstacle
            for (int k = 0; k < continueList.Count; k++)
            {
                // AltObject? nextObstacle;
                Console.WriteLine($"List count: {continueList.Count}");
                FetchingObstacles(k, continueList, allObstacles, handledObstacles);
            }


        }


        //this function will track both the current obstacle and the next obstacle on the list

        //this function will check if game play in current list is nearing its end

    }


}