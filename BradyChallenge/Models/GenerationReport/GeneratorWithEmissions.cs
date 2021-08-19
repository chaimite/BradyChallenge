using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BradyChallenge.Models.GenerationReport
{
    public class GeneratorWithEmissions : Generator
    {
        public double EmissionsRating { get; set; }
    }
}
