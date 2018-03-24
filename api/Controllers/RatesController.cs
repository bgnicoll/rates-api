using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rate_api.Helpers;

namespace rate_api.Controllers
{
    [Route("api/[controller]")]
    public class RatesController : Controller
    {
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
        public void Post([FromBody]string value)
        {
            
        }

        // PUT api/rates/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/rates/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
