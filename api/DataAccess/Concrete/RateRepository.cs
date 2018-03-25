using System.Collections.Generic;
using rate_api.DataAccess.Abstract;
using rate_api.DataAccess.Models;

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
    }
}