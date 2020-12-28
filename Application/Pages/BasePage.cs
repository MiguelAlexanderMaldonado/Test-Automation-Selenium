using CommonLibs.Implementation;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Application.Pages
{
    public class BasePage
    {
        #region Variables

        protected CommonElement cmnElement;
        protected IConfigurationRoot configuration;

        #endregion Variables

        #region Public methods

        public BasePage() 
        {
            this.cmnElement = new CommonElement();
            string workingDirectory = Environment.CurrentDirectory;
            string currentProjectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            this.configuration = new ConfigurationBuilder().AddJsonFile(currentProjectDirectory + "/config/appSettings.json").Build();
        }

        #endregion Public methods
    }
}
