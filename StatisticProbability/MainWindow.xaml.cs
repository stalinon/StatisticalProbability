using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Linq;

namespace StatisticProbability
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Button> list = new List<Button>();
        List<double> probs = new List<double>();

        public MainWindow()
        {
            InitializeComponent();
            list.Add(rec1); list.Add(rec2);
            list.Add(rec3); list.Add(rec4);
            list.Add(rec5); list.Add(rec6);
            list.Add(rec7); list.Add(rec8);
            list.Add(rec9); list.Add(rec10);
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
            TextBox.Text += "\n";
            double aver = 0;
            for (int i = 0; i < 10; i++)
            {
                TextBox.Text += "\t" + Math.Round(probs[i],2);
                list[i].Content = Math.Round(probs[i], 2);
                DoubleAnimation buttonAnimation = new DoubleAnimation
                {
                    From = list[i].ActualHeight,
                    To = probs[i] * 225,
                };
                buttonAnimation.Duration = TimeSpan.FromSeconds(1);
                list[i].BeginAnimation(HeightProperty, buttonAnimation);
                aver += probs[i];
            }
            aver /= 10;
            DoubleAnimation lineAnim = new DoubleAnimation
            {
                From = averageLine.ActualHeight,
                To = 2
            };
            lineAnim.Duration = TimeSpan.FromSeconds(1.5);
            averageLine.BeginAnimation(HeightProperty, lineAnim);
            lineAnim.From = averageLine.ActualHeight;
            lineAnim.To = 50;
            contentLine.BeginAnimation(HeightProperty, lineAnim);


            ThicknessAnimation lineAnimMargin = new ThicknessAnimation
            {
                From = averageLine.Margin,
                To = new Thickness(43, 250 - 225 * aver, 0, 0),
                Duration = TimeSpan.FromSeconds(1.5)
            };
            averageLine.BeginAnimation(MarginProperty, lineAnimMargin);
            contentLine.BeginAnimation(MarginProperty, lineAnimMargin);
            contentLine.Content = Math.Round(aver,2);
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
