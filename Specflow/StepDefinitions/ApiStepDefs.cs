using Metis.Applications.Imdtb.Api.AllUsers;
using TechTalk.SpecFlow;

namespace Metis.Specflow.StepDefinitions
{
    [Binding]
    public class ApiStepDefs
    {
        private readonly AllUsersApi _usersApi;

        public ApiStepDefs(AllUsersApi usersApi)
        {
            _usersApi = usersApi;
        }

        [When("wij de Users api vragen om een lijst met alle gebruikers")]
        public void WhenWeAskTheUsersApiForAListWithAllUsers()
        {
            _usersApi.GetAllUsers();
        }

        [When("wij een nieuwe user aanmaken via de api")]
        public void WhenWeCreateANewUserUsingTheApi()
        {
            _usersApi.AddCurrentUser();
        }
    }
}