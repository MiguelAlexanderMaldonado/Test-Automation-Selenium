using Application.Pages;
using AventStack.ExtentReports;
using CommonLibs.Properties;
using NUnit.Framework;
using System.Threading;

namespace Tests.PageTests
{
    [Category("InitialPage")]
    public class InitialPageTest: BaseTests
    {
        #region Variables
        private LoginPage loginPage;
        private InitialPage initialPage;
        private readonly int playlistPosition = 1;
        #endregion Variables

        [SetUp]
        public void SetUpContext() 
        {
            loginPage = new LoginPage(base.driver);
            initialPage = new InitialPage(base.driver);
            base.MaximizeBrowser();
        }

        [Test]
        public void CreatePlaylistSpotifyTest()
        {
            this.extentReportUtils.CreateATestCase(Resource.VerifyCreatePlaylistTest);
            this.extentReportUtils.AddTestLog(Status.Info, Resource.PerformingCreatePlaylistTest);
            // Loads Spotify page.
            loginPage.Open();
            // Sets the user name / email and password to test the login in Spotify page.
            loginPage.LoginToSpotify("", "");
            // Test: Create a new playlist in Spotify lateral playlist.            
            initialPage.CreatePlaylistSpotify();
            // Wait 3000 ms.
            initialPage.GoToInitPageSpotify();
            Thread.Sleep(3000);
            // Compares titles once the playlist has been created.
            string playlistTittle = initialPage.GetTitlePlaylistSpotify(playlistPosition);
            Assert.AreEqual(Resource.NewPlaylist, playlistTittle);
        }

        /// <summary>
        /// Delete the playlist created in the method CreatePlaylistSpotifyTest().
        /// </summary>
        [Test]
        public void DeletePlaylistSpotifyTest()
        {
            this.extentReportUtils.CreateATestCase(Resource.VerifyDeletePlaylistTest);
            this.extentReportUtils.AddTestLog(Status.Info, Resource.PerformingDeletePlaylistTest);
            // Loads Spotify page.
            loginPage.Open();
            // Sets the user name / email and password to test the login in Spotify page.
            loginPage.LoginToSpotify("", "");
            // Test: Delete the  new playlist in Spotify lateral playlist.
            string previousPlaylistTitle = initialPage.GetTitlePlaylistSpotify(playlistPosition);
            initialPage.DeletePlaylistSpotify(playlistPosition);
            initialPage.GoToInitPageSpotify();
            // Waits 3000 ms.
            Thread.Sleep(3000);
            // Compares titles once the playlist has been created.
            string postPlaylistTitle = initialPage.GetTitlePlaylistSpotify(playlistPosition);
            Assert.AreNotEqual(previousPlaylistTitle, postPlaylistTitle);
        }
    }
}
