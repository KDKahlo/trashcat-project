
//Foundation class basepage this gives access to the namespaces classes and methods
using AltTester.AltTesterUnitySDK.Driver;


namespace trashcat_automation.pages
{
    public class MainMenuPage : BasePage
    {
        //defines constructor for MainMenuPage. and extends base driver
        public MainMenuPage(AltDriver driver) : base(driver)
        {

        }
        //this will point the the startbutton at the start of the game, finding it by the name.
        public AltObject StartButton { get => Driver.FindObject(By.NAME, "StartButton"); }
        //this will point the the start text at the start of the game, finding it by the name.     
        public AltObject StartText { get => Driver.FindObject(By.NAME, "START"); }

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
    }
}