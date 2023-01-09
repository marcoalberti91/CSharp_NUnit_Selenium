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
using System.Linq;
using System.Text.RegularExpressions;

namespace CS_NUnit
{
    public class SeleniumTraining : BaseClass_ToolsQA
    {
        public void AccessSeleniumTraining(Xpath__Login Xpath, Values__Login Value)
        {
            try
            {
                // Verify presence of Logo in Home page
                AssertElementExists(Xpath.HomePageLogo);

                // Click on SELENIUM TRAINING box
                FindElementAndClick(Xpath.SeleniumTraining, Value.SeleniumTraining, 10);
            }

            catch (Exception e)
            {
                GetScreenshotAndPrintError(e);
            }
        }
    }
}

