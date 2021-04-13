using System.Collections.Generic;
using System.Linq;
using Bogus;
using Metis.Applications.Imdtb.Data.Enums;
using Metis.Helpers;
using NUnit.Framework;

namespace Metis.Applications.Imdtb.Data.Models
{
    public class User
    {
        public UserRole Role { get; set; }
        public bool Active { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public int Id { get; set; }

        public UserRole SetUserRole()
        {
            if (Role == UserRole.Admin) return Role;

            var faker = new Faker<User>();
            faker.RuleFor(get => get.Role, set => UserRole.User);
            Role = faker.Generate().Role;
            return Role;
        }

        public bool SetActive()
        {
            var faker = new Faker<User>();
            faker.RuleFor(get => get.Active, set => true);
            Active = faker.Generate().Active;
            return true;
        }

        public string SetUserName()
        {
            if (!string.IsNullOrEmpty(UserName)) return UserName;

            var faker = new Faker<User>();
            faker.RuleFor(get => get.UserName, set => $"user{DateTimeHelper.GetDateTimeNowStringValue()}");
            UserName = faker.Generate().UserName;
            return UserName;
        }

        public string SetPassword()
        {
            if (!string.IsNullOrEmpty(PassWord)) return PassWord;

            var faker = new Faker<User>();
            faker.RuleFor(get => get.PassWord, set => "testpassword");
            PassWord = faker.Generate().PassWord;
            return PassWord;
        }

        public int SetId(List<int> idList)
        {
            var faker = new Faker<User>();

            if (idList.Count == 0)
            {
                faker.RuleFor(get => get.Id, set => 1);
            }
            else
            {
                var idToSet = idList.Max() + 1;
                faker.RuleFor(get => get.Id, set => idToSet);
            }
            Id = faker.Generate().Id;
            return Id;
        }
    }
}