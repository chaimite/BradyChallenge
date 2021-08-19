using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BradyChallenge.Models.GenerationOutput
{
    [XmlRoot("GenerationOutput")]
    public class GenerationOutput
    {
        [XmlArray]
        [XmlArrayItem(ElementName = "Generator")]
        public List<Generator> Totals { get; set; }
        [XmlArray]
        [XmlArrayItem(ElementName = "Day")]
        public List<Day> MaxEmissionGenerators { get; set; }
        [XmlArray]
        [XmlArrayItem(ElementName = "ActualHeatRate")]
        public List<ActualHeatRate> ActualHeatRates { get; set; }
    }
}
