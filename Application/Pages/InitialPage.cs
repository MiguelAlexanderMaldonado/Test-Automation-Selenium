using CommonLibs.Properties;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Application.Pages
{
    public class InitialPage: BasePage
    {
        #region Variables

        private readonly IWebDriver _driver;

        #endregion Variables

        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="driver"></param>
        public InitialPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        /// <summary>
        /// Drags and drops an element of the Spotify lateral playlist.
        /// </summary>
        public void CreatePlaylistSpotify() 
        {
            /*** Loads JQuery in the context             
            //string workingDirectory = Environment.CurrentDirectory;
            //string currentProjectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            //IJavaScriptExecutor js = this._driver as IJavaScriptExecutor;
            //var jqueryFile = File.ReadAllText(currentProjectDirectory + "/js/jquery-3.5.1.js");
            //js.ExecuteScript(jqueryFile);
            ***/

            // Clicks the new playlist button.
            string createPlayList = "//div/div/div/button/*[@shape-rendering='crispEdges']/..";
            WebDriverWait wait = new WebDriverWait(this._driver, TimeSpan.FromSeconds(3));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(createPlayList)));
            this.cmnElement.ClickElement(this._driver.FindElement(By.XPath(createPlayList)));
            // Clicks the title of the new playlist.
            string playlistTitle = "//div/div/span/button/h1/..";
            wait.Until(ExpectedConditions.ElementExists(By.XPath(playlistTitle)));
            this.cmnElement.ClickElement(this._driver.FindElement(By.XPath(playlistTitle)));
            // Sets title to the new playlist.
            string setPlaylistTitle = "//*[@data-testid='playlist-edit-details-name-input']";
            wait.Until(ExpectedConditions.ElementExists(By.XPath(setPlaylistTitle)));
            this._driver.FindElement(By.XPath(setPlaylistTitle)).Clear();
            this.cmnElement.SetText(this._driver.FindElement(By.XPath(setPlaylistTitle)), Resource.NewPlaylist);
            // Saves the new playlist.
            string savePlaylist = "//*[@data-testid='playlist-edit-details-save-button']";
            wait.Until(ExpectedConditions.ElementExists(By.XPath(setPlaylistTitle)));
            this.cmnElement.ClickElement(this._driver.FindElement(By.XPath(savePlaylist)));
            

        }

        /// <summary>
        /// Load the init page.
        /// </summary>
        public void GoToInitPageSpotify() {
            // Clicks the init button.
            string initButton = "//*[@class='icon home-icon']/..";
            WebDriverWait wait = new WebDriverWait(this._driver, TimeSpan.FromSeconds(3));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(initButton)));
            this.cmnElement.ClickElement(this._driver.FindElement(By.XPath(initButton)));
        }

        /// <summary>
        /// Gets the title of the Spotify playlist located in the index position.
        /// </summary>
        /// <param name="indexList">Position of the list.</param>
        /// <returns>The title.</returns>
        public string GetTitlePlaylistSpotify(int indexList)
        {
            string title = string.Empty;
            string firstPlayList = $"//*[@class='os-content']/ul/div[{indexList}]/li/a/span";

            WebDriverWait wait = new WebDriverWait(this._driver, TimeSpan.FromSeconds(3));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(firstPlayList)));

            title = this.cmnElement.GetText(this._driver.FindElement(By.XPath(firstPlayList)));
            return title;
        }

        /// <summary>
        /// Deletes the playlist of the Spotify playlist located in the index position passed like parameter.
        /// </summary>
        /// <param name="indexPlaylist">List position.</param>
        public void DeletePlaylistSpotify(int indexPlaylist)
        {
            // Clicks (rigth click) the playlist to delete.
            string playList = $"//*[@class='os-content']/ul/div[{indexPlaylist}]/li/a/span/..";
            WebDriverWait wait = new WebDriverWait(this._driver, TimeSpan.FromSeconds(3));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(playList)));
            // Rigth click.
            Actions action = new Actions(this._driver);
            action.ContextClick(this._driver.FindElement(By.XPath(playList))).Build().Perform();
            // Clicks the delete option.
            string deleteOption = "//*[@role='menu']/li[5]";
            wait = new WebDriverWait(this._driver, TimeSpan.FromSeconds(3));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(deleteOption)));
            this.cmnElement.ClickElement(this._driver.FindElement(By.XPath(deleteOption)));
            // Clicks the accept delete option.
            string acceptDeleteOption = "//*[@role='dialog']/div[1]/div/button[2]";
            wait = new WebDriverWait(this._driver, TimeSpan.FromSeconds(3));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(acceptDeleteOption)));
            this.cmnElement.ClickElement(this._driver.FindElement(By.XPath(acceptDeleteOption)));
        }


        #endregion Public methods
    }
}
