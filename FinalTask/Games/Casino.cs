using System;
using System.Collections.Generic;
using System.Linq;
using FinalTask.Interfaces;
using FinalTask.Services;
using FinalTask.Models;
using FinalTask.Games;

namespace FinalTask.Games
{
    public class Casino : IGame
    {
        private readonly ISaveLoadService<PlayerProfile> _saveLoadService;
        private PlayerProfile _currentPlayer;
        private const int MaxBankValue = 10000;
        private const int InitialBalance = 1000;

        private readonly Dictionary<string, CasinoGameBase> _games;

        public Casino(ISaveLoadService<PlayerProfile> saveLoadService)
        {
            _saveLoadService = saveLoadService ?? throw new ArgumentNullException(nameof(saveLoadService));
            _games = new Dictionary<string, CasinoGameBase>();
            InitializeGames();
        }

        private void InitializeGames()
        {
            _games["1"] = new Blackjack(36);
            _games["2"] = new DiceGame(2, 1, 6);
        }

        public void StartGame()
        {
            Console.WriteLine("=== ДОБРО ПОЖАЛОВАТЬ В КАЗИНО! ===");
            Console.WriteLine("===================================");

            try
            {
                ShowMainMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Критическая ошибка: " + ex.Message);
                Console.WriteLine("Нажмите любую клавишу для выхода...");
                Console.ReadKey();
            }
        }

        private void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("\n=== ГЛАВНОЕ МЕНЮ ===");
                Console.WriteLine("1 - Выбрать профиль");
                Console.WriteLine("2 - Создать новый профиль");
                Console.WriteLine("3 - Список профилей");
                Console.WriteLine("4 - Удалить профиль");
                Console.WriteLine("0 - Выход");
                Console.Write("Ваш выбор: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        SelectProfile();
                        break;
                    case "2":
                        CreateNewProfile();
                        break;
                    case "3":
                        ShowProfilesList();
                        break;
                    case "4":
                        DeleteProfile();
                        break;
                    case "0":
                        Console.WriteLine("До свидания!");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
        }

        private void SelectProfile()
        {
            var profiles = GetAllProfiles();
            if (profiles.Count == 0)
            {
                Console.WriteLine("Нет доступных профилей. Создайте новый профиль.");
                return;
            }

            Console.WriteLine("\n=== ВЫБОР ПРОФИЛЯ ===");
            for (int i = 0; i < profiles.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {profiles[i]}");
            }
            Console.WriteLine("0 - Назад");

            Console.Write("Выберите профиль: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= profiles.Count)
            {
                _currentPlayer = profiles[choice - 1];

                if (_currentPlayer.Balance <= 0)
                {
                    Console.WriteLine("Баланс профиля равен 0. Сброс до начального значения.");
                    _currentPlayer.ResetProfile();
                    SaveCurrentProfile();
                }

                Console.WriteLine($"Выбран профиль: {_currentPlayer.Name}");
                StartGameSession();
            }
            else if (choice != 0)
            {
                Console.WriteLine("Неверный выбор профиля.");
            }
        }

        private void CreateNewProfile()
        {
            Console.Write("Введите имя нового профиля: ");
            string name = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Имя не может быть пустым.");
                return;
            }

            var existingProfiles = GetAllProfiles();
            if (existingProfiles.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Профиль с таким именем уже существует.");
                return;
            }

            _currentPlayer = new PlayerProfile { Name = name, Balance = InitialBalance };
            SaveCurrentProfile();
            Console.WriteLine($"Создан новый профиль: {name}");
            StartGameSession();
        }

        private void ShowProfilesList()
        {
            var profiles = GetAllProfiles();
            if (profiles.Count == 0)
            {
                Console.WriteLine("Нет созданных профилей.");
                return;
            }

            Console.WriteLine("\n=== СПИСОК ПРОФИЛЕЙ ===");
            foreach (var profile in profiles)
            {
                Console.WriteLine(profile);
            }
        }

