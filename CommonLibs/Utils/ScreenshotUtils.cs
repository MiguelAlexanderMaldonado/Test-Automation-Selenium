using CommonLibs.Properties;
using OpenQA.Selenium;
using System;
using System.IO;

namespace CommonLibs.Utils
{
    public class ScreenshotUtils
    {
        private ITakesScreenshot _camera;

        #region Public methods

        public ScreenshotUtils(IWebDriver driver) 
        {
            this._camera = (ITakesScreenshot)driver;
        }

        public void CaptureAndSaveScreenshot(string filename) 
        {
            _ = filename.Trim();
            if (File.Exists(filename)) 
            {
                throw new Exception(Resource.FileExists);
            }
            Screenshot screenshot = this._camera.GetScreenshot();
            screenshot.SaveAsFile(filename);
        }

        #endregion Public methods
    }
}
