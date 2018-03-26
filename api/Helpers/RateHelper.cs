using System;
using System.Collections.Generic;
using System.Linq;
using rate_api.DataAccess.Abstract;
using rate_api.DataAccess.Models;
using rate_api.Models;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace rate_api.Helpers
{
    public static class RateHelper
    {
        public static List<rate_api.DataAccess.Models.Rate> ParseNewRates(List<rate_api.Models.Rate> rates)
        {
            var parsedRates = new List<rate_api.DataAccess.Models.Rate>();
            foreach(var rate in rates)
            {
                var timesArray = rate.times.Split('-');
                if (timesArray.Length > 1)
                {
                    var startTime = timesArray[0];
                    var endTime = timesArray[1];
                    var days = rate.days.Split(',');
                    for(int i = 0; i <= days.Length - 1; i++)
                    {
                        var parsedRate = new rate_api.DataAccess.Models.Rate();
                        parsedRate.DayOfWeek = Enum.GetName(typeof(DayOfWeek), ParseDayOfTheWeek(days[i]));
                        int startTimeParse = 0;
                        int.TryParse(startTime, out startTimeParse);
                        parsedRate.StartTime = startTimeParse;
                        int endTimeParse = 0;
                        int.TryParse(endTime, out endTimeParse);
                        parsedRate.EndTime = endTimeParse;
                        parsedRate.Price = rate.price;
                        parsedRates.Add(parsedRate);
                    }
                }
            }
            return parsedRates;
        }
        public static int StoreNewRates(List<rate_api.DataAccess.Models.Rate> rates, IRateRepository rateRepo)
        {
            if (rates != null && rates.Any())
            {
                return rateRepo.AddRange(rates);
            }
            return 0;
        }
        public static DayOfWeek ParseDayOfTheWeek(string abbreviatedDay)
        {
            switch (abbreviatedDay)
            {
                case "sun":
                return DayOfWeek.Sunday;
                case "mon":
                return DayOfWeek.Monday;
                case "tues":
                return DayOfWeek.Tuesday;
                case "wed":
                return DayOfWeek.Wednesday;
                case "thurs":
                return DayOfWeek.Thursday;
                case "fri":
                return DayOfWeek.Friday;
                case "sat":
                return DayOfWeek.Saturday;
                default :
                throw new Exception();
            }
        }
        public static RatesPost DeserializeRates(string contentType, string rawRates)
        {
            var rates = new RatesPost();
            switch (contentType)
            {
                case "application/json":
                    rates = JsonConvert.DeserializeObject<RatesPost>(rawRates);
                break;
                case "application/xml":
                case "text/xml":
                    var serializer = new XmlSerializer(typeof(RatesPost));
                    using (var reader = XmlReader.Create(new StringReader(rawRates)))
                    {
                        rates = (RatesPost)serializer.Deserialize(reader);
                    }
                break;
                case "application/x-protobuf":
                case "application/protobuf":
                case "application/octet-stream":

                break;
            }
            return rates;
        }

        public static double? CalculatePrice(DateTime start, DateTime end, IRateRepository rateRepo)
        {
            if (start.DayOfWeek != end.DayOfWeek)
            {
                //Requirements and given data indicated time span can not be multiple days
                return null;
            }

            var startTime = start.Hour.ToString("D2") + start.Minute.ToString("D2"); 
            int startTimeAsFourDigitInt = 0;
            int.TryParse(startTime, out startTimeAsFourDigitInt);

            var endTime = end.Hour.ToString("D2") + end.Minute.ToString("D2");
            int endTimeAsFourDigitInt = 0;
            int.TryParse(endTime, out endTimeAsFourDigitInt);

            var price = rateRepo.RetrieveRateForTimeRange(startTimeAsFourDigitInt, endTimeAsFourDigitInt, start.DayOfWeek.ToString());
            return price;
        }

        public static string FormatPrice(double price)
        {
            var format = price % 1 == 0 ? "#,0" : "#,0.00";
            return(price.ToString(format));
        }
    }
}