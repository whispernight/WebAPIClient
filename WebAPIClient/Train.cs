using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebAPIClient
{

    public class ctatt
    {
        [XmlElement("tmst")]
        public String tmst { get; set; }
        [XmlElement("errCd")]
        public int errCd { get; set; }
        [XmlElement("errNm")]
        public String errNm { get; set; }
        [XmlElement("eta")]
        public eta[] Trains { get; set; }
    }
    //I receive from the WebAPI the Train/s info
    public class eta
    {
        [XmlElement("staId")]
        public int staId { get; set; }
        [XmlElement("stpId")]
        public int stpId { get; set; }
        [XmlElement("staNm")]
        public String staNm { get; set; }
        [XmlElement("stpDe")]
        public String stpDe { get; set; }
        [XmlElement("prdt")]
        public String prdt { get; set; }
        [XmlElement("arrT")]
        public String arrT { get; set; }
    }

    public class showTrains
    {
        [XmlElement("staDirection")]
        public string staDirection { get; set; }
        [XmlElement("timeLeft")]
        public int timeLeft { get; set; }
    
    }
}

