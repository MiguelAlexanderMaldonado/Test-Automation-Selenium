using CommonLibs.Implementation;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Application.Pages
{
    public class BasePage
    {
        #region Variables
        protected IWebDriver Driver { get; set; }
        protected CommonElement CmnElement { get; set; }
        protected CommonActions CmnActions { get; set; }
        protected IConfigurationRoot Configuration { get; set; }
        private WebDriverWait Wait { get; set; }
        #endregion Variables

        #region Public methods

        public BasePage(IWebDriver driver) 
        {
            this.Driver = driver;
            this.CmnElement = new CommonElement();
            this.CmnActions = new CommonActions(this.Driver);
            this.Wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(3));
            string workingDirectory = Environment.CurrentDirectory;
            string currentProjectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            this.Configuration = new ConfigurationBuilder().AddJsonFile(currentProjectDirectory + "/config/appSettings.json").Build();
        }

        public void WaitUntilElementExists(By element)
        {
            Wait.Until(ExpectedConditions.ElementExists(element));
        }

        #endregion Public methods
    }
}
