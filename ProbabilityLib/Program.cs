using System;
using System.Collections.Generic;

namespace ProbabilityLib
{
    class Program
    {
        public delegate double AverageCalculation(int numofExperiments);

        public static double CoinCalculateProbability(int numOfExperiments)
        {
            int success = 0;
            for (int i = 0; i < numOfExperiments; i++)
            {
                var coin = new Coin();
                success = (coin).Side ? success + 1 : success;
            }
            return (double)success / numOfExperiments;
        }

        public static List<double> ProbabilityList(AverageCalculation calculation, int numofExperiments)
        {
            var probs = new List<double>();
            for (int i = 1; i <= 10; i++)
            {
                probs.Add(calculation(numofExperiments));
            }
            return probs;
        }

        static void Main(string[] args)
        {

        }
    }
}
