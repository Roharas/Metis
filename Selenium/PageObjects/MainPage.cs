using Metis.Applications.Imdtb.Configuration;
using Metis.Applications.Imdtb.Data;
using Metis.Selenium.Enums;
using Metis.Selenium.Extensions;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Metis.Selenium.PageObjects
{
    [Binding]
    public class MainPage
    {
        private readonly TestdataCollection _testdataCollection;
        private readonly IOptions<EnvironmentConfig> _environmentOptions;

        private By BySneakPreview => By.ClassName("sneek-preview__container");
        private By ByLoginButton => By.CssSelector("[href*='/login']");
        private By ByLoginPageForm => By.ClassName("cta__container");
        private By ByUserNameInput => By.CssSelector("[id='username']");
        private By ByPassWordInput => By.CssSelector("[id*='password']");
        private By ByLoginSubmit => By.ClassName("login__submit");
        private By ByClass => By.ClassName("movies");

        public MainPage(TestdataCollection testdataCollection, IOptions<EnvironmentConfig> environmentOptions)
        {
            _testdataCollection = testdataCollection;
            _environmentOptions = environmentOptions;
        }

        public void AssertMainPageToBeDisplayed()
        {
            //Searchforelement to Demo extensions and to include a waiter
            _testdataCollection.Driver.SearchForElement(BySneakPreview);
        }

        //For now all pages in the same pageobject, normally we would split each seperate page in to its own object, sometimes we use a basepage too.
        public void AssertLoginPageToBeDisplayed()
        {
            _testdataCollection.Driver.SearchForElement(ByLoginPageForm);
        }

        public void AssertMoviesPageToBeDisplayed()
        {
            _testdataCollection.Driver.SearchForElement(BySneakPreview);
        }

        public void NavigateToMainPage()
        {
            _testdataCollection.Driver.Navigate().GoToUrl(_environmentOptions.Value.FrontendUrl);
        }

        public void ClickLoginButton()
        {
            _testdataCollection.Driver.FindElement(ByLoginButton).Click();
        }

        public void ClickSubmitButton()
        {
            _testdataCollection.Driver.FindElement(ByLoginSubmit).Click();
        }

        public void EnterUserName()
        {
            _testdataCollection.Driver.FindElement(ByUserNameInput).SendKeys(_testdataCollection.FindCurrentUser().UserName);
        }

        public void EnterPassWord()
        {
            _testdataCollection.Driver.FindElement(ByPassWordInput).SendKeys(_testdataCollection.FindCurrentUser().PassWord);
        }
    }
}