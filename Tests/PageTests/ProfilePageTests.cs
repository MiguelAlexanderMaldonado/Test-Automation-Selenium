using Application.Pages;
using CommonLibs.Properties;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.PageTests
{
    [Category("ProfilePage")]
    public class ProfilePageTests: BaseTests
    {
        #region Variables
        private ProfilePage profilePage;
        private readonly string newUserName = "Miguel Alexander Maldonado Lenis";
        #endregion Variables

        [SetUp]
        public void SetUpContext()
        {
            profilePage = new ProfilePage(base.driver);
            base.MaximizeBrowser();
        }

        [Description("Change the user name in profile page")]
        [Test]
        public void ChangeProfileNameTest() 
        {
            base.SetTestCaseReportLog(Resource.VerifyChangeProfileNameTest, Resource.PerformingChangeProfileNameTest);
            // Loads Spotify page.
            this.profilePage.Open();
            // Test: Sets thes user name / email and password to test the login in Spotify page.
            this.profilePage.LoginToSpotify(base.configuration["userNameSpotify"], base.configuration["passwordSpotify"]);
            // Changes the user name.
            this.profilePage.ChangeProfileName(this.newUserName);
            // Waits the page flow until get the profile user name.
            Thread.Sleep(5000);
            // Compares names.
            string userName = this.profilePage.GetProfileUserName();
            Assert.AreEqual(newUserName, userName);
        }

    }
}
