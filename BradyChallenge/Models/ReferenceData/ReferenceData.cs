using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BradyChallenge.Models.ReferenceData
{
    [XmlRoot("ReferenceData")]
    public class ReferenceData
    {
        
        public Factors Factors { get; set; }

    }
}
