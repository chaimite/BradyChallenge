using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BradyChallenge.Models.GenerationReport
{
    [XmlRoot("GenerationReport")]
    public class GenerationReport

    {
        [XmlArray]
        [XmlArrayItem(ElementName = "WindGenerator")]
        public List<WindGenerator> Wind { get; set; }
        [XmlArray]
        [XmlArrayItem(ElementName = "GasGenerator")]
        public List<GasGenerator> Gas { get; set; }
        [XmlArray]
        [XmlArrayItem(ElementName = "CoalGenerator")]
        public List<CoalGenerator> Coal { get; set; }
    }
}
