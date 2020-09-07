using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNOProjectCO3.Games
{
    public class IDGenerator
    {
        static Random rand = new Random();

        public static long GenerateID()
        {
            return DateTime.UtcNow.ToBinary() + rand.Next(int.MaxValue) << rand.Next(8);
        }
    }
}
