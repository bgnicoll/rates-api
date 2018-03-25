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

        public override string ToString()
        {
            return "RateId = " + RateId.ToString() + " | DayOfWeek = " + DayOfWeek + " | StartTime = " + StartTime + " | EndTime = " + EndTime + " | Price = " + Price.ToString();
        }

        public override bool Equals(object obj)
        {
            var rate = obj as Rate;
            if (this.DayOfWeek == rate.DayOfWeek && this.StartTime == rate.StartTime && this.EndTime == rate.EndTime && this.Price == rate.Price)
            {
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    
}