using System.Collections.Generic;
using Metis.Applications.Imdtb.Data.Models;

namespace Metis.Applications.Imdtb.Data.Factory
{
    public class TestDataFactory
    {
        public TestdataCollection CreateNewTestdataCollection()
        {
            var testDataCollection = new TestdataCollection();
            const int defaultId = 0;

            testDataCollection.Driver = null;
            testDataCollection.Users = new List<User>();
            testDataCollection.Movies = new List<Movie>();
            testDataCollection.CurrentUserId = defaultId;
            testDataCollection.CurrentMovieId = defaultId;

            return testDataCollection;
        }
    }
}