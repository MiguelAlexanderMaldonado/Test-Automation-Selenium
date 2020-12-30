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
            // Loads the project directories.
            string workingDirectory = Environment.CurrentDirectory;
            this._currentSolutionDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;
            string currentProjectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            // Defines the name of report file.
            string reportFilename = this._currentSolutionDirectory + "/reports/testReport.html";
            this.extentReportUtils = new ExtentReportUtils(reportFilename);
            // Loads the configuration file.
            this.configuration = new ConfigurationBuilder().AddJsonFile(currentProjectDirectory + "/config/appSettings.json").Build();
            // Checks the logs directories.
            CheckLogFolders();
        }

        /// <summary>
        /// It execute before the three test case.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            // Creates a new test case.
            this.extentReportUtils.CreateATestCase("Setup");
            // Loads the driver type.
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
            // Sets the timeout to page load.
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(this._pageLoadTimeout);
            // Defines the screenshots utility.
            this._screenshot = new ScreenshotUtils(driver);
            // Adds test log info.
            this.extentReportUtils.AddTestLog(Status.Info, Resource.BrowserType + " - " + browserType);
        }

        /// <summary>
        /// It execute after every test case.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            // Defines the screenshot file name.
            string currentExecutionTime = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'ss");
            string screenshotFileName = $"{this._screenshotDirectory}/test-{currentExecutionTime}.png";
            // Checks if the test case failed.
            if (TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Failure))
            {
                // Adds test log info.
                this.extentReportUtils.AddTestLog(Status.Fail, Resource.OneOrMoreStepFiled);
                // Takes a screenshot and save it.
                this._screenshot.CaptureAndSaveScreenshot(screenshotFileName);
                // Adds the screenshot to the report.
                this.extentReportUtils.AddScreenshot(screenshotFileName);
            }
        }

        /// <summary>
        /// It execute once after executing all the tests.
        /// </summary>
        [OneTimeTearDown]
        public void PostCleanUp()
        {
            this.extentReportUtils.FlushReport();
            this.driver.Close();
            this.driver.Quit();
        }

        /// <summary>
        /// Maximizes the browser window.
        /// </summary>
        /// <param name="webDriver">Web driver.</param>
        public void MaximizeBrowser()
        {
            this.driver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Defines the report context to the report log.
        /// </summary>
        /// <param name="testCaseName">Case name.</param>
        /// <param name="testLog">Log text.</param>
        public void SetTestCaseReportLog(string testCaseName, string testLog)
        {
            this.extentReportUtils.CreateATestCase(testCaseName);
            this.extentReportUtils.AddTestLog(Status.Info, testLog);
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
                Directory.CreateDirectory(this._screenshotDirectory);
            
            if (!Directory.Exists(reportsFolder))
                Directory.CreateDirectory(reportsFolder);            
        }

        #endregion Private methods
    }
}
