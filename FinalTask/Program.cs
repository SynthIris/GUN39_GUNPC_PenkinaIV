using FinalTask.Games;
using FinalTask.Models;
using FinalTask.Services;

namespace FinalTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();

            try
            {
                var saveLoadService = new FileSystemSaveLoadService<PlayerProfile>("saves");
                var casino = new Casino(saveLoadService);
                casino.StartGame();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Критическая ошибка: {ex.Message}");
                Console.ReadKey();
            }
        }
    }
}
