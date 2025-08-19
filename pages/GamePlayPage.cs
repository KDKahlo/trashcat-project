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
        public float GetPlayerCurrentSpeed(AltObject playerPivot)
        {
            return PlayerPivot.UpdateObject().GetComponentProperty<float>("PlayerPivotInputController", "CurrentSpeed", "Assembly-CSharp");
        }
        //this function will give us access to current lane property of the player
        public int GetPlayerCurrentLane(AltObject player)
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
        //this function will return the jump distance based on the current speed of the player
        public float JumpDistance(float speed)
        {
            if (speed > 18f)
            {
                return 8.0f;
            }

            else if (speed > 15f)
            {
                return 7.0f;
            }
            else if (speed > 14f)
            {
                return 6.0f;
            }
            else if (speed > 11f)
            {
                return 5.0f;
            }
            else if (speed > 10f)
            {
                return 4.5f;
            }
            else
            {
                return 5.0f;
            }
        }



        public float WrappedObstacleZ(AltObject obstacle)
        {
            float newObstacleWorldZ = 0.0f;
            if (obstacle.worldZ >= 100.0f)
            {
                newObstacleWorldZ = obstacle.worldZ % 100;
                Console.WriteLine($"Obstacle new higher worldZ : {newObstacleWorldZ}");
                return newObstacleWorldZ;
            }
            else if (obstacle.worldZ < 0.0f)
            {
                newObstacleWorldZ = obstacle.worldZ + 100;
                Console.WriteLine($"Obstacle new negative worldZ : {newObstacleWorldZ}");
                return newObstacleWorldZ;
            }
            else if (obstacle.worldZ > 0.0f && obstacle.worldZ < 100.0f) 
            {
                newObstacleWorldZ = obstacle.worldZ;
                Console.WriteLine($"Obstacle new worldZ: {newObstacleWorldZ}");
                return newObstacleWorldZ;
            }
            else
            {
                Console.WriteLine("Obstacle worldZ is not in the expected range.");
                return newObstacleWorldZ; 
            }
        }

        public void HandleObstacleBeyondZero(AltObject obstacle, AltObject player)
        {
            var newObstacleWorldZ = WrappedObstacleZ(obstacle);
            if (newObstacleWorldZ < player.UpdateObject().worldZ)
            {
                while (player.UpdateObject().worldZ >= 0f)
                {
                    Console.WriteLine("Obstacle is beyond zero, waiting for player to move forward.");
                    Console.WriteLine($"Obstacle worldZ: {newObstacleWorldZ - player.UpdateObject().worldZ}");
                    if (player.UpdateObject().worldZ >= 0f)
                    {
                        Console.WriteLine("Player is moving forward, obstacle is now in range.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Player is not moving forward, obstacle is still beyond zero.");
                        System.Threading.Thread.Sleep(1000); // Wait for a second before checking again
                    }
                }
            }
            if (newObstacleWorldZ >= player.UpdateObject().worldZ)
            {
                while (player.UpdateObject().worldZ >= 0f)
                {
                    Console.WriteLine("Obstacle is not beyond zero, player is moving forward.");
                    Console.WriteLine($"Obstacle worldZ: {newObstacleWorldZ - player.UpdateObject().worldZ}");
                    if (player.UpdateObject().worldZ >= 0f)
                    {
                        Console.WriteLine("Player is moving forward, obstacle is now in range.");
                        break;
                    }

                }
            }
            if (newObstacleWorldZ > player.UpdateObject().worldZ){
                while (GetWrappedDistance(player.UpdateObject().worldZ, newObstacleWorldZ) > 3f && GetPlayerCurrentLife() != 0)
                {
                Console.WriteLine("Obstacle is beyond zero, waiting for player to move forward.");
                Console.WriteLine($"Obstacle worldZ: {newObstacleWorldZ - player.UpdateObject().worldZ}");
             } 
            }

        }
        public float GetWrappedDistance(float playerWorldZ, float target)
        {
            const float maxZ = 100f;
            float directDisance = target - playerWorldZ;
            if (directDisance < 0)
            {
                directDisance += maxZ;
            }
            float wrappedDistance = maxZ - directDisance;
            return Math.Min(directDisance, wrappedDistance);
        }
        //this function will allow the player to avoid all obstables in the game

        public void PlayerCrossesTheObstacle(AltObject obstacle, AltObject player)
        {
            var newObstacleWorldZ = WrappedObstacleZ(obstacle);
            while (GetWrappedDistance(player.UpdateObject().worldZ, newObstacleWorldZ) > 3f && GetPlayerCurrentLife() != 0)
            {
                if (newObstacleWorldZ < player.UpdateObject().worldZ)
                {
                    Console.WriteLine($"Obstacle hits while moving. Continuing to move forward.");
                    break;
                }
               
            }
        }
        public void AvoidAllObstacles()
        {
            var player = PlayerPivot;
            List<AltObject> allObstacles;
            List<AltObject> continueList = new List<AltObject>();
            HashSet<string> handledObstacles = new HashSet<string>();
            allObstacles = Driver.FindObjectsWhichContain(By.NAME, "Obstacle");
            allObstacles.Sort((x, y) => x.worldZ == y.worldZ ? x.worldX.CompareTo(y.worldX) : x.worldZ.CompareTo(y.worldZ));
            allObstacles.RemoveAll(obs => obs.worldZ < player.UpdateObject().worldZ);
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
                var life = GetPlayerCurrentLife();
                AltObject? nextObstacle;
                Console.WriteLine($"List count: {continueList.Count}");
                FetchingObstacles(k, continueList, allObstacles, handledObstacles);
                var obstacle = continueList[k];
                nextObstacle = continueList[k + 1];
                AltObject? nextToNextObstacle = k < continueList.Count - 2 ? continueList[k + 2] : null;
                Console.WriteLine($"Current Obstacle name: {obstacle.name}");
                Console.WriteLine($"current Obstacle id: {obstacle.id}");
                Console.WriteLine($"current Obstacle index: {k}");
                Console.WriteLine($"current Obstacle X value: {obstacle.worldX}");
                Console.WriteLine($"character current speed: {GetPlayerCurrentSpeed(player)}");

                var jumpDistance = JumpDistance(GetPlayerCurrentSpeed(player));

                if (obstacle.name == "ObstacleRat(Clone)" || obstacle.name == "ObstacleLowBarrier(Clone)" || obstacle.name == "ObstacleHighBarrier(Clone)")
                {
                    var newObstacleWorldZ = WrappedObstacleZ(obstacle);
                    float distance = (newObstacleWorldZ - player.UpdateObject().worldZ + 100) % 100;

                    if (distance > 0 && distance <= 90)
                    {
                        while (distance >= jumpDistance)
                        {
                            Console.WriteLine("Approach upcoming obstacle");
                            distance = (newObstacleWorldZ - player.UpdateObject().worldZ + 100) % 100;
                        }
                    }
                    if (distance < jumpDistance && distance > 0)
                    {
                        switch (obstacle.name)
                        {
                            case "ObstacleRat(Clone)":
                                Console.WriteLine("Jumping over Rat obstacle");
                                JumpComponentMethod(player);
                                break;
                            case "ObstacleLowBarrier(Clone)":
                                Console.WriteLine("Jumping over Low Barrier obstacle");
                                JumpComponentMethod(player);
                                break;
                            case "ObstacleHighBarrier(Clone)":
                                Console.WriteLine("sliding under high barrier obstacle");
                                SlideComponentMethod(player);
                                break;
                            default:
                                Console.WriteLine("Unknown obstacle type, cannot jump or slide.");
                                break;
                        }
                    }
                    if (life == GetPlayerCurrentLife())
                    {
                        HandleObstacleBeyondZero(obstacle, player.UpdateObject());
                        if (GetPlayerCurrentLife() == 0)
                        {
                            Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                            break;
                        }
                        continue;
                    }
                    else if (life != GetPlayerCurrentLife())
                    {
                        PlayerCrossesTheObstacle(obstacle.UpdateObject(), player.UpdateObject());
                        if (GetPlayerCurrentLife() == 0)
                        {
                            Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                            break;
                        }
                        continue;
                    }
                    else if (GetPlayerCurrentLife() == 0)
                    {
                        Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                        break;
                    }
                }

                else if (life != GetPlayerCurrentLife())
                {
                    HandleObstacleBeyondZero(obstacle, player.UpdateObject());
                    if (GetPlayerCurrentLife() == 0)
                    {
                        Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                        break;
                    }
                    continue;
                }

                else if (obstacle.name == "ObstacleDog(Clone)" && GetPlayerCurrentLife() > 0)
                {
                    var newObstacleWorldZ = WrappedObstacleZ(obstacle);
                    //this if statement will check if player is in the middle lane where x is 0
                    if (GetPlayerCurrentLane(player) == 0)
                    {
                        if (obstacle.worldX == -1.5f)
                        {
                        float distance = (newObstacleWorldZ - player.UpdateObject().worldZ + 100) % 100;
                            if (distance > 0f && distance <= 90f)
                            {
                                while (distance >= 18f)
                                {
                                    Console.WriteLine("Approach upcoming obstacle");
                                    distance = (newObstacleWorldZ - player.UpdateObject().worldZ + 100) % 100;
                                }
                                if (distance < 18f && distance > 0f)
                                {
                                    Console.WriteLine("Obstaccle is close, moving right to avoid Dog obstacle");
                                    MoveRightComponentMethod(player);

                                    if (life == GetPlayerCurrentLife())
                                    {
                                        HandleObstacleBeyondZero(obstacle, player.UpdateObject());
                                        if (GetPlayerCurrentLife() == 0)
                                        {
                                            Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                                            break;
                                        }
                                        continue;
                                    }
                                    else if (life != GetPlayerCurrentLife())
                                    {
                                        PlayerCrossesTheObstacle(obstacle.UpdateObject(), player.UpdateObject());
                                        if (GetPlayerCurrentLife() == 0)
                                        {
                                            Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                                            break;
                                        }
                                        continue;
                                    }
                                    else if (GetPlayerCurrentLife() == 0)
                                    {
                                        Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                                        break;
                                    }
                                    continue;
                                }
                         else if (life != GetPlayerCurrentLife())
                {
                    HandleObstacleBeyondZero(obstacle, player.UpdateObject());
                    if (GetPlayerCurrentLife() == 0)
                    {
                        Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                        break;
                    }
                    continue;
                }
                    }
                        }
                        else if (obstacle.worldX == 0f)
                        {
                        float distance = (newObstacleWorldZ - player.UpdateObject().worldZ + 100) % 100;
                            if (distance > 0f && distance <= 90f)
                            {
                                while (distance >= 18f)
                                {
                                    Console.WriteLine("Approach upcoming obstacle");
                                    distance = (newObstacleWorldZ - player.UpdateObject().worldZ + 100) % 100;
                                }
                                if (distance < 18f && distance > 0f)
                                {
                                    Console.WriteLine("Obstacle is not in the path");
                                   // MoveRightComponentMethod(player);

                                    if (life == GetPlayerCurrentLife())
                                    {
                                        HandleObstacleBeyondZero(obstacle, player.UpdateObject());
                                        if (GetPlayerCurrentLife() == 0)
                                        {
                                            Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                                            break;
                                        }
                                        continue;
                                    }
                                    else if (life != GetPlayerCurrentLife())
                                    {
                                        PlayerCrossesTheObstacle(obstacle.UpdateObject(), player.UpdateObject());
                                        if (GetPlayerCurrentLife() == 0)
                                        {
                                            Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                                            break;
                                        }
                                        continue;
                                    }
                                    else if (GetPlayerCurrentLife() == 0)
                                    {
                                        Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                                        break;
                                    }
                                    continue;
                                }
                         else if (life != GetPlayerCurrentLife())
                {
                    HandleObstacleBeyondZero(obstacle, player.UpdateObject());
                    if (GetPlayerCurrentLife() == 0)
                    {
                        Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                        break;
                    }
                    continue;
                }
                    }
                        }
                        else if (obstacle.worldX == 1.5f)
                        {
                        float distance = (newObstacleWorldZ - player.UpdateObject().worldZ + 100) % 100;
                            if (distance > 0f && distance <= 90f)
                            {
                                while (distance >= 18f)
                                {
                                    Console.WriteLine("Approach upcoming obstacle");
                                    distance = (newObstacleWorldZ - player.UpdateObject().worldZ + 100) % 100;
                                }
                                if (distance < 18f && distance > 0f)
                                {
                                    Console.WriteLine("Obstacle is not on the path");
                                    //MoveRightComponentMethod(player);

                                    if (life == GetPlayerCurrentLife())
                                    {
                                        HandleObstacleBeyondZero(obstacle, player.UpdateObject());
                                        if (GetPlayerCurrentLife() == 0)
                                        {
                                            Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                                            break;
                                        }
                                        continue;
                                    }
                                    else if (life != GetPlayerCurrentLife())
                                    {
                                        PlayerCrossesTheObstacle(obstacle.UpdateObject(), player.UpdateObject());
                                        if (GetPlayerCurrentLife() == 0)
                                        {
                                            Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                                            break;
                                        }
                                        continue;
                                    }
                                    else if (GetPlayerCurrentLife() == 0)
                                    {
                                        Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                                        break;
                                    }
                                    continue;
                                }
                         else if (life != GetPlayerCurrentLife())
                {
                    HandleObstacleBeyondZero(obstacle, player.UpdateObject());
                    if (GetPlayerCurrentLife() == 0)
                    {
                        Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                        break;
                    }
                    continue;
                }
                    }
                        }
                    }
                    //this if statement will check is the player is in the left lane where x is -1.5
                    else if (GetPlayerCurrentLane(player) == 1)
                    {
                      if (obstacle.worldX == -1.5f)
                        {
                        float distance = (newObstacleWorldZ - player.UpdateObject().worldZ + 100) % 100;
                            if (distance > 0f && distance <= 90f)
                            {
                                while (distance >= 18f)
                                {
                                    Console.WriteLine("Approach upcoming obstacle");
                                    distance = (newObstacleWorldZ - player.UpdateObject().worldZ + 100) % 100;
                                }
                                if (distance < 18f && distance > 0f)
                                {
                                    Console.WriteLine("Obstacle is out of player path");
                                    //MoveRightComponentMethod(player);

                                    if (life == GetPlayerCurrentLife())
                                    {
                                        HandleObstacleBeyondZero(obstacle, player.UpdateObject());
                                        if (GetPlayerCurrentLife() == 0)
                                        {
                                            Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                                            break;
                                        }
                                        continue;
                                    }
                                    else if (life != GetPlayerCurrentLife())
                                    {
                                        PlayerCrossesTheObstacle(obstacle.UpdateObject(), player.UpdateObject());
                                        if (GetPlayerCurrentLife() == 0)
                                        {
                                            Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                                            break;
                                        }
                                        continue;
                                    }
                                    else if (GetPlayerCurrentLife() == 0)
                                    {
                                        Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                                        break;
                                    }
                                    continue;
                                }
                         else if (life != GetPlayerCurrentLife())
                {
                    HandleObstacleBeyondZero(obstacle, player.UpdateObject());
                    if (GetPlayerCurrentLife() == 0)
                    {
                        Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                        break;
                    }
                    continue;
                }
                    }
                        }
                        else if (obstacle.worldX == 0f)
                        {
                        float distance = (newObstacleWorldZ - player.UpdateObject().worldZ + 100) % 100;
                            if (distance > 0f && distance <= 90f)
                            {
                                while (distance >= 18f)
                                {
                                    Console.WriteLine("Approach upcoming obstacle");
                                    distance = (newObstacleWorldZ - player.UpdateObject().worldZ + 100) % 100;
                                }
                                if (distance < 18f && distance > 0f)
                                {
                                    Console.WriteLine("Obstacle is in player's path, move left to avoid Dog obstacle");
                                    MoveLeftComponentMethod(player);

                                    if (life == GetPlayerCurrentLife())
                                    {
                                        HandleObstacleBeyondZero(obstacle, player.UpdateObject());
                                        if (GetPlayerCurrentLife() == 0)
                                        {
                                            Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                                            break;
                                        }
                                        continue;
                                    }
                                    else if (life != GetPlayerCurrentLife())
                                    {
                                        PlayerCrossesTheObstacle(obstacle.UpdateObject(), player.UpdateObject());
                                        if (GetPlayerCurrentLife() == 0)
                                        {
                                            Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                                            break;
                                        }
                                        continue;
                                    }
                                    else if (GetPlayerCurrentLife() == 0)
                                    {
                                        Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                                        break;
                                    }
                                    continue;
                                }
                         else if (life != GetPlayerCurrentLife())
                {
                    HandleObstacleBeyondZero(obstacle, player.UpdateObject());
                    if (GetPlayerCurrentLife() == 0)
                    {
                        Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                        break;
                    }
                    continue;
                }
                    }
                        }
                        else if (obstacle.worldX == 1.5f)
                        {
                        float distance = (newObstacleWorldZ - player.UpdateObject().worldZ + 100) % 100;
                            if (distance > 0f && distance <= 90f)
                            {
                                while (distance >= 18f)
                                {
                                    Console.WriteLine("Approach upcoming obstacle");
                                    distance = (newObstacleWorldZ - player.UpdateObject().worldZ + 100) % 100;
                                }
                                if (distance < 18f && distance > 0f)
                                {
                                    Console.WriteLine("Obstacle is not on the path");
                                    //MoveRightComponentMethod(player);

                                    if (life == GetPlayerCurrentLife())
                                    {
                                        HandleObstacleBeyondZero(obstacle, player.UpdateObject());
                                        if (GetPlayerCurrentLife() == 0)
                                        {
                                            Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                                            break;
                                        }
                                        continue;
                                    }
                                    else if (life != GetPlayerCurrentLife())
                                    {
                                        PlayerCrossesTheObstacle(obstacle.UpdateObject(), player.UpdateObject());
                                        if (GetPlayerCurrentLife() == 0)
                                        {
                                            Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                                            break;
                                        }
                                        continue;
                                    }
                                    else if (GetPlayerCurrentLife() == 0)
                                    {
                                        Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                                        break;
                                    }
                                    continue;
                                }
                         else if (life != GetPlayerCurrentLife())
                {
                    HandleObstacleBeyondZero(obstacle, player.UpdateObject());
                    if (GetPlayerCurrentLife() == 0)
                    {
                        Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                        break;
                    }
                    continue;
                }
                    }
                        }
                    }
                    //this if statement will check if player is in the right lane where x is 1.5
                    else if (GetPlayerCurrentLane(player) == 2)
                    {
                     if (obstacle.worldX == -1.5f)
                        {
                        float distance = (newObstacleWorldZ - player.UpdateObject().worldZ + 100) % 100;
                            if (distance > 0f && distance <= 90f)
                            {
                                while (distance >= 18f)
                                {
                                    Console.WriteLine("Approach upcoming obstacle");
                                    distance = (newObstacleWorldZ - player.UpdateObject().worldZ + 100) % 100;
                                }
                                if (distance < 18f && distance > 0f)
                                {
                                    Console.WriteLine("Obstacle is not in player's path");
                                    //MoveRightComponentMethod(player);

                                    if (life == GetPlayerCurrentLife())
                                    {
                                        HandleObstacleBeyondZero(obstacle, player.UpdateObject());
                                        if (GetPlayerCurrentLife() == 0)
                                        {
                                            Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                                            break;
                                        }
                                        continue;
                                    }
                                    else if (life != GetPlayerCurrentLife())
                                    {
                                        PlayerCrossesTheObstacle(obstacle.UpdateObject(), player.UpdateObject());
                                        if (GetPlayerCurrentLife() == 0)
                                        {
                                            Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                                            break;
                                        }
                                        continue;
                                    }
                                    else if (GetPlayerCurrentLife() == 0)
                                    {
                                        Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                                        break;
                                    }
                                    continue;
                                }
                         else if (life != GetPlayerCurrentLife())
                {
                    HandleObstacleBeyondZero(obstacle, player.UpdateObject());
                    if (GetPlayerCurrentLife() == 0)
                    {
                        Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                        break;
                    }
                    continue;
                }
                    }
                        }
                        else if (obstacle.worldX == 0f)
                        {
                        float distance = (newObstacleWorldZ - player.UpdateObject().worldZ + 100) % 100;
                            if (distance > 0f && distance <= 90f)
                            {
                                while (distance >= 18f)
                                {
                                    Console.WriteLine("Approach upcoming obstacle");
                                    distance = (newObstacleWorldZ - player.UpdateObject().worldZ + 100) % 100;
                                }
                                if (distance < 18f && distance > 0f)
                                {
                                    Console.WriteLine("Obstacle is not in the path");
                                   // MoveRightComponentMethod(player);

                                    if (life == GetPlayerCurrentLife())
                                    {
                                        HandleObstacleBeyondZero(obstacle, player.UpdateObject());
                                        if (GetPlayerCurrentLife() == 0)
                                        {
                                            Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                                            break;
                                        }
                                        continue;
                                    }
                                    else if (life != GetPlayerCurrentLife())
                                    {
                                        PlayerCrossesTheObstacle(obstacle.UpdateObject(), player.UpdateObject());
                                        if (GetPlayerCurrentLife() == 0)
                                        {
                                            Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                                            break;
                                        }
                                        continue;
                                    }
                                    else if (GetPlayerCurrentLife() == 0)
                                    {
                                        Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                                        break;
                                    }
                                    continue;
                                }
                         else if (life != GetPlayerCurrentLife())
                {
                    HandleObstacleBeyondZero(obstacle, player.UpdateObject());
                    if (GetPlayerCurrentLife() == 0)
                    {
                        Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                        break;
                    }
                    continue;
                }
                    }
                        }
                        else if (obstacle.worldX == 1.5f)
                        {
                        float distance = (newObstacleWorldZ - player.UpdateObject().worldZ + 100) % 100;
                            if (distance > 0f && distance <= 90f)
                            {
                                while (distance >= 18f)
                                {
                                    Console.WriteLine("Approach upcoming obstacle");
                                    distance = (newObstacleWorldZ - player.UpdateObject().worldZ + 100) % 100;
                                }
                                if (distance < 18f && distance > 0f)
                                {
                                    Console.WriteLine("Obstacle is in player's path, move left");
                                    MoveLeftComponentMethod(player);

                                    if (life == GetPlayerCurrentLife())
                                    {
                                        HandleObstacleBeyondZero(obstacle, player.UpdateObject());
                                        if (GetPlayerCurrentLife() == 0)
                                        {
                                            Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                                            break;
                                        }
                                        continue;
                                    }
                                    else if (life != GetPlayerCurrentLife())
                                    {
                                        PlayerCrossesTheObstacle(obstacle.UpdateObject(), player.UpdateObject());
                                        if (GetPlayerCurrentLife() == 0)
                                        {
                                            Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                                            break;
                                        }
                                        continue;
                                    }
                                    else if (GetPlayerCurrentLife() == 0)
                                    {
                                        Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                                        break;
                                    }
                                    continue;
                                }
                         else if (life != GetPlayerCurrentLife())
                {
                    HandleObstacleBeyondZero(obstacle, player.UpdateObject());
                    if (GetPlayerCurrentLife() == 0)
                    {
                        Console.WriteLine("Player has lost all lives, exiting obstacle avoidance.");
                        break;
                    }
                    continue;
                }
                    }
                        }
                    }
                }
            }
         
        }

    }


}