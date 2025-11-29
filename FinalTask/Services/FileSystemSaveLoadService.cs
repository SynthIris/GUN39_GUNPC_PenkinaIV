using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace FinalTask.Services
{
    public class FileSystemSaveLoadService<T> : ISaveLoadService<T>
    {
        private readonly string _savePath;

        public FileSystemSaveLoadService(string path)
        {
            _savePath = path ?? throw new ArgumentNullException(nameof(path));

            if (!Directory.Exists(_savePath))
            {
                Directory.CreateDirectory(_savePath);
            }
        }

        public void SaveData(T data, string identifier)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (string.IsNullOrWhiteSpace(identifier))
                throw new ArgumentException("Идентификатор не может быть пустым", nameof(identifier));

            string filePath = Path.Combine(_savePath, $"{identifier}.txt");

            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                string json = JsonSerializer.Serialize(data, options);
                File.WriteAllText(filePath, json);

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Ошибка при сохранении данных: {ex.Message}", ex);
            }
        }

        public T LoadData(string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier))
                throw new ArgumentException("Идентификатор не может быть пустым", nameof(identifier));

            string filePath = Path.Combine(_savePath, $"{identifier}.txt");

            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Файл не найден: {filePath}");
                    return default(T);
                }

                string json = File.ReadAllText(filePath);

                if (string.IsNullOrWhiteSpace(json))
                {
                    Console.WriteLine("Файл пуст");
                    return default(T);
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                T data = JsonSerializer.Deserialize<T>(json, options);
                Console.WriteLine($"Профиль загружен: {filePath}");

                return data;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Ошибка чтения файла {filePath}: {ex.Message}");
                return default(T);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
                return default(T);
            }
        }

        public void DeleteData(string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier))
                throw new ArgumentException("Идентификатор не может быть пустым", nameof(identifier));

            string filePath = Path.Combine(_savePath, $"{identifier}.txt");

            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    Console.WriteLine($"Профиль удален: {filePath}");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Ошибка при удалении данных: {ex.Message}", ex);
            }
        }

        public List<string> GetAllProfileNames()
        {
            try
            {
                if (!Directory.Exists(_savePath))
                    return new List<string>();

                var files = Directory.GetFiles(_savePath, "*.txt");
                return files.Select(f => Path.GetFileNameWithoutExtension(f)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении списка профилей: {ex.Message}");
                return new List<string>();
            }
        }
    }
}