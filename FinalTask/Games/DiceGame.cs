using System;
using System.Collections.Generic;
using FinalTask.Models;

namespace FinalTask.Games
{
    public class DiceGame : CasinoGameBase
    {
        private readonly int _diceCount;
        private readonly int _minValue;
        private readonly int _maxValue;
        private List<Dice> _dices;

        public DiceGame(int diceCount, int minValue, int maxValue)
        {
            if (diceCount <= 0)
                throw new ArgumentException("Количество костей должно быть больше 0", nameof(diceCount));

            if (minValue >= maxValue)
                throw new ArgumentException("Минимальное значение должно быть меньше максимального", nameof(minValue));

            _diceCount = diceCount;
            _minValue = minValue;
            _maxValue = maxValue;

            CreateDices();
        }

        protected override void FactoryMethod()
        {
            CreateDices();
        }

        private void CreateDices()
        {
            _dices = new List<Dice>();
            for (int i = 0; i < _diceCount; i++)
            {
                _dices.Add(new Dice(_minValue, _maxValue));
            }
        }

        public override void PlayGame()
        {
            Console.WriteLine("=== ИГРА В КОСТИ ===");
            Console.WriteLine($"Количество костей: {_diceCount}");
            Console.WriteLine($"Диапазон значений: {_minValue}-{_maxValue}");

            if (_dices == null || _dices.Count == 0)
            {
                Console.WriteLine("Ошибка: кости не созданы!");
                OnLoseInvoke();
                return;
            }

            int playerScore = ThrowDice("Игрок");

            int computerScore = ThrowDice("Компьютер");

            DetermineWinner(playerScore, computerScore);
        }

        private int ThrowDice(string playerName)
        {
            int total = 0;
            var results = new List<int>();

            Console.Write($"{playerName} бросает: ");

            foreach (var dice in _dices)
            {
                int roll = dice.Number;
                results.Add(roll);
                total += roll;
                Console.Write($"{roll} ");
            }

            Console.WriteLine($"= {total}");
            return total;
        }

        private void DetermineWinner(int playerScore, int computerScore)
        {
            Console.WriteLine("\n=== РЕЗУЛЬТАТ ===");
            Console.WriteLine($"Игрок: {playerScore} | Компьютер: {computerScore}");

            if (playerScore > computerScore)
            {
                Console.WriteLine("Вы выиграли!");
                OnWinInvoke();
            }
            else if (playerScore < computerScore)
            {
                Console.WriteLine("Компьютер выиграл!");
                OnLoseInvoke();
            }
            else
            {
                Console.WriteLine("Ничья!");
                OnDrawInvoke();
            }
        }
    }
}