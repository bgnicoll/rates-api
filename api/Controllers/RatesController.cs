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
        public IActionResult Post([FromBody]rate_api.Models.RatesPost rates)
        {
            if (rates != null && rates.rates != null)
            {
                var parsedRates = RateHelper.ParseNewRates(rates.rates);
                var numberOfNewRatesAdded = RateHelper.StoreNewRates(parsedRates, _rateRepo);
                return Ok(numberOfNewRatesAdded);
            }
            return BadRequest();
        }
    }
}
