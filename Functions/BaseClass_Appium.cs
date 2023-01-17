using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using CS_NUnit.Xpath;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Support.UI;

namespace CS_NUnit.Functions
{

    public class BaseClass_Appium : CollectionMethods
    {
        [OneTimeSetUp]
        public void InitializeAppium()
        {
            OpenAndroid();
        }

        [OneTimeTearDown]
        public void CloseAppium()
        {
            driver.Quit();
        }
    }
}