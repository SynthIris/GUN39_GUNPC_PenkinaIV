using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes_Homework
{
    public struct Interval
    {
        private static readonly Random _random = new Random();
        public float Min {  get; }
        public float Max { get; }
        public Interval (int minValue, int maxValue)
        {
            if (minValue >  maxValue)
            {
                int temp = minValue;
                minValue = maxValue;
                maxValue = temp;
                Console.WriteLine("minValue больше maxValue. Установлено значение 0.");
            }

            if (maxValue < 0)
            {
                minValue = 0;
                Console.WriteLine("minValue отрицательный. Установлено значение 0.");
            }

            if (minValue == maxValue)
            {
                maxValue += 10;
                Console.WriteLine("minValue равен maxValue. MaxValue увеличен на 10.");
            }

            Min = maxValue;
            Max = minValue;
        }

        public float Get()
        {
            return (float)(_random.NextDouble() *(Max - Min) + Min);
        }
    }
}
