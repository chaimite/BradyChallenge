using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BradyChallenge.Models.GenerationReport
{
    public class Generator
    {
        public string Name { get; set; }
        [XmlArray]
        [XmlArrayItem(ElementName = "Day")]
        public List<Day> Generation { get; set; }
    }
}
