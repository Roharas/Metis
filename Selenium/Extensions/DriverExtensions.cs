using System;
using System.Threading;
using OpenQA.Selenium;

namespace Metis.Selenium.Extensions
{
    public static class DriverExtensions
    {
        private const int Timeout = 15;

        public static IWebElement SearchForElement(this IWebDriver webDriver, By byElement)
        {
            var dateTimestart = DateTime.Now;
            var dateTimeCurrent = dateTimestart;
            var timeoutTime = dateTimestart.AddSeconds(Timeout);
            IWebElement element = null;

            do
            {
                try
                {
                    element = webDriver.FindElement(byElement);
                    return element;
                }
                catch (NotFoundException)
                {
                    dateTimeCurrent = DateTime.Now;
                    Thread.Sleep(250);
                }
            } while (dateTimeCurrent < timeoutTime);

            if (element == null)
            {
                throw new TimeoutException($"Expected element {byElement} not found within {Timeout} seconds");
            }

            return element;
        }
    }
}