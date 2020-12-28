using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace CommonLibs.Utils
{
    public class ExtentReportUtils
    {

        #region Variables

        private readonly ExtentHtmlReporter _extentHtmlReporter;
        private readonly ExtentReports _extentReports;
        private ExtentTest _extentTest;

        #endregion Variables

        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="htmlReportFile">Path and name of html report file</param>
        public ExtentReportUtils(string htmlReportFile)
        {
            this._extentHtmlReporter = new ExtentHtmlReporter(htmlReportFile);
            this._extentReports = new ExtentReports();
            this._extentReports.AttachReporter(this._extentHtmlReporter);
        }

        /// <summary>
        /// Create a test case with the name passed by parameter.
        /// </summary>
        /// <param name="testcaseName">Name of test case.</param>
        public void CreateATestCase(string testcaseName) 
        {
            this._extentTest = this._extentReports.CreateTest(testcaseName);
        }

        /// <summary>
        /// Adding info to the log.
        /// </summary>
        /// <param name="status">Level of info to adding (Info, Error, Debug, etc).</param>
        /// <param name="comment">Comment to add.</param>
        public void AddTestLog(Status status, string comment)
        {
            this._extentTest.Log(status, comment);
        }

        /// <summary>
        /// Add screenshot to the test case.
        /// </summary>
        /// <param name="screenshotFileName"></param>
        public void AddScreenshot(string screenshotFileName) 
        {
            this._extentTest.AddScreenCaptureFromPath(screenshotFileName);
        }

        /// <summary>
        /// Erases any previous data on a relevant report and creates a whole new report.
        /// </summary>
        public void FlushReport() 
        {
            this._extentReports.Flush();
        }

        #endregion Public methods
    }
}
