using CS_NUnit.Values;
using CS_NUnit.Xpath;
using NUnit.Framework;

namespace CS_NUnit
{
    [TestFixture]
    public class TestSuite_NoRegressionTest
    {
        // Selenium Test suite
        public class SeleniumTrainingTestsuite : SeleniumTraining
        {
            Xpath__Login Xpath = new Xpath__Login();
            Values__Login Value = new Values__Login();

            [Test]
            [Category("SeleniumTraining")]
            public void SeleniumTestcase() => SeleniumTest(Xpath, Value);
        }

        // IropnPython Test suite
        public class IronPythonTrainingTestsuite : IronPythonTraining
        {

            [Test]
            [Category("SeleniumTraining")]
            public void IronPythonTrainingTestcase() => IronPythonTest();
        }

        // Appium Test suite
        public class AppiumTrainingTestSuite : AppiumTraining
        {
            Xpath__Mobile Xpath = new Xpath__Mobile();
            Values__Mobile Value = new Values__Mobile();

            [Test]
            [Category("Appium")]
            public void AppiumTestCase() => AppiumTest(Xpath, Value);
        }
    }
}