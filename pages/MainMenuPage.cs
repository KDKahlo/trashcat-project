
//Foundation class basepage this gives access to the namespaces classes and methods
using System.Runtime.CompilerServices;
using AltTester.AltTesterUnitySDK.Driver;


namespace trashcat_automation.pages
{
    public class MainMenuPage : BasePage
    {
        //defines constructor for MainMenuPage. and extends base driver
        public MainMenuPage(AltDriver driver) : base(driver)
        {

        }
        //property methods section -- methods for pointers to find different objects in the game.
        //this will point the the startbutton at the start of the game, finding it by the name.
        public AltObject StartButton { get => Driver.FindObject(By.NAME, "StartButton"); }
        //this will point the the start text at the start of the game, finding it by the name.     
        public AltObject StartText { get => Driver.FindObject(By.NAME, "START"); }
        //this will point the the leader board, finding it by the path.
        public AltObject LeaderBoard { get => Driver.WaitForObject(By.PATH, "/UICamera/Loadout/OpenLeaderBoard", timeout: 30); }

        //this will point to the Store, finding it by the path.
        public AltObject Store { get => Driver.WaitForObject(By.PATH, "/UICamera/Loadout/StoreButton", timeout: 30); }

        //this will point to the Missions, finding it by the path.
        public AltObject Missions { get => Driver.WaitForObject(By.PATH, "/UICamera/Loadout/MissionsButton", timeout: 30); }

        //this will point to the Settings, finding it by the path.
        public AltObject Settings { get => Driver.WaitForObject(By.PATH, "/UICamera/Loadout/SettingsButton", timeout: 30); }

        //this function will wait for an object that contains the text "Button" and return it.
        public AltObject ObjectButton { get => Driver.WaitForObjectWhichContains(By.NAME, "Button", timeout: 30); }

