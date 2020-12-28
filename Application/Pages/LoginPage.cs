using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Application.Pages
{
    public class LoginPage: BasePage
    {
        #region Variables

        private readonly IWebDriver _driver;

        #endregion Variables

        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="driver"></param>
        public LoginPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        /// <summary>
        /// Open the URL of the Spotify login page.
        /// </summary>
        public void Open()
        {
            this._driver.Navigate().GoToUrl(base.configuration["baseSpotifyUrl"]);
        }

        /// <summary>
        /// It login with user name and password in Spotify web page.
        /// </summary>
        /// <param name="sUsername">User name / email.</param>
        /// <param name="sPassword">Password.</param>
        public void LoginToSpotify(string sUsername, string sPassword)
        {
            // Waits to the cookies window be load
            string cookiesElement = "onetrust-accept-btn-handler";
            WebDriverWait wait = new WebDriverWait(this._driver, TimeSpan.FromSeconds(3));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(cookiesElement)));
            this.cmnElement.ClickElement(this._driver.FindElement(By.Id(cookiesElement)));
            // Clicks first login button
            var firstLogInButton = this._driver.FindElement(By.XPath("//*[@type='button'][@data-testid='login-button']"));
            this.cmnElement.ClickElement(firstLogInButton);
            // Waits to the final login button be load
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("login-button")));
            // Sets the user
            var username = this._driver.FindElement(By.Id("login-username"));
            this.cmnElement.SetText(username, sUsername);
            // Sets the password
            var password = this._driver.FindElement(By.Id("login-password"));
            this.cmnElement.SetText(password, sPassword);
            // Click the log in button
            var finalLoginButton = this._driver.FindElement(By.Id("login-button"));
            this.cmnElement.ClickElement(finalLoginButton);
        }

        /// <summary>
        /// Get the page title of the web page.
        /// </summary>
        /// <returns>Page title.</returns>
        public string GetTitlePage()
        {
            // Waits the page flow until get the title page.
            System.Threading.Thread.Sleep(5000);
            return this._driver.Title;
        }

        #endregion Public methods
    }
}
