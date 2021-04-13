using System;
using Metis.Selenium.Configuration;
using Metis.Selenium.Enums;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Metis.Selenium.Factory
{
    public class WebDriverFactory
    {
        private readonly BrowserConfig _browserConfig;

        public WebDriverFactory(IOptions<BrowserConfig> browserConfig)
        {
            _browserConfig = browserConfig.Value;
        }

        public IWebDriver CreateWebDriver()
        {
            switch (_browserConfig.Browser)
            {
                case Browsers.Firefox:
                    var firefoxOptions = new FirefoxOptions();
                    GetFirefoxOptions(firefoxOptions);
                    return new FirefoxDriver(FirefoxSlownessWorkaround(), firefoxOptions);

                case Browsers.Chrome:
                    var chromeOptions = new ChromeOptions();
                    GetChromeOptions(chromeOptions);
                    return new ChromeDriver(chromeOptions);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void GetChromeOptions(ChromeOptions chromeOptions)
        {
            if (_browserConfig.DisableNotifications)
            {
                chromeOptions.AddArguments("--disable-notifications");
            }
            chromeOptions.AddUserProfilePreference("browser.fullscreen.autohide", _browserConfig.HideToolBarWhenFullScreen);
            chromeOptions.AcceptInsecureCertificates = _browserConfig.AcceptInsecureCertificates;
            chromeOptions.PageLoadStrategy = _browserConfig.PageLoadStrategy;
        }

        private void GetFirefoxOptions(FirefoxOptions firefoxOptions)
        {
            if (_browserConfig.DisableNotifications)
            {
                firefoxOptions.AddArguments("--disable-notifications");
            }
            firefoxOptions.SetPreference($"browser.fullscreen.autohide", _browserConfig.HideToolBarWhenFullScreen);
            firefoxOptions.AcceptInsecureCertificates = _browserConfig.AcceptInsecureCertificates;
            firefoxOptions.PageLoadStrategy = _browserConfig.PageLoadStrategy;
        }

        private static FirefoxDriverService FirefoxSlownessWorkaround()
        {
            //Without this fix the geckodriver for firefox is extremely slow and will take close to 8 minutes while other browserdrivers can run it in around 10 seconds
            //It is a known issue with the ipv4/ipv6 callback listners in the driver specifically for .NET core. https://github.com/SeleniumHQ/selenium/issues/6597
            var ffds = FirefoxDriverService.CreateDefaultService();
            ffds.Host = "::1";
            return ffds;
        }
    }
}