using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Prometheus;
using rate_api.DataAccess.Abstract;
using rate_api.Helpers;
using rate_api.Models;

namespace rate_api.Controllers
{
    [Route("api/[controller]")]
    public class RatesController : Controller
    {
        private IRateRepository _rateRepo;
        private readonly string UnavailablePrice = "unavailable";
        public RatesController(IRateRepository rateRepo)
        {
            _rateRepo = rateRepo;
        }
        // GET api/rates/?start=2015-07-01T07:00:00Z&end=2015-07-01T12:00:00Z
        [HttpGet]
        public IActionResult Get(string start, string end)
        {
            var latencyStopWatch = new Stopwatch();
            latencyStopWatch.Start();
            var latencyHistogram = Metrics.CreateHistogram("rate_api_rate_get_request_duration_ms", "Duration of a request to the get endpoint");

            var getRateCounter = Metrics.CreateCounter("rate_api_get_requests_total", "Expresses how often the rates GET endpoint of the rate API is used.");
            getRateCounter.Inc();

            var parsedStartDate = DateHelper.ParseIsoDate(start);
            var parsedEndDate = DateHelper.ParseIsoDate(end);
            if (!parsedStartDate.HasValue || !parsedEndDate.HasValue)
            {
                return BadRequest();
            }
            try
            {
                var price = RateHelper.CalculatePrice(parsedStartDate.Value, parsedEndDate.Value, _rateRepo);
                if (price.HasValue)
                {
                    return Json(new {price = RateHelper.FormatPrice(price.Value)});
                }
                return Json(new {price = UnavailablePrice});
            }
            catch
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
            }
            finally
            {
                latencyStopWatch.Stop();
                latencyHistogram.Observe(latencyStopWatch.ElapsedMilliseconds);
            }
        }

        // POST api/rates
        [HttpPost]
        public IActionResult Post([FromBody]RatesPost rates)
        {
            var postRateCounter = Metrics.CreateCounter("rate_api_post_requests_total", "Expresses how often the rates POST endpoint of the rate API is used.");
            postRateCounter.Inc();

            if (rates != null && rates.rates != null)
            {
                var parsedRates = new List<DataAccess.Models.Rate>();
                try
                {
                    parsedRates = RateHelper.ParseNewRates(rates.rates);
                }
                catch
                {
                    return BadRequest();
                }
                try
                {
                    var numberOfNewRatesAdded = RateHelper.StoreNewRates(parsedRates, _rateRepo);
                    return Json(new { newRates = numberOfNewRatesAdded});
                }
                catch
                {
                    return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                }
            }
            return BadRequest();
        }
    }
}