        public bool IsDisplayed()
        {
            if (StartButton != null && StartText != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void PressStart()
        {
            StartButton.Tap();
        }
        //this function will find text in the game without returning anything
        public void FindTextObjects()
        {
            //where we store the text we want to find
            string elementName = "Text";
            //this line will find all the text we target on the screen
            var textObjects = Driver.FindObjects(By.NAME, elementName);
            //we will loop through
            foreach (var item in textObjects)
            {
                Console.WriteLine(item.name);
            }
            Console.WriteLine(textObjects.Count);
        }

        //this function will find an Object that contains Text "Button"
        public void FindObjectContainsButton()
        {
            //where we store the text we want to find
            string elementName = "Button";
            //this line will find all the text we target on the screen
            var buttonObject = Driver.FindObjectWhichContains(By.NAME, elementName);
            //we will loop through

            Console.WriteLine(buttonObject.name);
            Assert.NotNull(buttonObject);

        }
        //this function will find all objects that contain the text "Button"
        public void FindAllObjectsWhichContainsButton()
        {
            //where we store the text we want to find
            string elementName = "Button";
            //this line will find all the text we target on the screen
            var buttonObjects = Driver.FindObjectsWhichContain(By.NAME, elementName);
            //we will loop through
            foreach (var item in buttonObjects)
            {
                Console.WriteLine(item.name);
            }
            Console.WriteLine(buttonObjects.Count);
            Assert.NotNull(buttonObjects);
        }
        //this function will find objects by the coordinates given for the object
        public void FindImageAtCoordinates()
        {
            //this will find the object by the coordinates given, using FindObjectAtCoordinates AltTester fucntion.
            var objectByCoordinates = Driver.FindObjectAtCoordinates(new AltVector2(534.0f, 857.0f));
            //we will loop through
            Console.WriteLine(objectByCoordinates.name);
            Assert.NotNull(objectByCoordinates);
        }
        //this function will find and retrieve all active objects in the current game scene
        public void GetAllEnabledObjects()
        {
            //this will find all the objects in the current scene
            var allObjects = Driver.GetAllElements(enabled: true);
            //we will loop through
            foreach (var item in allObjects)
            {
                Console.WriteLine(item.name);
            }
            Console.WriteLine(allObjects.Count);
            Assert.NotNull(allObjects);
        }
        //this function will verify the Leaderboard is displayed
        public void IsLeaderBoardDisplayed()
        {
            //this will check if the Leaderboard is displayed
            if (LeaderBoard != null)
            {
                Console.WriteLine("Leaderboard is displayed.");
            }
            else
            {
                Console.WriteLine("Leaderboard is not displayed.");
            }
        }
        //this function will verify the Store is displayed
        public void IsStoreDisplayed()
        {
            //this will check if the Store is displayed
            if (Store != null)
            {
                Console.WriteLine("Store is displayed.");
            }
            else
            {
                Console.WriteLine("Store is not displayed.");
            }
        }
        //this function will verify the Missions is displayed
        public void IsMissionsDisplayed()
        {
            //this will check if the Missions is displayed
            if (Missions != null)
            {
                Console.WriteLine("Missions is displayed.");
            }
            else
            {
                Console.WriteLine("Missions is not displayed.");
            }
        }
        //this function will verify the Settings is displayed
        public void IsSettingsDisplayed()
        {
            //this will check if the Settings is displayed
            if (Settings != null)
            {
                Console.WriteLine("Settings is displayed.");
            }
            else
            {
                Console.WriteLine("Settings is not displayed.");
            }
        }
        //this function will wait for an object that contains the text "Button" and return it.
        public void WaitForObjectContainsButton()
        {

            Console.WriteLine(ObjectButton.name);
            Assert.That(ObjectButton.name, Is.EqualTo("pauseButton"));
        }
        //this function will verify that the logo is removed from the screen when the StartButton is clicked
        public void WaitForLogoToDisappear()
        {
            //this will wait for the StartButton to be clicked
            StartButton.Click();
            //this will wait for the logo to disappear
            Driver.WaitForObjectNotBePresent(By.NAME, "Image", timeout: 1, interval: 0.5);
            Console.WriteLine("Logo has disappeared from main menu screen.");
        }
        public void StartButtonProperties()
        {
            Console.WriteLine($"StartButton Name: {StartButton.name}");
            Console.WriteLine($"X Coordinate: {StartButton.x}, Y Coordinate: {StartButton.y}");
            Console.WriteLine($"Start Button enabled state: {StartButton.enabled}");
            Console.WriteLine($"Start Button World, X = {StartButton.worldX}, Y= {StartButton.worldY}, Z = {StartButton.worldZ}");
        }
        public string StartButtonText()
        {
            AltObject ButtonTextObject = Driver.FindObject(By.PATH, "/UICamera/Loadout/StartButton/Text");
            //this will return the text of the StartButton
            return ButtonTextObject.GetText();
        }
        public string ModifyStartButtonText()
        {
            string newText = "Sprint";
            AltObject ButtonTextObject = Driver.FindObject(By.PATH, "/UICamera/Loadout/StartButton/Text").SetText(newText, true);
            //this will return the text of the StartButton
            return ButtonTextObject.GetText();
        }
        //this function will get the coordinated of an object by its name
        public void GetStartButtonScreenPosition()
        {
            //this will get the screen position of the StartButton
            AltVector2 screenPosition = StartButton.GetScreenPosition();
            //this will print the screen position of the StartButton
            Console.WriteLine($"StartButton Screen Position: X = {screenPosition.x}, Y = {screenPosition.y}");
            Assert.NotNull(screenPosition);
        }
        public void GetStartButtonWorldPosition()
        {
            //this will get the world position of the StartButton
            AltVector3 worldPosition = StartButton.GetWorldPosition();
            //this will print the world position of the StartButton
            Console.WriteLine($"StartButton World Position: X = {worldPosition.x}, Y = {worldPosition.y}, Z = {worldPosition.z}");
            Assert.NotNull(worldPosition);
        }
        //this function will call on startbutton component 
        //object.CallComponentMethod<returnType>(componentName, methodName, assemblyName, parameters);
        public void StartButtonComponentMethod()
        {
            StartButton.CallComponentMethod<AsyncVoidMethodBuilder>("UnityEngine.UI.Button", "Press", "UnityEngine.UI", new object[] { });
        }
        //this function will allow us to use the GetComponentProperty method to get the font size of the StartButton
        public int GetStartButtonComponentProperty()
        {
            //this will get the font size of the StartButton
            AltObject startButtonText = Driver.FindObject(By.PATH, "/UICamera/Loadout/StartButton/Text");
            //this will return the font size of the StartButton
            int buttonFontSize = startButtonText.GetComponentProperty<int>("UnityEngine.UI.Text", "fontSize", "UnityEngine.UI");
            Console.WriteLine($"StartButton Font Size: {buttonFontSize}");
            return buttonFontSize;
        }
        //this function will modify the start buttons font size property using SetComponentProperty()
        public void ModifyComponentProperty()
        {
            AltObject startButtonText = Driver.FindObject(By.PATH, "/UICamera/Loadout/StartButton/Text");
             startButtonText.SetComponentProperty("UnityEngine.UI.Text", "fontSize", 30, "UnityEngine.UI");
        }
    }

    
}