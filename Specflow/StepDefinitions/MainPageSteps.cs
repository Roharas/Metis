using Metis.Selenium.PageObjects;
using TechTalk.SpecFlow;

namespace Metis.Specflow.StepDefinitions
{
    [Binding]
    public class MainPageSteps
    {
        private readonly MainPage _mainPage;

        public MainPageSteps(MainPage mainPage)
        {
            _mainPage = mainPage;
        }

        [When("wij navigeren naar de Imtdb hoofdpagina")]
        public void WhenWeNavigateToTheImtdbMainPage()
        {
            _mainPage.NavigateToMainPage();
            _mainPage.AssertMainPageToBeDisplayed();
        }

        [Then("kunnen wij met de gebruiker inloggen op Imtdb")]
        public void ThenWeCanLogInOnImtdbWithTheUser()
        {
            _mainPage.ClickLoginButton();
            _mainPage.AssertLoginPageToBeDisplayed();
            _mainPage.EnterUserName();
            _mainPage.EnterPassWord();
            _mainPage.ClickSubmitButton();
            _mainPage.AssertMoviesPageToBeDisplayed();
        }
    }
}