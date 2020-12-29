using CommonLibs.Implementation;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using System;
using System.IO;

namespace Application.Pages
{
    public class BasePage
    {
        #region Variables

        protected CommonElement CmnElement { get; set; }
        protected IConfigurationRoot Configuration { get; set; }
        protected IWebDriver Driver { get; set; }

        protected CommonActions CmnActions { get; set; }

        #endregion Variables

        #region Public methods

        public BasePage(IWebDriver driver) 
        {
            this.Driver = driver;
            this.CmnElement = new CommonElement();
            this.CmnActions = new CommonActions(this.Driver);
            string workingDirectory = Environment.CurrentDirectory;
            string currentProjectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            this.Configuration = new ConfigurationBuilder().AddJsonFile(currentProjectDirectory + "/config/appSettings.json").Build();
        }

        #endregion Public methods
    }
}
