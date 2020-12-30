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

        [Description("Creates a playlist in Spotify lateral playlist")]
        [Test]        
        public void CreatePlaylistSpotifyTest()
        {
            base.SetTestCaseReportLog(Resource.VerifyCreatePlaylistTest, Resource.PerformingCreatePlaylistTest);
            // Logins in Spotify web page.
            LoginToSpotify();
            // Test: Create a new playlist in Spotify lateral playlist.            
            initialPage.CreatePlaylistSpotify();
            // Goes to the initial page.
            initialPage.GoToInitPageSpotify();
            // Waits 3s.
            Thread.Sleep(3000);
            // Compares titles once the playlist has been created.
            string playlistTittle = initialPage.GetTitlePlaylistSpotify(playlistPosition);
            Assert.AreEqual(Resource.NewPlaylist, playlistTittle, Resource.ErrorPlaylistNotCreate);
        }

        [Description("Deletes a playlist in Spotify lateral playlist")]
        [Test]
        public void DeletePlaylistSpotifyTest()
        {
            base.SetTestCaseReportLog(Resource.VerifyDeletePlaylistTest, Resource.PerformingDeletePlaylistTest);
            // Logins in Spotify web page.
            LoginToSpotify();
            // Test: Deletes a playlist in Spotify lateral playlist.
            string previousPlaylistTitle = initialPage.GetTitlePlaylistSpotify(playlistPosition);
            initialPage.DeletePlaylistSpotify(playlistPosition);
            initialPage.GoToInitPageSpotify();
            // Waits 3s.
            Thread.Sleep(3000);
            // Compares titles once the playlist has been deleted.
            string postPlaylistTitle = initialPage.GetTitlePlaylistSpotify(playlistPosition);
            Assert.AreNotEqual(previousPlaylistTitle, postPlaylistTitle, Resource.ErrorPlaylistNotDelete);
        }

        [Description("Reproduces a playlist in Spotify lateral playlist")]
        [Test]
        public void ReproducePlaylistSpotifyTest() 
        {
            base.SetTestCaseReportLog(Resource.VerifyReproducePlaylistTest, Resource.PerformingReproducePlaylistTest);
            // Logins in Spotify web page.
            LoginToSpotify();
            // Test: Reproduces a playlist in Spotify lateral playlist.
            initialPage.ReproducePlaylistSpotify(playlistPosition);
            // Waits 2 s.
            Thread.Sleep(2000);
            // Checks if the playlist is reproducing.
            Assert.IsTrue(initialPage.IsReproducing, Resource.ErrorPlaylistNotReproduce);
        }

        #region Private methods

        private void LoginToSpotify()
        {
            // Loads Spotify page.
            loginPage.Open();
            // Sets the user name / email and password to test the login in Spotify page.
            loginPage.LoginToSpotify(base.configuration["userNameSpotify"], base.configuration["passwordSpotify"]);
        }

        #endregion Private methods
    }
}
