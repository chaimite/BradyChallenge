using BradyChallenge.Models.GenerationReport;
using BradyChallenge.Models.ReferenceData;
using BradyChallenge.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using OutputGenerator = BradyChallenge.Models.GenerationOutput.Generator;
using InputGenerator = BradyChallenge.Models.GenerationReport.Generator;
using System.Linq;
using BradyChallenge.Utils;
using Day = BradyChallenge.Models.GenerationOutput.Day;
using BradyChallenge.Models.GenerationOutput;

namespace BradyChallenge.Controller
{
    public class GeneratorController
    {
        private readonly string fileLocation;
        private readonly string inputFileName;
        private readonly string outputFileName;
        private readonly string referenceDataFileName;
        private readonly ReferenceData referenceData;
        private readonly IOService iOService;

        public GeneratorController(IOService iOService)
        {
            fileLocation = GetFileLocation();
            inputFileName = GetInputFileName();
            outputFileName = GetOutputFileName();
            referenceDataFileName = GetReferenceDataFileName();
            this.iOService = iOService;
            this.iOService.WatchFile(fileLocation, inputFileName, AnalyzeGenerationReport);
            referenceData = ReadReferenceDataXMLFromLocation();
        }
        public void AnalyzeGenerationReport (object sender, FileSystemEventArgs e)
        {
            if (!e.Name.Contains(inputFileName))
            {
                return;
            }
            
            Console.WriteLine("The input was changed.");

            GenerationReport generationReport = ReadInputDataXMLFromLocation();

            List<OutputGenerator> Totals = new List<OutputGenerator>();
            Totals.AddRange(SumOfGenerationValues(referenceData.Factors.ValueFactor.Medium, generationReport.Coal));
            Totals.AddRange(SumOfGenerationValues(referenceData.Factors.ValueFactor.Medium, generationReport.Gas));
            Totals.AddRange(SumOfGenerationValues(referenceData.Factors.ValueFactor.Low, 
                generationReport.Wind.FindAll(wg => wg.Name.ToLower().Contains("offshore"))));
            Totals.AddRange(SumOfGenerationValues(referenceData.Factors.ValueFactor.High,
                generationReport.Wind.FindAll(wg => wg.Name.ToLower().Contains("onshore"))));

            List<Day> coalEmissions = DailyEmissions(referenceData.Factors.EmissionsFactor.High, generationReport.Coal);

            List<Day> gasEmissions = DailyEmissions(referenceData.Factors.EmissionsFactor.Medium, generationReport.Gas);
            List<Day> MaxEmissionGenerators = HighestDailyEmissions(coalEmissions, gasEmissions);

            List<ActualHeatRate> actualHeatRates = new List<ActualHeatRate>();
            actualHeatRates.AddRange(generationReport.Coal.Select(cg => new ActualHeatRate()
            {
                Name = cg.Name,
                HeatRate = cg.TotalHeatInput / cg.ActualNetGeneration
            }));

            GenerationOutput generationOutput = new GenerationOutput()
            {
                Totals = Totals,
                MaxEmissionGenerators = MaxEmissionGenerators,
                ActualHeatRates = actualHeatRates
            };

            this.iOService.WriteXMLFromLocation<GenerationOutput>(fileLocation + outputFileName, generationOutput);
        }

        private List<Day> HighestDailyEmissions(List<Day> coalEmissions, List<Day> gasEmissions)
        {
            List<Day> highestEmissions = new List<Day>();

            for (int i = 0; i < coalEmissions.Count; i++)
            {
                Day gasEmission = coalEmissions[i];

                Day coalEmission = gasEmissions[i];

                highestEmissions.Add(gasEmission.Emission > coalEmission.Emission ?
                    gasEmission : coalEmission);
            }
            return highestEmissions;
        }

        private List<OutputGenerator> SumOfGenerationValues<T>(double referenceData, List<T> generators ) where T : InputGenerator
        {
            List <OutputGenerator> outputGenerators = new List<OutputGenerator>();
            foreach (var inputGenerator in generators)
            {
                OutputGenerator outputGenerator = new OutputGenerator();
                outputGenerator.Name = inputGenerator.Name;
                outputGenerator.Total = inputGenerator.Generation.Sum(x =>
                    Calculations.DailyGenerationValue(x.Energy, x.Price, referenceData));
                outputGenerators.Add(outputGenerator);
            }
            return outputGenerators;
        }
        private List<Day> DailyEmissions<T>(double emissionFactor, List<T> generators) where T : GeneratorWithEmissions
        {
            List<Day> days = new List<Day>();
            foreach (var inputGenerator in generators)
            {
                foreach (var day in inputGenerator.Generation)
                {
                    Day outputDay = new Day();
                    outputDay.Name = inputGenerator.Name;
                    outputDay.Date = day.Date;
                    outputDay.Emission = Calculations.DailyEmission(day.Energy, inputGenerator.EmissionsRating, emissionFactor);
                    days.Add(outputDay);
                }
            }
            return days;
        }
        private ReferenceData ReadReferenceDataXMLFromLocation()
        {   
            var filePath = fileLocation + referenceDataFileName;
            return iOService.ReadXMLFromLocation<ReferenceData>(filePath);
        }
        private GenerationReport ReadInputDataXMLFromLocation()
        {
            var filePath = fileLocation + inputFileName;
            return iOService.ReadXMLFromLocation<GenerationReport>(filePath);
        }
        private string GetInputFileName()
        {
            return ConfigurationManager.AppSettings["inputFileName"];
        }
        private string GetOutputFileName()
        {
            return ConfigurationManager.AppSettings["outputFileName"];
        }
        private string GetFileLocation()
        {
            return ConfigurationManager.AppSettings["filelocation"];
        }
        private string GetReferenceDataFileName()
        {
            return ConfigurationManager.AppSettings["referenceDataFileName"];
        }
    }
}
