//Foundation class basepage this gives access to the namespaces classes and methods
using AltTester.AltTesterUnitySDK.Driver;
using AltTester.AltTesterUnitySDK;

namespace trashcat_automation.pages {

    public class BasePage {

        AltDriver driver;

        public AltDriver Driver {get => driver; set => driver = value;}

        public BasePage(AltDriver driver)
        {
            this.driver = driver;
        }
     public BasePage() {
        this.driver =  new AltDriver()
     }
    }
}