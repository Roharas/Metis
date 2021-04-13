using Metis.Selenium.Enums;
using OpenQA.Selenium;

namespace Metis.Selenium.Configuration
{
    public class BrowserConfig
    {
        public Browsers Browser { get; set; }
        public bool AcceptInsecureCertificates { get; set; }
        public bool DisableNotifications { get; set; }
        public bool HideToolBarWhenFullScreen { get; set; }
        public PageLoadStrategy PageLoadStrategy { get; set; }
    }
}