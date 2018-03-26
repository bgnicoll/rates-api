using System.Collections.Generic;
using rate_api.DataAccess.Abstract;
using rate_api.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace rate_api.DataAccess.Concrete
{
    public class RateRepository : IRateRepository
    {
        public int AddRange(List<Rate> rates)
        {
            using (var dbContext = new RatesContext())
            {
                dbContext.AddRange(rates);
                return dbContext.SaveChanges();
            }
        }

        public double? RetrieveRateForTimeRange(int start, int end, string day)
        {
            var query = $"SELECT * FROM \"Rates\" WHERE \"DayOfWeek\" = '{day}' AND \"StartTime\" >= {start} AND  {end} <= \"EndTime\" ORDER BY \"RateId\" DESC LIMIT 1";
            double? rateForTimeRange = null;
            using (var dbContext = new RatesContext())
            {
                var rate = dbContext.Rates.FromSql(query).FirstOrDefault();
                if (rate != null)
                {
                    rateForTimeRange = rate.Price;
                }
            }
            return rateForTimeRange;
        }
    }
}