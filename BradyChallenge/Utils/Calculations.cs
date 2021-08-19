namespace BradyChallenge.Utils
{
    public static class Calculations
    {
        public static double DailyGenerationValue(double energy, double price, double valueFactor)
        {
            return energy * price * valueFactor;
        }
        public static double DailyEmission(double energy, double emissionRating, double emissionFactor)
        {
            return energy * emissionRating * emissionFactor;
        }
        public static double ActualHeatRate(double totalHeatInput, double actualNetGeneration)
        {
            return totalHeatInput/actualNetGeneration;
        }
    }
}
