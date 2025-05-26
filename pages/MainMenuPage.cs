
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
    }
}