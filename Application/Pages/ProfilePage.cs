using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Pages
{
    public class ProfilePage : LoginPage
    {
        #region Variables
        private IWebElement UserWidgetLink 
        {
            get 
            {
                string userWidgetLink = "//*[@data-testid='user-widget-link']";
                base.WaitUntilElementExists(By.XPath(userWidgetLink));
                return base.Driver.FindElement(By.XPath(userWidgetLink));
            }
        }
        private IWebElement ProfileItem
        {
            get
            {
                string profileItem = "//*[@role='menu']/li[2]/a";
                base.WaitUntilElementExists(By.XPath(profileItem));
                return base.Driver.FindElement(By.XPath(profileItem));
            }
        }   
        private IWebElement ProfileMenu
        {
            get
            {
                string profileMenu = "//*[@aria-haspopup='menu']";
                base.WaitUntilElementExists(By.XPath(profileMenu));
                return base.Driver.FindElement(By.XPath(profileMenu));
            }
        }
        private IWebElement EditProfileButton
        {
            get
            {
                string editProfileButton = "//*[@id='context-menu-root']/ul/li[1]/button";
                base.WaitUntilElementExists(By.XPath(editProfileButton));
                return base.Driver.FindElement(By.XPath(editProfileButton));
            }
        }
        private IWebElement ProfileName
        {
            get
            {
                string profileName = "//*[@data-testid='user-edit-name-input']";
                base.WaitUntilElementExists(By.XPath(profileName));
                return base.Driver.FindElement(By.XPath(profileName));
            }
        }
        private IWebElement SaveProfileNameButton
        {
            get
            {
                string saveProfileNameButton = "//*[@class='GenericModal ']/div/form/button";
                base.WaitUntilElementExists(By.XPath(saveProfileNameButton));
                return base.Driver.FindElement(By.XPath(saveProfileNameButton));
            }
        }
        private IWebElement ProfileUserName
        {
            get
            {
                string profileUserName = "//*[@title='Edit details']/h1";
                base.WaitUntilElementExists(By.XPath(profileUserName));
                return base.Driver.FindElement(By.XPath(profileUserName));
            }
        }
        #endregion Variables

        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="driver"></param>
        public ProfilePage(IWebDriver driver) : base(driver) { }

        /// <summary>
        /// Changes the user name value with the parameter's value.
        /// </summary>
        /// <param name="newUserName">New user name.</param>
        public void ChangeProfileName(string newUserName) 
        {
            // Clicks user widget link.
            base.CmnElement.ClickElement(this.UserWidgetLink);
            // Clicks profile item.
            base.CmnElement.ClickElement(this.ProfileItem);
            // Clicks profile menu.
            base.CmnElement.ClickElement(this.ProfileMenu);
            // Clicks edit profile.
            base.CmnElement.ClickElement(this.EditProfileButton);
            // Changes user name.
            base.CmnElement.ClearText(this.ProfileName);
            base.CmnElement.SetText(this.ProfileName, newUserName);
            // Saves change.
            base.CmnElement.ClickElement(this.SaveProfileNameButton);
        }

        /// <summary>
        /// Gets the profile user name.
        /// </summary>
        /// <returns></returns>
        public string GetProfileUserName()
        {
            return this.ProfileUserName.Text;
        }

        #endregion Public methods

    }
}
