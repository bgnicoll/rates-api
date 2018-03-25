using System.Collections.Generic;
namespace rate_api.Models
{
    public class RatesPost
    {
        public List<Rate> rates { get; set; }
        public override bool Equals(object obj)
        {
            var ratesPost = obj as RatesPost;
            if (this.rates == ratesPost.rates)
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