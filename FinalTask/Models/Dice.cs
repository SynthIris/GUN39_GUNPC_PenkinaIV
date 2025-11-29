using System;
using FinalTask.Exceptions;

namespace FinalTask.Models
{
    public struct Dice
    {
        private readonly int _min;
        private readonly int _max;
        private static readonly Random _random = new Random();

        public int Number
        {
            get
            {
                return _random.Next(_min, _max + 1);
            }
        }

        public Dice(int min, int max)
        {
            if (min < 1)
            {
                throw new WrongDiceNumberException(min, 1, int.MaxValue);
            }

            if (max > int.MaxValue)
            {
                throw new WrongDiceNumberException(max, 1, int.MaxValue);
            }

            if (min >= max)
            {
                throw new WrongDiceNumberException(min, min + 1, max);
            }

            _min = min;
            _max = max;
        }
    }
}