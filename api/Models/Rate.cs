namespace rate_api.Models
{
    public class Rate
    {
        public string days { get; set; }
        public string times { get; set; }
        public double price { get; set; }

        public override bool Equals(object obj)
        {
            var rate = obj as Rate;
            if (this.days == rate.days && this.times == rate.times && this.price == rate.price)
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