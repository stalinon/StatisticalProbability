using System;

namespace StatisticProbability
{
    public class Coin
    {
        public bool Side { get; }

        public Coin()
        {
            var random = new Random((int)(DateTime.Now.Ticks));
            var num = random.Next(0, 2);
            Side = (num == 0) ? false : true;
        }
    }
}
