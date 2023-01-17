using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using CS_NUnit.Xpath;
using CS_NUnit.Values;
using CS_NUnit.Functions;
using Renci.SshNet;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;

namespace CS_NUnit
{
    public class AppiumTraining : BaseClass_Appium
    {
        public void AppiumTest(Xpath__Mobile Xpath, Values__Mobile Value)
        {
            // Server has to be initialized inside test script to prevent app restarting multiple times
            //AndroidDriver appiumDriver = new AndroidDriver();
            //appiumDriver.InitializeAppium();

            try
            {
                // Verify the Home page is opened and the title on the top is visible
                AssertElementExists(Xpath.HomePage);
            }

            catch (Exception e)
            {
                GetScreenshotAndPrintError(e);
            }
        }
    }
}