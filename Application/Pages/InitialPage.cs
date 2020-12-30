using CommonLibs.Properties;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;

namespace Application.Pages
{
    public class InitialPage : BasePage
    {
        #region Variables
        
        private IWebElement HomeIcon
        {
            get
            {
                string homeIcon = "//*[@class='icon home-icon']/..";
                base.WaitUntilElementExists(By.XPath(homeIcon));
                return base.Driver.FindElement(By.XPath(homeIcon));
            }
        }
        
        #region New playlist
            private IWebElement CreatePlaylistButton
            {
                get
                {
                    string createPlaylist = "//div/div/div/button/*[@shape-rendering='crispEdges']/..";
                    base.WaitUntilElementExists(By.XPath(createPlaylist));
                    return base.Driver.FindElement(By.XPath(createPlaylist));
                }
            }
            private IWebElement PlaylistTitleLink
            {
                get
                {
                    string playlistTitleLink = "//div/div/span/button/h1/..";
                    base.WaitUntilElementExists(By.XPath(playlistTitleLink));
                    return base.Driver.FindElement(By.XPath(playlistTitleLink));
                }
            }
            private IWebElement PlaylistTitleBox
            {
                get
                {
                    string playlistTitleBox = "//*[@data-testid='playlist-edit-details-name-input']";
                    base.WaitUntilElementExists(By.XPath(playlistTitleBox));
                    return base.Driver.FindElement(By.XPath(playlistTitleBox));
                }
            }
            private IWebElement SavePlaylistButton
            {
                get
                {
                    string savePlaylist = "//*[@data-testid='playlist-edit-details-save-button']";
                    base.WaitUntilElementExists(By.XPath(savePlaylist));
                    return base.Driver.FindElement(By.XPath(savePlaylist));
                }
            }
        #endregion New playlist
        
        #region Delete playlist
            private IWebElement DeleteOptionButton
            {
                get 
                {
                    string deleteOption = "//*[@role='menu']/li[5]";
                    base.WaitUntilElementExists(By.XPath(deleteOption));
                    return base.Driver.FindElement(By.XPath(deleteOption));
                }
            }
            private IWebElement AcceptDeleteOptionButton
            {
                get
                {
                    string acceptDeleteOption = "//*[@role='dialog']/div[1]/div/button[2]";
                    base.WaitUntilElementExists(By.XPath(acceptDeleteOption));
                    return base.Driver.FindElement(By.XPath(acceptDeleteOption));
                }
            }
        #endregion Delete playlist
        
        private IWebElement ReproduceButton
        {
            get 
            {
                string reproduceButton = "//*[@data-testid='action-bar-row']/*[@data-testid='play-button']";
                base.WaitUntilElementExists(By.XPath(reproduceButton));
                return base.Driver.FindElement(By.XPath(reproduceButton));
            }
        }

        public bool IsReproducing
        {
            get 
            {
                return Pause.Equals(ReproduceButton.GetAttribute("aria-label"));
            }
        }

        private string Pause => "Pausa";

        #endregion Variables

        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="driver"></param>
        public InitialPage(IWebDriver driver): base(driver){}

        /// <summary>
        /// Drags and drops an element of the Spotify lateral playlist.
        /// </summary>
        public void CreatePlaylistSpotify() 
        {
            /*** Loads JQuery in the context             
            //string workingDirectory = Environment.CurrentDirectory;
            //string currentProjectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            //IJavaScriptExecutor js = base.Driver as IJavaScriptExecutor;
            //var jqueryFile = File.ReadAllText(currentProjectDirectory + "/js/jquery-3.5.1.js");
            //js.ExecuteScript(jqueryFile);
            ***/

            // Clicks the new playlist button.
            base.CmnElement.ClickElement(CreatePlaylistButton);
            // Clicks the title of the new playlist.
            base.CmnElement.ClickElement(PlaylistTitleLink);
            // Sets title to the new playlist.
            base.CmnElement.ClearText(PlaylistTitleBox);
            base.CmnElement.SetText(PlaylistTitleBox, Resource.NewPlaylist);
            // Saves the new playlist.
            base.CmnElement.ClickElement(SavePlaylistButton);
        }

        /// <summary>
        /// Load the init page.
        /// </summary>
        public void GoToInitPageSpotify() {
            // Clicks the home icon button.
            base.CmnElement.ClickElement(HomeIcon);
        }

        /// <summary>
        /// Gets the title of the Spotify playlist located in the index position.
        /// </summary>
        /// <param name="indexList">Position of the list.</param>
        /// <returns>The title.</returns>
        public string GetTitlePlaylistSpotify(int indexList)
        {
            string firstPlayList = $"//*[@class='os-content']/ul/div[{indexList}]/li/a/span";
            base.WaitUntilElementExists(By.XPath(firstPlayList));
            return base.CmnElement.GetText(base.Driver.FindElement(By.XPath(firstPlayList)));
        }

        /// <summary>
        /// Deletes the playlist of the Spotify playlist located in the index position passed like parameter.
        /// </summary>
        /// <param name="indexPlaylist">List position.</param>
        public void DeletePlaylistSpotify(int indexPlaylist)
        {
            // Clicks (rigth click) the playlist to delete.
            string playList = $"//*[@class='os-content']/ul/div[{indexPlaylist}]/li/a/span/..";
            base.WaitUntilElementExists(By.XPath(playList));
            // Rigth click.
            base.CmnActions.ContextClick(base.Driver.FindElement(By.XPath(playList)));
            // Clicks the delete option.
            base.CmnElement.ClickElement(DeleteOptionButton);
            // Clicks the accept delete option.
            base.CmnElement.ClickElement(AcceptDeleteOptionButton);
        }

        /// <summary>
        /// Reproduce the playlist of the Spotify playlist located in the index position passed like parameter.
        /// </summary>
        /// <param name="indexPlaylist">List position.</param>
        public void ReproducePlaylistSpotify(int indexPlaylist)
        {
            // Clicks the playlist to reproduce.
            string playList = $"//*[@class='os-content']/ul/div[{indexPlaylist}]/li/a/span/..";
            base.WaitUntilElementExists(By.XPath(playList));
            base.CmnElement.ClickElement(base.Driver.FindElement(By.XPath(playList)));
            // Clicks the play button.
            base.CmnElement.ClickElement(ReproduceButton);
        }


        #endregion Public methods
    }
}
