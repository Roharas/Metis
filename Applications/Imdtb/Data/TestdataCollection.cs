using System.Collections.Generic;
using System.Linq;
using Metis.Applications.Imdtb.Data.Models;
using Metis.Selenium.Configuration;
using Metis.Selenium.Factory;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;

namespace Metis.Applications.Imdtb.Data
{
    public class TestdataCollection
    {
        public IWebDriver Driver;
        public List<User> Users;
        public List<Movie> Movies;
        public int CurrentUserId;
        public int CurrentMovieId;

        public void CreateNewWebDriver(IOptions<BrowserConfig> browserOptions)
        {
            Driver = new WebDriverFactory(browserOptions).CreateWebDriver();
        }

        public void CreateNewUser()
        {
            var user = new User();
            user.Role = user.SetUserRole();
            user.Active = user.SetActive();
            user.UserName = user.SetUserName();
            user.PassWord = user.SetPassword();
            user.Id = user.SetId(GetUserIdList());
            Users.Add(user);
            CurrentUserId = user.Id;
        }

        public IEnumerable<User> FindAllActiveUsers()
        {
            return Users.Where(user => user.Active);
        }

        public User FindUserById(int id)
        {
            return Users.Find(user => user.Id.Equals(id));
        }

        public User FindCurrentUser()
        {
            return FindUserById(CurrentUserId);
        }

        //FirstOrDefault assuming usernames need to be unique, can add assert later to check if there are no doubles
        public User FindActiveUserByName(string userName)
        {
            var users = FindAllActiveUsers();
            return users.FirstOrDefault(user => user.UserName.ToLowerInvariant().Equals(userName.ToLowerInvariant()));
        }

        private List<int> GetUserIdList()
        {
            return Users.Select(user => user.Id).ToList();
        }
    }
}