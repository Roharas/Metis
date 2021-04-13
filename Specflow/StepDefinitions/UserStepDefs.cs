using FluentAssertions;
using Metis.Applications.Imdtb.Data;
using TechTalk.SpecFlow;

namespace Metis.Specflow.StepDefinitions
{
    [Binding]
    public class UserStepDefs
    {
        private readonly TestdataCollection _testdataCollection;

        public UserStepDefs(TestdataCollection testdataCollection)
        {
            _testdataCollection = testdataCollection;
        }

        [Then("zien wij dat er een actieve gebruiker genaamd (.*) bestaat")]
        public void ThenWeSeeAnActiveUserWithNameExists(string userName)
        {
            _testdataCollection.FindActiveUserByName(userName).Should().NotBeNull("we are sure we the account exists");
        }
    }
}