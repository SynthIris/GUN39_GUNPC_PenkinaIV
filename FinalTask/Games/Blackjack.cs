using FinalTask.Enums;
using FinalTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalTask.Games
{
    public class Blackjack : CasinoGameBase
    {
        private readonly int _numberOfCards;
        private Queue<Card> _deck;
        private readonly List<Card> _playerHand;
        private readonly List<Card> _dealerHand;
        private readonly Random _random;

        public Blackjack(int numberOfCards)
        {
            if (numberOfCards < 10)
            {
                throw new ArgumentException("Количество карт должно быть не менее 10", nameof(numberOfCards));
            }

            _numberOfCards = numberOfCards;
            _playerHand = new List<Card>();
            _dealerHand = new List<Card>();
            _random = new Random();

            FactoryMethod();
        }

        protected override void FactoryMethod()
        {
            CreateCards();
            Shuffle();
        }

        private void CreateCards()
        {
            var cards = new List<Card>();
            var suits = new[] { Suit.Diamonds, Suit.Hearts, Suit.Clubs, Suit.Spades };
            var ranks = new[] {
                Rank.Six, Rank.Seven, Rank.Eight, Rank.Nine, Rank.Ten,
                Rank.Jack, Rank.Queen, Rank.King, Rank.Ace
            };

            int cardsCreated = 0;
            while (cardsCreated < _numberOfCards)
            {
                foreach (var suit in suits)
                {
                    foreach (var rank in ranks)
                    {
                        if (cardsCreated >= _numberOfCards) break;
                        cards.Add(new Card(suit, rank));
                        cardsCreated++;
                    }
                    if (cardsCreated >= _numberOfCards) break;
                }
            }

            _deck = new Queue<Card>(cards);
        }

        private void Shuffle()
        {
            var cardsList = _deck.ToList();

            for (int i = cardsList.Count - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                var temp = cardsList[i];
                cardsList[i] = cardsList[j];
                cardsList[j] = temp;
            }

            _deck = new Queue<Card>(cardsList);
        }

        private Card DrawCard()
        {
            if (_deck == null || _deck.Count == 0)
            {
                FactoryMethod();
            }
            return _deck.Dequeue();
        }

        public override void PlayGame()
        {
            Console.WriteLine("=== ИГРА В БЛЭКДЖЕК ===");

            try
            {
                _playerHand.Clear();
                _dealerHand.Clear();

                DealInitialCards();
                PlayerTurn();

                if (CalculateScore(_playerHand) <= 21)
                {
                    DealerTurn();
                }

                DetermineWinner();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
                OnLoseInvoke();
            }
        }

        private void DealInitialCards()
        {
            _playerHand.Add(DrawCard());
            _playerHand.Add(DrawCard());
            _dealerHand.Add(DrawCard());
            _dealerHand.Add(DrawCard());

            Console.WriteLine($"Ваши карты: {GetHandString(_playerHand)} (очки: {CalculateScore(_playerHand)})");
            Console.WriteLine($"Карта дилера: {_dealerHand[0]} и [скрытая]");
        }

        private void PlayerTurn()
        {
            while (CalculateScore(_playerHand) < 21)
            {
                Console.Write("Хотите взять еще карту? (д/н): ");
                string input = Console.ReadLine()?.ToLower();

                if (input == "д" || input == "y" || input == "да")
                {
                    _playerHand.Add(DrawCard());
                    Console.WriteLine($"Ваши карты: {GetHandString(_playerHand)} (очки: {CalculateScore(_playerHand)})");

                    if (CalculateScore(_playerHand) > 21)
                    {
                        Console.WriteLine("Перебор! У вас больше 21 очка.");
                        break;
                    }
                }
                else if (input == "н" || input == "n" || input == "нет")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Пожалуйста, введите 'д' или 'н'");
                }
            }
        }

        private void DealerTurn()
        {
            Console.WriteLine($"Карты дилера: {GetHandString(_dealerHand)} (очки: {CalculateScore(_dealerHand)})");

            while (CalculateScore(_dealerHand) < 17)
            {
                _dealerHand.Add(DrawCard());
                Console.WriteLine($"Дилер взял карту. Теперь у него: {GetHandString(_dealerHand)} (очки: {CalculateScore(_dealerHand)})");
            }
        }

        private void DetermineWinner()
        {
            int playerScore = CalculateScore(_playerHand);
            int dealerScore = CalculateScore(_dealerHand);

            Console.WriteLine($"\n=== РЕЗУЛЬТАТ ===");
            Console.WriteLine($"Ваши очки: {playerScore}");
            Console.WriteLine($"Очки дилера: {dealerScore}");

            if (playerScore == dealerScore && playerScore < 21)
            {
                Console.WriteLine("Ничья! Добор по одной карте...");
                _playerHand.Add(DrawCard());
                _dealerHand.Add(DrawCard());

                playerScore = CalculateScore(_playerHand);
                dealerScore = CalculateScore(_dealerHand);

                Console.WriteLine($"После добора - Ваши очки: {playerScore}, Очки дилера: {dealerScore}");
            }

            if (playerScore > 21 && dealerScore > 21)
            {
                Console.WriteLine("Оба игрока превысили 21 - ничья!");
                OnDrawInvoke();
            }
            else if (playerScore > 21)
            {
                Console.WriteLine("У вас перебор - вы проиграли!");
                OnLoseInvoke();
            }
            else if (dealerScore > 21)
            {
                Console.WriteLine("У дилера перебор - вы выиграли!");
                OnWinInvoke();
            }
            else if (playerScore == dealerScore)
            {
                Console.WriteLine("Ничья!");
                OnDrawInvoke();
            }
            else if (playerScore > dealerScore)
            {
                Console.WriteLine("Вы выиграли!");
                OnWinInvoke();
            }
            else
            {
                Console.WriteLine("Дилер выиграл!");
                OnLoseInvoke();
            }
        }

        private int CalculateScore(List<Card> hand)
        {
            int score = 0;
            int aceCount = 0;

            foreach (var card in hand)
            {
                if (card.Rank == Rank.Ace)
                {
                    aceCount++;
                    score += 11;
                }
                else if (card.Rank >= Rank.Ten)
                {
                    score += 10;
                }
                else
                {
                    score += (int)card.Rank;
                }
            }

            while (score > 21 && aceCount > 0)
            {
                score -= 10;
                aceCount--;
            }

            return score;
        }

        private string GetHandString(List<Card> hand)
        {
            return string.Join(", ", hand.Select(card => card.ToString()));
        }
    }
}