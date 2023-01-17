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
using System.Text.RegularExpressions;
using IronPython.Hosting;

namespace CS_NUnit
{
    public class IronPythonTraining : BaseClass
    {
        // Test case template to wrap a Python script inside NUnit .NET framework, using IronPython
        public void IronPythonTest()
        {
            // Create an instance of the Python engine
            var engine = Python.CreateEngine();

            // Define the path to the Python script
            string Path = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string scriptPath = Path + "\\source\\repos-visualstudio\\CS_NUnit\\TestCases\\ironpython_script.py";

            // Execute the Python script and capture the output
            var source = engine.CreateScriptSourceFromFile(scriptPath);
            var scope = engine.CreateScope();
            source.Execute(scope);

            // check if the variable exists in the script's scope
            if (scope.TryGetVariable("result", out object result))
            {
                // Check the output against expected results
                Assert.AreEqual(15, result);
            }
            else
            {
                // handle the case where the variable doesn't exist
                Assert.Fail("result variable not found.");
            }
        }
    }
}

