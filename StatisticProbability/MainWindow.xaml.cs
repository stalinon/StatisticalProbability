using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace StatisticProbability
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        public delegate double AverageCalculation(int numofExperiments, int variant);

        public static double CoinCalculateProbability(int numOfExperiments, int variant)
        {
            int success = 0;
            for (int i = 0; i < numOfExperiments; i++)
            {
                var coin = new Coin();
                success = (coin).Side ? success + 1 : success;
            }
            return (double)success / numOfExperiments;
        }

        public static double DiceCalculateProbability(int numOfExperiments, int variant)
        {
            int success = 0;
            for (int i = 0; i < numOfExperiments; i++)
            {
                var dice = new Dice();
                success = (dice).Side == variant ? success + 1 : success;
            }
            return (double)success / numOfExperiments;
        }

        public static List<double> ProbabilityList(AverageCalculation calculation, int numofExperiments, int variant)
        {
            var probs = new List<double>();
            for (int i = 1; i <= 10; i++)
            {
                probs.Add(calculation(numofExperiments, 1));
            }
            return probs;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text = "";
            var probs = new List<double>();
            if (TypeOfExperiment.Text == "Coin")
            {
                AverageCalculation calculation = CoinCalculateProbability;
                probs = ProbabilityList(calculation, (int)NumOfExperiments.Value, 1);
            }
            else if (TypeOfExperiment.Text == "Dice")
            {
                AverageCalculation calculation = DiceCalculateProbability;
                probs = ProbabilityList(calculation, (int)NumOfExperiments.Value, Convert.ToInt32(DiceSide.Text));
            }
            for (int i = 0; i < 10; i++)
            {
                TextBox.Text += (i+1) + "\t";
            }
            TextBox.Text += "\n";
            for (int i = 0; i < 10; i++)
            {
                TextBox.Text += Math.Round(probs[i],2) + "\t";
            }
            
        }

        private void ComboBox_Closing(object sender, EventArgs e)
        {
            if (TypeOfExperiment.Text == "Dice")
            {
                CanvasDice.Visibility = Visibility.Visible;
            }
            else if (TypeOfExperiment.Text == "Coin")
            {
                CanvasDice.Visibility = Visibility.Hidden;
            }
        }
    }
}
