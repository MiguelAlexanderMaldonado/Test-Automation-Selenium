using Application.Pages;
using AventStack.ExtentReports;
using CommonLibs.Properties;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.PageTests
{
    public class InitialPageTest: BaseTests
    {
        [Test]
        public void CreatePlaylistSpotifyTest()
        {
            this.extentReportUtils.CreateATestCase(Resource.VerifyCreatePlaylistTest);
            this.extentReportUtils.AddTestLog(Status.Info, Resource.PerformingCreatePlaylistTest);
            LoginPage loginPage = new LoginPage(base.driver);
            InitialPage initialPage = new InitialPage(base.driver);
            int playListPosition = 1;
            // Loads Spotify page.            
            base.MaximizeBrowser();
            loginPage.Open();
            // Sets the user name / email and password to test the login in Spotify page.
            loginPage.LoginToSpotify("", "");
            // Test: Create a new playlist in Spotify lateral playlist.            
            initialPage.CreatePlaylistSpotify();
            // Wait 3000 ms.
            initialPage.GoToInitPageSpotify();
            Thread.Sleep(3000);
            // Compares titles once the playlist has been created.
            string playlistTittle = initialPage.GetTitlePlaylistSpotify(playListPosition);
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
            LoginPage loginPage = new LoginPage(base.driver);
            InitialPage initialPage = new InitialPage(base.driver);
            int playlistPosition = 1;
            base.MaximizeBrowser();
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
