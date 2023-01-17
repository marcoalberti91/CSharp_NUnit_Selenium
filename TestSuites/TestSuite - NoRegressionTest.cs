using CS_NUnit.Values;
using CS_NUnit.Xpath;
using NUnit.Framework;

namespace CS_NUnit
{
    [TestFixture]
    public class TestSuite_NoRegressionTest
    {
        public class AccessSeleniumTrainingTestsuite : SeleniumTraining
        {
            Xpath__Login Xpath = new Xpath__Login();
            Values__Login Value = new Values__Login();

            [Test]
            [Category("SeleniumTraining")]
            public void AccessSeleniumTrainingTestcase() => AccessSeleniumTraining(Xpath, Value);
        }

        public class IronPythonTrainingTestsuite : IronPythonTraining
        {

            [Test]
            [Category("SeleniumTraining")]
            public void IronPythonTrainingTestcase() => IronPythonTest();
        }
    }
}