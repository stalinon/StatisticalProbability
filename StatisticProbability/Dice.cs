using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticProbability
{
    public class Dice
    {
        public int Side { get; }

        public Dice()
        {
            var random = new Random((int)(DateTime.Now.Ticks));
            Side = random.Next(1, 7);
        }
    }
}
