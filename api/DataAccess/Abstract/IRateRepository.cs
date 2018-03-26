using System.Collections.Generic;

namespace rate_api.DataAccess.Abstract
{
    public interface IRateRepository
    {
        int AddRange(List<rate_api.DataAccess.Models.Rate> rates);
        double? RetrieveRateForTimeRange(int start, int end, string day);
    }
}