using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Application.Pages
{
    public class LoginPage : BasePage
    {
        #region Variables
        private IWebElement CookiesElement 
        {
            get
            {
                string cookiesElement = "onetrust-accept-btn-handler";
                base.WaitUntilElementExists(By.Id(cookiesElement));
                return base.Driver.FindElement(By.Id(cookiesElement));
            }
        }
        private IWebElement FirstLoginButton => base.Driver.FindElement(By.XPath("//*[@type='button'][@data-testid='login-button']"));
        private IWebElement UserNameBox => base.Driver.FindElement(By.Id("login-username"));
        private IWebElement PasswordBox => base.Driver.FindElement(By.Id("login-password"));
        private IWebElement LastLoginButton => base.Driver.FindElement(By.Id("login-button"));

        public string TitlePage 
        {
            get 
            {
                return base.Driver.Title;
            }
        }

        #endregion Variables

        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="driver"></param>
        public LoginPage(IWebDriver driver) : base(driver) {}

        /// <summary>
        /// Open the URL of the Spotify login page.
        /// </summary>
        public void Open()
        {
            base.Driver.Navigate().GoToUrl(base.Configuration["baseUrlSpotify"]);
        }

        /// <summary>
        /// It login with user name and password in Spotify web page.
        /// </summary>
        /// <param name="sUsername">User name / email.</param>
        /// <param name="sPassword">Password.</param>
        public void LoginToSpotify(string sUsername, string sPassword)
        {
            // Accepts the cookies window.
            base.CmnElement.ClickElement(CookiesElement);
            // Clicks first login button.
            base.CmnElement.ClickElement(FirstLoginButton);
            // Waits to the final login button be load.
            // Waits 2s.
            Thread.Sleep(2000);
            // Sets the user
            base.CmnElement.SetText(UserNameBox, sUsername);
            // Sets the password.
            base.CmnElement.SetText(PasswordBox, sPassword);
            // Clicks last login button.
            base.CmnElement.ClickElement(LastLoginButton);
        }

        #endregion Public methods
    }
}
