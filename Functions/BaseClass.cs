using System;
using NUnit.Framework;
using CS_NUnit.Xpath;

namespace CS_NUnit.Functions
{
    public class BaseClass : CollectionMethods
    {
        // Import variable from .runsettings
        static string url_runsettings = TestContext.Parameters["weburl"].ToString();
        static string username_runsettings = TestContext.Parameters["username"].ToString();
        static string password_runsettings = TestContext.Parameters["password"].ToString();

        //Define standard fleet manager username and password, parametrized on each environment
        private String Email = username_runsettings;
        private String Password = password_runsettings;

        public BaseClass(Account Account)
        {
            this.Password = Account.Password;
            this.Email = Account.Email;
        }


        public BaseClass()
        {
        }

        [OneTimeSetUp]
        public void StartTest()
        {
        }

        [SetUp]
        public void Open()
        {
        }

        [TearDown]
        public void Close()
        {
        }

        [OneTimeTearDown]
        public void CloseTest()
        {
        }
    }
}