        private void DeleteProfile()
        {
            var profiles = GetAllProfiles();
            if (profiles.Count == 0)
            {
                Console.WriteLine("Нет профилей для удаления.");
                return;
            }

            Console.WriteLine("\n=== УДАЛЕНИЕ ПРОФИЛЯ ===");
            for (int i = 0; i < profiles.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {profiles[i]}");
            }
            Console.WriteLine("0 - Назад");

            Console.Write("Выберите профиль для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= profiles.Count)
            {
                var profileToDelete = profiles[choice - 1];

                if (_currentPlayer != null && _currentPlayer.Name == profileToDelete.Name)
                {
                    _currentPlayer = null;
                    Console.WriteLine("Текущий профиль сброшен.");
                }

                try
                {
                    _saveLoadService.DeleteData(profileToDelete.Name);
                    Console.WriteLine($"Профиль {profileToDelete.Name} удален.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при удалении профиля: {ex.Message}");
                }
            }
        }

        private List<PlayerProfile> GetAllProfiles()
        {
            var profiles = new List<PlayerProfile>();

            try
            {
                var fileSystemService = _saveLoadService as FileSystemSaveLoadService<PlayerProfile>;
                if (fileSystemService != null)
                {
                    var profileNames = fileSystemService.GetAllProfileNames();
                    foreach (string name in profileNames)
                    {
                        var profile = _saveLoadService.LoadData(name);
                        if (profile != null)
                        {
                            profiles.Add(profile);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке списка профилей: {ex.Message}");
            }

            return profiles;
        }

        private void StartGameSession()
        {
            if (_currentPlayer == null) return;

            while (_currentPlayer.Balance > 0)
            {
                Console.WriteLine($"\nТекущий игрок: {_currentPlayer.Name}");
                Console.WriteLine($"Текущий баланс: {_currentPlayer.Balance}");
                Console.WriteLine("Выберите игру:");
                Console.WriteLine("1 - Блэкджек");
                Console.WriteLine("2 - Кости");
                Console.WriteLine("0 - Выход в главное меню");
                Console.Write("Ваш выбор: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PlayBlackjack();
                        break;
                    case "2":
                        PlayDiceGame();
                        break;
                    case "0":
                        SaveCurrentProfile();
                        return;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }

                if (_currentPlayer.Balance > MaxBankValue)
                {
                    Console.WriteLine("Вы потратили половину выигрыша в баре");
                    _currentPlayer.Balance /= 2;
                    SaveCurrentProfile();
                }
            }

            Console.WriteLine("Нет денег? На выход!");
            SaveCurrentProfile();
        }

        private void SaveCurrentProfile()
        {
            if (_currentPlayer != null)
            {
                try
                {
                    _saveLoadService.SaveData(_currentPlayer, _currentPlayer.Name);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при сохранении профиля: {ex.Message}");
                }
            }
        }

        private void PlayBlackjack()
        {
            Console.Write("Введите ставку: ");
            if (int.TryParse(Console.ReadLine(), out int bet) && _currentPlayer.CanPlaceBet(bet))
            {
                var blackjack = new Blackjack(36);

                blackjack.OnWin += () => {
                    _currentPlayer.ProcessWin(bet);
                    Console.WriteLine($"Вы выиграли {bet}!");
                };

                blackjack.OnLose += () => {
                    _currentPlayer.ProcessLoss(bet);
                    Console.WriteLine($"Вы проиграли {bet}!");
                };

                blackjack.OnDraw += () => {
                    Console.WriteLine("Ничья! Ставка возвращена.");
                };

                blackjack.PlayGame();
                SaveCurrentProfile();
            }
            else
            {
                Console.WriteLine("Неверная ставка!");
            }
        }

        private void PlayDiceGame()
        {
            Console.Write("Введите ставку: ");
            if (int.TryParse(Console.ReadLine(), out int bet) && _currentPlayer.CanPlaceBet(bet))
            {
                var diceGame = new DiceGame(2, 1, 6);

                diceGame.OnWin += () => {
                    _currentPlayer.ProcessWin(bet);
                    Console.WriteLine($"Вы выиграли {bet}!");
                };

                diceGame.OnLose += () => {
                    _currentPlayer.ProcessLoss(bet);
                    Console.WriteLine($"Вы проиграли {bet}!");
                };

                diceGame.OnDraw += () => {
                    Console.WriteLine("Ничья! Ставка возвращена.");
                };

                diceGame.PlayGame();
                SaveCurrentProfile();
            }
            else
            {
                Console.WriteLine("Неверная ставка!");
            }
        }
    }
}