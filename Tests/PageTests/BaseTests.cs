using Application.Pages;
using AventStack.ExtentReports;
using CommonLibs.Implementation;
using CommonLibs.Properties;
using CommonLibs.Utils;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.IO;

namespace Tests.PageTests
{
    public class BaseTests
    {
        #region Variables

        protected IWebDriver driver;
        protected ExtentReportUtils extentReportUtils;
        protected IConfigurationRoot configuration;

        private string _currentSolutionDirectory;
        private ScreenshotUtils _screenshot;
        private string _screenshotDirectory;
        private readonly int _pageLoadTimeout = 60;

        #endregion Variables

        #region Public methods

        /// <summary>
        /// It execute before SetUp method.
        /// </summary>
        [OneTimeSetUp]
        public void PreSetup()
        {
            string workingDirectory = Environment.CurrentDirectory;
            this._currentSolutionDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;
            string reportFilename = this._currentSolutionDirectory + "/reports/testReport.html";
            this.extentReportUtils = new ExtentReportUtils(reportFilename);
            CheckLogFolders();
        }

        /// <summary>
        /// It execute before the three test case.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string currentProjectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            this.configuration = new ConfigurationBuilder().AddJsonFile(currentProjectDirectory + "/config/appSettings.json").Build();
            this.extentReportUtils.CreateATestCase("Setup");
            string browserType = this.configuration["browserType"];
            try
            {
                if (browserType.Equals(Resource.ChromeType))
                {
                    this.driver = new ChromeDriver();
                }
                else if (browserType.Equals(Resource.EdgeType))
                {
                    this.driver = new EdgeDriver();
                }
                else if (browserType.Equals(Resource.FirefoxType))
                {
                    this.driver = new FirefoxDriver();
                }
                else
                {
                    this.driver = new ChromeDriver();
                }
            }
            catch (Exception e)
            {
                new Exception(Resource.InvalidBrowserType + browserType + e.Message);
            }
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(this._pageLoadTimeout);
            this._screenshot = new ScreenshotUtils(driver);

            this.extentReportUtils.AddTestLog(Status.Info, Resource.BrowserType + " - " + browserType);
        }

        /// <summary>
        /// It execute after every test case.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            string currentExecutionTime = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'ss");
            string screenshotFileName = $"{this._screenshotDirectory}/test-{currentExecutionTime}.png";
            if (TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Failure))
            {
                this.extentReportUtils.AddTestLog(Status.Fail, Resource.OneOrMoreStepFiled);
                this._screenshot.CaptureAndSaveScreenshot(screenshotFileName);
                this.extentReportUtils.AddScreenshot(screenshotFileName);
            }
            this.driver.Quit();
        }

        /// <summary>
        /// It execute once after executing all the tests.
        /// </summary>
        [OneTimeTearDown]
        public void PostCleanUp()
        {
            this.extentReportUtils.FlushReport();
        }

        /// <summary>
        /// Maximizes the browser window.
        /// </summary>
        /// <param name="webDriver">Web driver.</param>
        public void MaximizeBrowser()
        {
            this.driver.Manage().Window.Maximize();
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Creates log folders if is necessary.
        /// </summary>
        private void CheckLogFolders()
        {
            this._screenshotDirectory = $"{this._currentSolutionDirectory}/screenshots/";
            string reportsFolder = $"{this._currentSolutionDirectory}/reports/";
            if (!Directory.Exists(this._screenshotDirectory))
            {
                Directory.CreateDirectory(this._screenshotDirectory);
            }            
            if (!Directory.Exists(reportsFolder))
            {
                Directory.CreateDirectory(reportsFolder);
            }
        }

        #endregion Private methods
    }
}
