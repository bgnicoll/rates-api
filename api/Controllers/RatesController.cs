using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using rate_api.DataAccess.Abstract;
using rate_api.Helpers;

namespace rate_api.Controllers
{
    [Route("api/[controller]")]
    public class RatesController : Controller
    {
        private IRateRepository _rateRepo;
        public RatesController(IRateRepository rateRepo)
        {
            _rateRepo = rateRepo;
        }
        // GET api/rates/?start=2015-07-01T07:00:00Z&end=2015-07-01T12:00:00Z&format=xml
        [HttpGet]
        public IActionResult Get(string start, string end, string format = "json")
        {
            var parsedStartDate = DateHelper.ParseIsoDate(start);
            var parsedEndDate = DateHelper.ParseIsoDate(end);
            if (parsedStartDate == null || parsedEndDate == null)
            {
                return BadRequest();
            }
            return Ok(parsedStartDate?.ToString() ?? "unavailable");
        }

        // POST api/rates
        [HttpPost]
        public IActionResult Post([FromBody]string rawRates)
        {
            var rates = RateHelper.DeserializeRates(Request.ContentType, rawRates);
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
                    return Ok(numberOfNewRatesAdded + " new rates added");
                }
                catch
                {
                    return  StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                }
            }
            return BadRequest();
        }
    }
}
