using Application.Pages;
using AventStack.ExtentReports;
using CommonLibs.Properties;
using NUnit.Framework;
using System.Threading;

namespace Tests.PageTests
{
    public class LoginPageTests : BaseTests
    {
        [Test]
        public void VerifySpotifyLoginTest()
        {
            this.extentReportUtils.CreateATestCase(Resource.VerifyLoginTest);
            this.extentReportUtils.AddTestLog(Status.Info, Resource.PerformingLogin);
            LoginPage loginPage = new LoginPage(base.driver);
            base.MaximizeBrowser();
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
