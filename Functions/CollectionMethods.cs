using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using Renci.SshNet;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.Interfaces;

namespace CS_NUnit.Functions
{
    public class CollectionMethods
    {
        public IWebDriver driver;
        public IDictionary<string, object> vars { get; private set; }
        public IJavaScriptExecutor js;
        static string username_runsettings = TestContext.Parameters["username"].ToString();
        static string password_runsettings = TestContext.Parameters["password"].ToString();

        private String Email = username_runsettings;
        private String Password = password_runsettings;

        // Method to open the ChromeDriver session
        public void OpenChromeDriver()
        {
            ChromeOptions option = new ChromeOptions();
            option.AddArgument("--disable-web-security");
            option.AddArgument("--ignore-ssl-errors=yes");
            option.AddArgument("--ignore-certificate-errors");
            option.AddArgument("--no-sandbox");
            option.AddArgument("--headless");
            option.AddArgument("--lang=en-GB");
            option.AddArgument("--window-size=1920,1080");
            //option.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);
            //option.AddUserProfilePreference("download.default_directory", Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "\\DataCollector\\");

            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), option);

            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(120);
            js = (IJavaScriptExecutor)driver;
            vars = new Dictionary<string, object>();
        }

        // Method to open the Android session
        public void OpenAndroid()
        {
            AppiumOptions option = new AppiumOptions();
            option.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            option.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "13.0");
            option.AddAdditionalCapability(MobileCapabilityType.DeviceName, "R5CR8218TJV");
            option.AddAdditionalCapability(MobileCapabilityType.AutomationName, "UiAutomator2");

            // Use the command > adb shell dumpsys window | find "mCurrentFocus" < to retrieve AppPackage and AppActivity 
            option.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "com.calm.android");
            option.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, "com.calm.android.ui.splash.SplashActivity");

            // Use app capability if the app is in a particular location
            //option.AddAdditionalCapability(MobileCapabilityType.App, @"C:\Users\marco.alberti\Downloads\Calculator_v8.1 (403424005)_apkpure.com.apk");

            // Provide uri of Appium Server
            driver = new AndroidDriver<AndroidElement>(new Uri("http://127.0.0.1:4723/wd/hub"), option);

            // Same as for Selenium as follow:
            // driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), option);
        }

        // Method to close the ChromeDriver session
        public void CloseDriver()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }

        // Method to open the website page
        public void OpenWebsite(String url_runsettings)
        {
            driver.Navigate().GoToUrl(url_runsettings);
            Console.WriteLine("Navigating to: " + url_runsettings);
            driver.Manage().Window.Maximize();
        }

        // Method to verify if an element exists, identified through the Xpath "XpathVar"
        public void AssertElementExists(String XpathVar)
        {
            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(30));
            wait.Until(driver => driver.FindElement(By.XPath(XpathVar)).Displayed);
            Console.WriteLine("Verified presence of element by its XPaht: " + XpathVar);
        }

        // Method to verify if an element, identified through the Xpath "XpathVar", contains a specific test "ValueVar"
        public void AssertElementIsEqualTo(String XpathVar, String ValueVar)
        {
            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(30));
            wait.Until(driver => driver.FindElement(By.XPath(XpathVar)).Displayed);
            Assert.That(driver.FindElement(By.XPath(XpathVar)).Text, Is.EqualTo(ValueVar));
            Console.WriteLine("Verified presence of " + ValueVar + " label");
        }

        // Method to verify if an element, identified through the Xpath "XpathVar", contains a specific Value attrbiute "ValueVar"
        public void AssertValueIsEqualTo(String XpathVar, String ValueVar)
        {
            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(30));
            wait.Until(driver => driver.FindElement(By.XPath(XpathVar)).Displayed);
            Console.WriteLine("Attribute = " + driver.FindElement(By.XPath(XpathVar)).GetAttribute("value"));
            Assert.That(driver.FindElement(By.XPath(XpathVar)).GetAttribute("value").ToString(), Is.EqualTo(ValueVar));
            Console.WriteLine("Verified presence of " + ValueVar + " label");
        }

        //Method to verify if an element identified through the Xpath "XpathVar" contains a specific test "ValueVar" and then click on it
        public void FindElementAndClick(String XpathVar, String ValueVar, int timer)
        {
            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(30));
            wait.Until(driver => driver.FindElement(By.XPath(XpathVar)).Displayed);
            Console.WriteLine("Element found with Xpath: " + XpathVar);
            Assert.That(driver.FindElement(By.XPath(XpathVar)).Text, Is.EqualTo(ValueVar));
            Console.WriteLine("Assert element value coherent with expectation: " + ValueVar);
            IWebElement ElementToClick = driver.FindElement(By.XPath(XpathVar));
            ElementToClick.Click();
            Console.WriteLine("Clicking on button: " + ValueVar + ", by Xpath: " + XpathVar);
            System.Threading.Thread.Sleep(timer);
        }

        // Method to verify if an element identified through the Xpath "XpathVar" contains a specific test "ValueVar" and then click on it (JAVASCRIPT)
        public void JSFindElementAndClick(String XpathVar, String ValueVar, int timer)
        {
            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(30));
            wait.Until(driver => driver.FindElement(By.XPath(XpathVar)).Displayed);
            Console.WriteLine("Element found with Xpath: " + XpathVar);
            Assert.That(driver.FindElement(By.XPath(XpathVar)).Text, Is.EqualTo(ValueVar));
            Console.WriteLine("Assert element value coherent with expectation: " + ValueVar);
            IWebElement ElementToClick = driver.FindElement(By.XPath(XpathVar));
            js.ExecuteScript("arguments[0].click();", ElementToClick);
            Console.WriteLine("Clicking on button: " + ValueVar + ", by Xpath: " + XpathVar);
            System.Threading.Thread.Sleep(timer);
        }

        // Method to click and insert a text "ValueToInsert" onto and element identified through the Xpath "XpathVar"
        public void FindElementClickAndSendText(String XpathVar, String ValueToInsert, int timer)
        {
            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(30));
            wait.Until(driver => driver.FindElement(By.XPath(XpathVar)).Displayed);
            IWebElement ElementToClick = driver.FindElement(By.XPath(XpathVar));
            ElementToClick.Click();
            System.Threading.Thread.Sleep(timer);
            Console.WriteLine("Clicking on element by Xpath: " + XpathVar);
            ElementToClick.SendKeys(ValueToInsert);
            Console.WriteLine("Inserting value: " + ValueToInsert);
            System.Threading.Thread.Sleep(timer);
        }

        // Method to click on dropdown choice "ValueToSelect" by Xpath "XpathVar"
        public void ClickDropdownChoice(String XpathVar, String ValueToInsert, int timer)
        {
            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(30));
            wait.Until(driver => driver.FindElement(By.XPath(XpathVar)).Displayed);
            IWebElement VehicleDropdownChoice = driver.FindElement(By.XPath(XpathVar));
            VehicleDropdownChoice.Click();
            System.Threading.Thread.Sleep(timer);
            Console.WriteLine("Selecting dropdown value proposed by inserting: " + ValueToInsert);
        }

        //Method to Refresh the check identified by the XPath "XpathVar". If the element doesn't appear after three refresh the test fails
        public void RefreshCheck(String XpathVar)
        {
            for (int i = 0; i <= 3; i++)
            {
                try
                {
                    WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(30));
                    wait.Until(driver => driver.FindElement(By.XPath(XpathVar)).Displayed);
                    break;
                }
                catch
                {
                    driver.Navigate().Refresh();
                    Console.WriteLine("Repeating refresh");
                    System.Threading.Thread.Sleep(3000);
                }
                if (i == 3)
                {
                    Assert.Fail("3 refresh, page not loaded, fail test");
                }
            }
        }

        //Decompress gzip file
        public static void Decompress(FileInfo fileToDecompress)
        {
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        Console.WriteLine($"Decompressed: {fileToDecompress.Name}");
                    }
                }
            }
        }

        // Exception method to print logs and attach a screenshot
        public void GetScreenshotAndPrintError(Exception e)
        {
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            String ReportFolderPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "\\TestResults\\";
            String ScreenShotName = ReportFolderPath + "ErrorScreenshot_" + DateTime.Now.ToString("HHmmss") + ".png";
            ss.SaveAsFile(ScreenShotName, ScreenshotImageFormat.Png);
            TestContext.AddTestAttachment(ScreenShotName, "Screenshot for failure analysis");
            Console.WriteLine("Test failed");
            Console.WriteLine(e.StackTrace);
            Assert.Fail("Test failed. See test output and attachment for analysis");
        }

        // Method to verify IP address of the working machine
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        // Method to scroll till a specifich element, identified by its XPath
        public void Scroll(string XPath)
        {
            var element = driver.FindElement(By.XPath(XPath));
            Actions actions = new Actions(driver);
            actions.MoveToElement(element);
            actions.Perform();
        }

        // Method to swipe mobile screen in choosing direction. Pass in input the direction of the swipe needed
        public void Swipe(string Direction)
        {
            ITouchAction a;

            if (Direction == "Left")
            {
                a = new TouchAction((IPerformsTouchActions)driver).Press(1000, 1000).Wait(1000).MoveTo(100, 1000).Release();
                a.Perform();
                Console.WriteLine("Swiping mobile screen left");
            }
            else if (Direction == "Right")
            {
                a = new TouchAction((IPerformsTouchActions)driver).Press(100, 1000).Wait(1000).MoveTo(1000, 1000).Release();
                a.Perform();
                Console.WriteLine("Swiping mobile screen right");
            }
            else if (Direction == "Up")
            {
                a = new TouchAction((IPerformsTouchActions)driver).Press(500, 1500).Wait(1000).MoveTo(500, 500).Release();
                a.Perform();
                Console.WriteLine("Swiping mobile screen up");
            }
            else if (Direction == "Down")
            {
                a = new TouchAction((IPerformsTouchActions)driver).Press(500, 500).Wait(1000).MoveTo(500, 1500).Release();
                a.Perform();
                Console.WriteLine("Swiping mobile screen down");
            }
            else
            {
                Console.WriteLine("Wrong expected direction");
            }
        }

        // Method to tap in a specific point, defined by pixels (X,Y). Useful to close Android keyboard
        public void TapOutside(int PixelX, int PixelY)
        {
            ITouchAction a;
            a = new TouchAction((IPerformsTouchActions)driver).Press(PixelX, PixelY).Wait(200).Release();
            a.Perform();
        }

        // Method to scroll a page horizontally "horPixel" and vertically "verPixel"
        public void ScrollByPixels(int horPixel, int verPixel)
        {
            js.ExecuteScript("window.scrollTo(" + horPixel + "," + verPixel + ")");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Scrolling to position: " + horPixel + "," + verPixel);
        }

        // Functions to interact with VECTOR CANalyzer tools (commented since require CANalyzer COM Reference)
        /*
        // Define global variable named mCANalyzerApp, through which the COM server’s Application object will be accessed
        private CANalyzer.Application mCANalyzerApp;
        private CANalyzer.Measurement mCANalyzerMeasurement;
        
        // Function to specify and send CAN trace.cfg file
        public void CANfunSendCANtracection(string CANcfgTrace, int TimeBeforeSending, int TraceDuration)
        {
            // Variable inzilialization
            mCANalyzerApp = new CANalyzer.Application();
            mCANalyzerMeasurement = (CANalyzer.Measurement)mCANalyzerApp.Measurement;

            // Setup CANalyzer run with 1st configuration
            // Load first configuration file
            string Path = System.Environment.GetEnvironmentVariable("USERPROFILE");
            mCANalyzerApp.Open(Path + "\\source\\repos\\E2E\\CAN traces\\Configurations\\" + CANcfgTrace, true, true);

            CANalyzer.CAPL CANalyzerCAPL = (CANalyzer.CAPL)mCANalyzerApp.CAPL;
            CANalyzerCAPL.Compile(null);

            Console.WriteLine("Sending trace after " + TimeBeforeSending + " ms");
            System.Threading.Thread.Sleep(TimeBeforeSending);

            Console.WriteLine("Triggering configuration");
            mCANalyzerMeasurement.Start();

            System.Threading.Thread.Sleep(TraceDuration);
            Console.WriteLine("Trace sent for " + TraceDuration + " ms");

            // Stop configuration after 3 min
            mCANalyzerMeasurement.Stop();
            Console.WriteLine("Stop configuration");
        }

        // Function to Start sending CAN trace
        public void StartCANtrace(string CANcfgTrace, int TimeBeforeSending)
        {
            // Variable inzilialization
            mCANalyzerApp = new CANalyzer.Application();
            mCANalyzerMeasurement = (CANalyzer.Measurement)mCANalyzerApp.Measurement;

            // Setup CANalyzer run with 1st configuration
            // Load first configuration file
            string Path = System.Environment.GetEnvironmentVariable("USERPROFILE");
            mCANalyzerApp.Open(Path + "\\source\\repos\\E2E\\CAN traces\\Configurations\\" + CANcfgTrace, true, true);

            CANalyzer.CAPL CANalyzerCAPL = (CANalyzer.CAPL)mCANalyzerApp.CAPL;
            CANalyzerCAPL.Compile(null);

            Console.WriteLine("Sending trace after " + TimeBeforeSending + " ms");
            System.Threading.Thread.Sleep(TimeBeforeSending);

            Console.WriteLine("Triggering configuration");
            mCANalyzerMeasurement.Start();
        }

        // Function to Stop sending CAN trace
        public void StopCANtrace()
        {
            // Stop configuration 
            mCANalyzerMeasurement.Stop();
            Console.WriteLine("Stop configuration");
        }
        */
    }
}
