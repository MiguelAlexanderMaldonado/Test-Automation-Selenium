using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonLibs.Implementation
{
    public class CommonActions
    {
        #region Variables

        private readonly Actions actions;

        #endregion Variables

        #region Public methods

        public CommonActions(IWebDriver driver)
        {
            actions = new Actions(driver);
        }

        public void ContextClick(IWebElement element) => this.actions.ContextClick(element).Build().Perform();

        #endregion Public methods

    }
}
