using Application.Pages;
using AventStack.ExtentReports;
using CommonLibs.Properties;
using NUnit.Framework;
using System.Threading;

namespace Tests.PageTests
{
    [Category("LoginPage")]
    public class LoginPageTests : BaseTests
    {
        #region Variables
        private LoginPage loginPage;
        #endregion Variables

        [SetUp]
        public void SetUpContext()
        {
            this.loginPage = new LoginPage(base.driver);
            base.MaximizeBrowser();
        }

        [Test]
        public void VerifySpotifyLoginTest()
        {
            this.extentReportUtils.CreateATestCase(Resource.VerifyLoginTest);
            this.extentReportUtils.AddTestLog(Status.Info, Resource.PerformingLogin);            
            // Loads Spotify page.
            loginPage.Open();
            // Test: Sets thes user name / email and password to test the login in Spotify page.
            loginPage.LoginToSpotify("", "");
            // Waits the page flow until get the title page.
            Thread.Sleep(5000);
            // Compares titles once login occurs.
            string actualTitle = base.driver.Title;
            Assert.AreEqual(Resource.ExpectedSpotifyTitle, actualTitle);
        }
    }
}
