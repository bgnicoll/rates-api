using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace rate_api.Models
{
    [XmlRoot] 
    public class RatesPost
    {
        [XmlArray("rates")]
        public List<Rate> rates { get; set; }
    }
}