using System;
using System.Collections.Generic;
using System.Text;

namespace BradyChallenge.Models.GenerationReport
{
    public class CoalGenerator : GeneratorWithEmissions
    {
        public double TotalHeatInput { get; set; }
        public double ActualNetGeneration { get; set; }
    }
}
