using System.Collections.Generic;
using rate_api.DataAccess.Abstract;
using rate_api.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

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
            double? rateForTimeRange = null;
            using (var dbContext = new RatesContext())
            {
                var rate = dbContext.Rates.FirstOrDefault(y => y.DayOfWeek == day && end <= y.EndTime && start >= y.StartTime);
                if (rate != null)
                {
                    rateForTimeRange = rate.Price;
                }
            }
            return rateForTimeRange;
        }
    }
}