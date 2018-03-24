using System;
namespace rate_api.DataAccess.Models
{
    public class Rate
    {
        public int RateId { get; set; }
        public string DayOfWeek { get; set; }
        public string StartTime { get; set; }   
        public string EndTime { get; set; } 
        public double Price { get; set; }
    }
}