
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
             
        public AltObject StartText { get => Driver.FindObject(By.NAME, "START"); }
    }
}