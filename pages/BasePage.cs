//Foundation class basepage this gives access to the namespaces classes and methods
using AltTester.AltTesterUnitySDK.Driver;
using AltTester.AltTesterUnitySDK;
//this helps organize code into group all realated classes for page into a single package
namespace trashcat_automation.pages {
//this class will act as the base class that the other page classes can inherit from
    public class BasePage {
//this will allow us to communicate with the game. it's the bridge between the code and the game.
        AltDriver driver;
        //gives controlled access to private driver field allowing other classes to get or set a driver instances as needed
        public AltDriver Driver {get => driver; set => driver = value;}
//this constructor allows us to create a basepage with an existing altdriver instance 
//which is useful if we waant to pass in a driver already connected to the game. 
        public BasePage(AltDriver driver)
        {
            this.driver = driver;
        }
    //this is a parameterless constructor which creates a new instance of AltDriver if none is provided.
    //This is helpful when we want basepage to create it's own driver connection.
     public BasePage() {
        this.driver =  new AltDriver();
     }
    }
}