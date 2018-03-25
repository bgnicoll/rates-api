using NUnit.Framework;
using rate_api.Helpers;
using rate_api.DataAccess.Models;
using System;
using rate_api.Models;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class RateHelperTests
    {
        [TestCase("sun", DayOfWeek.Sunday)]
        [TestCase("mon", DayOfWeek.Monday)]
        [TestCase("tues", DayOfWeek.Tuesday)]
        [TestCase("wed", DayOfWeek.Wednesday)]
        [TestCase("thurs", DayOfWeek.Thursday)]
        [TestCase("fri", DayOfWeek.Friday)]
        [TestCase("sat", DayOfWeek.Saturday)]
        public void ParseDayOfTheWeek_ShouldReturnDayOfWeekEnum(string dayToParse, DayOfWeek expectedEnumResult)
        {
            var parsedDay = RateHelper.ParseDayOfTheWeek(dayToParse);
            Assert.AreEqual(parsedDay, expectedEnumResult);
        }

        [Test]
        public void ParseDayOfTheWeek_ShouldThrowException()
        {
            Assert.Throws<Exception>(() => RateHelper.ParseDayOfTheWeek("Donnerstag"));
        }

        [Test]
        public void ParseNewRates_ShouldReturnDataAccessRateList()
        {
            var inputList = GetSampleInput();
            var expected = new List<rate_api.DataAccess.Models.Rate>
            {
                new rate_api.DataAccess.Models.Rate() { DayOfWeek = "Monday", StartTime = "0600", EndTime = "1800", Price = 1500},
                new rate_api.DataAccess.Models.Rate() { DayOfWeek = "Tuesday", StartTime = "0600", EndTime = "1800", Price = 1500},
                new rate_api.DataAccess.Models.Rate() { DayOfWeek = "Wednesday", StartTime = "0600", EndTime = "1800", Price = 1500},
                new rate_api.DataAccess.Models.Rate() { DayOfWeek = "Thursday", StartTime = "0600", EndTime = "1800", Price = 1500},
                new rate_api.DataAccess.Models.Rate() { DayOfWeek = "Friday", StartTime = "0600", EndTime = "1800", Price = 1500},
                new rate_api.DataAccess.Models.Rate() { DayOfWeek = "Saturday", StartTime = "0600", EndTime = "2000", Price = 2000},
                new rate_api.DataAccess.Models.Rate() { DayOfWeek = "Sunday", StartTime = "0600", EndTime = "2000", Price = 2000},
            };
            var actual = RateHelper.ParseNewRates(inputList.rates);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void DeserializeRates_JSON_ShouldReturnRatesPost()
        {
            var input = "{ \"rates\": [ { \"days\": \"mon,tues,wed,thurs,fri\", \"times\": \"0600-1800\", \"price\": 1500 }, {\"days\": \"sat,sun\",\"times\": \"0600-2000\",\"price\": 2000}]}";
            var contentType = "application/json";
            var expected = GetSampleInput();
            var actual = RateHelper.DeserializeRates(contentType, input);
            Console.WriteLine(expected.ToString());
            Console.WriteLine(actual.ToString());
            Assert.AreEqual(expected, actual);
        }

        private RatesPost GetSampleInput()
        {
            return new RatesPost
            {
                rates = new List<rate_api.Models.Rate>
                {
                    new rate_api.Models.Rate() { days = "mon,tues,wed,thurs,fri", times = "0600-1800", price = 1500 },
                    new rate_api.Models.Rate() { days = "sat,sun", times = "0600-2000", price = 2000 }
                }
            };
        }
    }
}