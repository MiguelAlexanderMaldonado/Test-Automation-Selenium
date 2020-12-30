using CommonLibs.Implementation;
using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Targets;
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

        protected static Logger logger;

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
            SetLoggerConfiguration();
        }

        public void WaitUntilElementExists(By element)
        {
            this.Wait.Until(ExpectedConditions.ElementExists(element));
        }

        /// <summary>
        /// Configures the logger manager.
        /// </summary>
        private void SetLoggerConfiguration() 
        {
            FileTarget target = (FileTarget)LogManager.Configuration.FindTargetByName("ownFile-web");
            DateTime dateTime = DateTime.Now;
            target.FileName = $"{this.Configuration["nlogDirectory"]}/nlog-{dateTime:yyyyMMdd}.log";
            LogManager.ReconfigExistingLoggers();
            logger = LogManager.GetCurrentClassLogger();
        }

        #endregion Public methods
    }
}
