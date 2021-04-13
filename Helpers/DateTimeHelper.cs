using System;

namespace Metis.Helpers
{
    public class DateTimeHelper
    {
        public static string GetDateTimeNowStringValue()
        {
            var dateTimeNowString = $"{DateTime.Now.Year}" +
                                    $"{DateTime.Now.Month}" +
                                    $"{DateTime.Now.Day}" +
                                    $"{DateTime.Now.Hour}" +
                                    $"{DateTime.Now.Minute}" +
                                    $"{DateTime.Now.Second}";
            return dateTimeNowString;
        }
    }
}