using Metis.Applications.Imdtb.Data;
using Metis.Selenium.Configuration;
using Microsoft.Extensions.Options;
using TechTalk.SpecFlow;

namespace Metis.Specflow
{
    [Binding]
    public class TestController
    {
        /// <summary>
        /// The Testcontroller can be used to regulate what happens before or after testruns, features, and scenario's
        /// It also provides the possibility to do this based on tags and priority orders (lowest first, highest last)
        /// </summary>
        private readonly TestdataCollection _testdataCollection;

        private readonly IOptions<BrowserConfig> _browserOptions;

        public TestController(TestdataCollection testdataCollection, IOptions<BrowserConfig> browserOptions)
        {
            _testdataCollection = testdataCollection;
            _browserOptions = browserOptions;
        }

        [BeforeScenario("Web", Order = 1)]
        public void BeforeEachWebScenario()
        {
            _testdataCollection.CreateNewWebDriver(_browserOptions);
        }

        [AfterScenario("Web", Order = 1)]
        public void AfterEachWebScenario()
        {
            _testdataCollection.Driver.Quit();
        }
    }
}