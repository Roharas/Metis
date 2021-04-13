using Metis.Applications.Imdtb.Configuration;
using Metis.Applications.Imdtb.Data;
using Metis.Applications.Imdtb.Data.Enums;
using Metis.Applications.Imdtb.Data.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TechTalk.SpecFlow;

namespace Metis.Applications.Imdtb.Api.AllUsers
{
    [Binding]
    public class AllUsersApi : ApiBase
    {
        private readonly TestdataCollection _testdataCollection;

        public AllUsersApi(IOptions<EnvironmentConfig> environmentConfig, TestdataCollection testdataCollection) : base(
            environmentConfig)
        {
            _testdataCollection = testdataCollection;
        }

        /// <summary>
        /// Performs a Get on the allUsers api and stores any users found in the testdatacollection users list
        /// </summary>
        public void GetAllUsers()
        {
            var usersApiResponse = GetApiResponseString($"{_environmentConfig.Value.UsersApi}/api/Users");
            ConvertApiResponseToTestDataCollection(usersApiResponse);
        }

        public void AddCurrentUser()
        {
            //Creates new user and sets it to be the current user
            _testdataCollection.CreateNewUser();
            PostApiRequest($"{_environmentConfig.Value.UsersApi}/api/Users", ConvertTestDataCollectionUserToJson(_testdataCollection.FindCurrentUser()));
        }

        /// <summary>
        /// The users model in the api's response contain strings and no password so we need to convert these to the users that we use in the testdatacollection
        /// </summary>
        /// <param name="usersJson"></param>
        private void ConvertApiResponseToTestDataCollection(string usersJson)
        {
            var allUsers = JsonConvert.DeserializeObject<AllUsersResponse>(usersJson);
            foreach (var apiUser in allUsers.AllUsers)
            {
                _testdataCollection.Users.Add(new User
                {
                    Role = apiUser.Role != null && apiUser.Role.ToLowerInvariant().Equals("admin")
                        ? UserRole.Admin
                        : UserRole.User,
                    Active = apiUser.Active,
                    UserName = apiUser.UserName,
                    Id = apiUser.Id
                });
            }
        }

        private string ConvertTestDataCollectionUserToJson(User user)
        {
            var apiUser = new AllUsersUser
            {
                UserName = user.UserName,
                Role = user.Role == UserRole.Admin ? "admin" : "user",
                Password = user.PassWord,
                Active = true
            };

            return JsonConvert.SerializeObject(apiUser);
        }
    }
}