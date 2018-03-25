using System;
using System.Collections.Generic;
using System.Linq;
using rate_api.DataAccess.Abstract;
using rate_api.DataAccess.Models;
using rate_api.Models;

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
                    var startTime = timesArray[0];
                    var endTime = timesArray[1];
                    var days = rate.days.Split(',');
                    for(int i = 0; i <= days.Length - 1; i++)
                    {
                        var parsedRate = new rate_api.DataAccess.Models.Rate();
                        parsedRate.DayOfWeek = Enum.GetName(typeof(DayOfWeek), ParseDayOfTheWeek(days[i]));
                        parsedRate.StartTime = startTime;
                        parsedRate.EndTime = endTime;
                        parsedRate.Price = rate.price;
                        parsedRates.Add(parsedRate);
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
    }
}