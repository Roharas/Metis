using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Metis.Applications.Imdtb.Api;
using Metis.Applications.Imdtb.Configuration;
using Metis.Applications.Imdtb.Data.Factory;
using Metis.Selenium.Configuration;
using Metis.Selenium.PageObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolidToken.SpecFlow.DependencyInjection;
using TechTalk.SpecFlow;

namespace Metis.Specflow
{
    public class TestDependencies
    {
        [ScenarioDependencies]
        public static IServiceCollection RegisterServices()
        {
            //All Dependencies in 1 place at scenario startup for now because the solidtoken injection package makes it easy to quickly register dependencys.
            //The ScenarioDependencies are run before the TestController is initialized.

            var services = new ServiceCollection();
            var rootDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            //Here we get all the settings from the jsonfiles and register them as configuration objects
            var applicationSettings = new ConfigurationBuilder()
                .SetBasePath($"{rootDir}/Applications/Imdtb/Configuration/Values")
                .AddJsonFile("ImdtbSettings.json")
                .AddEnvironmentVariables()
                .Build();
            var seleniumSettings = new ConfigurationBuilder()
                .SetBasePath($"{rootDir}/Selenium/Configuration/Values")
                .AddJsonFile("SeleniumSettings.json")
                .AddEnvironmentVariables()
                .Build();
            services.Configure<EnvironmentConfig>(applicationSettings.GetSection("Environment"));
            services.Configure<BrowserConfig>(seleniumSettings.GetSection("Browser"));
            services.AddSingleton<IConfiguration>(_ => applicationSettings);
            services.AddSingleton<IConfiguration>(_ => seleniumSettings);

            //Here we create a new TestdataCollection and register it each time we start a new test scenario.
            services.AddSingleton<TestDataFactory>();
            services.AddSingleton(provider => provider.GetService<TestDataFactory>().CreateNewTestdataCollection());

            //Register each class that contains a SpecFlow [Binding]
            foreach (var type in typeof(TestDependencies).Assembly.GetTypes().Where(t => Attribute.IsDefined((MemberInfo)t, typeof(BindingAttribute))))
            {
                services.AddSingleton(type);
            }

            return services;
        }
    }
}