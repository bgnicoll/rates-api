using System.Xml.Serialization;

namespace rate_api.Models
{
    public class Rate
    {
        [XmlElement(ElementName = "Days")]
        public string days { get; set; }
        [XmlElement(ElementName = "Times")]
        public string times { get; set; }
        [XmlElement(ElementName = "Price")]
        public double price { get; set; }
    }
}