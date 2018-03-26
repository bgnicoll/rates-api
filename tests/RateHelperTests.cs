using NUnit.Framework;
using rate_api.Helpers;
using rate_api.DataAccess.Models;
using System;
using rate_api.Models;
using System.Collections.Generic;
using NSubstitute;
using rate_api.DataAccess.Abstract;

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
            var inputList = GetSampleInputA();
            var expected = new List<rate_api.DataAccess.Models.Rate>
            {
                new rate_api.DataAccess.Models.Rate() { DayOfWeek = "Monday", StartTime = 600, EndTime = 1800, Price = 1500},
                new rate_api.DataAccess.Models.Rate() { DayOfWeek = "Tuesday", StartTime = 600, EndTime = 1800, Price = 1500},
                new rate_api.DataAccess.Models.Rate() { DayOfWeek = "Wednesday", StartTime = 600, EndTime = 1800, Price = 1500},
                new rate_api.DataAccess.Models.Rate() { DayOfWeek = "Thursday", StartTime = 600, EndTime = 1800, Price = 1500},
                new rate_api.DataAccess.Models.Rate() { DayOfWeek = "Friday", StartTime = 600, EndTime = 1800, Price = 1500},
                new rate_api.DataAccess.Models.Rate() { DayOfWeek = "Saturday", StartTime = 600, EndTime = 2000, Price = 2000},
                new rate_api.DataAccess.Models.Rate() { DayOfWeek = "Sunday", StartTime = 600, EndTime = 2000, Price = 2000},
            };
            var actual = RateHelper.ParseNewRates(inputList.rates);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void DeserializeRates_JSON_ShouldReturnRatesPost()
        {
            var input = "{ \"rates\": [ { \"days\": \"mon\", \"times\": \"0600-1800\", \"price\": 1500 }, {\"days\": \"sat\",\"times\": \"0600-2000\",\"price\": 2000}]}";
            var contentType = "application/json";
            var expected = GetSampleInputB();
            var actual = RateHelper.DeserializeRates(contentType, input);
            Assert.AreEqual(expected.rates[0].days, actual.rates[0].days);
        }

        // [Test]
        // public void DeserializeRates_XML_ShouldReturnRatesPost()
        // {
        //     var input = "<RatesPost><Rates><Days>mon</Days><Price>1500</Price><Times>0600-1800</Times><Days>sat</Days><Price>2000</Price><Times>0600-2000</Times></Rates></RatesPost>";
        //     var contentType = "application/xml";
        //     var expected = GetSampleInputB();
        //     var actual = RateHelper.DeserializeRates(contentType, input);
        //     Assert.AreEqual(expected.rates[0].days, actual.rates[0].days);
        // }

        [Test]
        public void CalculatePrice_ShouldReturnPrice()
        {
            double? expected = 1000;
            var _rateRepo = Substitute.For<IRateRepository>();
            _rateRepo.RetrieveRateForTimeRange(500, 1000, "Saturday").Returns(expected);

            var startDate = new DateTime(2018, 3, 24, 5, 0, 0);
            var endDate = new DateTime(2018, 3, 24, 10, 0, 0);

            var actual = RateHelper.CalculatePrice(startDate, endDate, _rateRepo);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CalculatePrice_DifferentStartAndEndDays_ShouldReturnNull()
        {
            double? expected = null;
            var _rateRepo = Substitute.For<IRateRepository>();

            var startDate = new DateTime(2018, 3, 24, 5, 0, 0);
            var endDate = new DateTime(2018, 3, 25, 5, 0, 0);

            var actual = RateHelper.CalculatePrice(startDate, endDate, _rateRepo);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CalculatePrice_UnavailableRange_ShouldReturnNull()
        {
            double? expected = null;
            var _rateRepo = Substitute.For<IRateRepository>();
            _rateRepo.RetrieveRateForTimeRange(500, 1000, "Saturday").Returns(expected);

            var startDate = new DateTime(2018, 3, 24, 5, 0, 0);
            var endDate = new DateTime(2018, 3, 24, 10, 0, 0);

            var actual = RateHelper.CalculatePrice(startDate, endDate, _rateRepo);

            Assert.AreEqual(expected, actual);
        }

        private RatesPost GetSampleInputA()
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
        private RatesPost GetSampleInputB()
        {
            return new RatesPost
            {
                rates = new List<rate_api.Models.Rate>
                {
                    new rate_api.Models.Rate() { days = "mon", times = "0600-1800", price = 1500 },
                    new rate_api.Models.Rate() { days = "sat", times = "0600-2000", price = 2000 }
                }
            };
        }
    }
}