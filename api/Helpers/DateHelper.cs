using System;

namespace rate_api.Helpers
{
    public static class DateHelper
    {
        public static DateTime? ParseIsoDate(string dateToParse)
        {
            var parsedDate =  DateTime.MinValue;
            if (DateTime.TryParse(dateToParse, out parsedDate))
            {
                return parsedDate;
            }
            return null;
        }
    }
}