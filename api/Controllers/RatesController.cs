using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
        }

        // POST api/rates
        [HttpPost]
        public IActionResult Post([FromBody]RatesPost rates)
        {
            //var deserializedRates = RateHelper.DeserializeRates(Request.ContentType, rates);
            var deserializedRates = rates;
            if (deserializedRates != null && deserializedRates.rates != null)
            {
                var parsedRates = new List<DataAccess.Models.Rate>();
                try
                {
                    parsedRates = RateHelper.ParseNewRates(deserializedRates.rates);
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
