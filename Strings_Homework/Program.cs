using System.Text;

namespace Strings_Homework
{
    internal class Program
    {
        // Задание 1 - Конкатенация строк
        public static string ConcatenateStrings(string firstString, string secondString)
        {
            string concatenatedString = firstString + secondString;
            return concatenatedString;
        }

        // Задание 2 - Приветствие пользователя
        public static string GreetUser(string userName, int userAge)
        {
            string greetingMessage = $"Hello, {userName}!\nYou are {userAge} years old.";
            return greetingMessage;
        }

        // Задание 3 - Информация о строке
        public static string GetStringInfo(string inputString)
        {
            int characterCount = inputString.Length;
            string uppercaseString = inputString.ToUpper();
            string lowercaseString = inputString.ToLower();

            string stringInformation = "Количество символов: " + characterCount +
                                     "\nВерхний регистр: " + uppercaseString +
                                     "\nНижний регистр: " + lowercaseString;
            return stringInformation;
        }

        // Задание 4 - Первые 5 символов
        public static string GetFirstFiveCharacters(string inputText)
        {
            if (inputText.Length >= 5)
            {
                string firstFive = inputText.Substring(0, 5);
                return firstFive;
            }
            else
            {
                return inputText;
            }
        }

        // Задание 5 - StringBuilder для предложения
        public static StringBuilder BuildSentenceFromArray(string[] wordArray)
        {
            StringBuilder sentenceBuilder = new StringBuilder();

            for (int index = 0; index < wordArray.Length; index++)
            {
                sentenceBuilder.Append(wordArray[index]);
                sentenceBuilder.Append(" ");
            }

            return sentenceBuilder;
        }

        // Задание 6 - Замена слов в строке
        public static string ReplaceWordsInString(string originalString, string wordToFind, string wordToReplace)
        {
            string modifiedString = originalString.Replace(wordToFind, wordToReplace);
            return modifiedString;
        }
        static void Main(string[] args)
        {
            // Проверка задания 1
            Console.WriteLine("1. Конкатенация строк:");
            string concatenationResult = ConcatenateStrings("Привет, ", "мир!");
            Console.WriteLine(concatenationResult);

            // Проверка задания 2  
            Console.WriteLine("\n2. Приветствие пользователя:");
            string greetingResult = GreetUser("Анна", 25);
            Console.WriteLine(greetingResult);

            // Проверка задания 3
            Console.WriteLine("\n3. Информация о строке:");
            string stringInfoResult = GetStringInfo("Hello World");
            Console.WriteLine(stringInfoResult);

            // Проверка задания 4
            Console.WriteLine("\n4. Первые 5 символов:");
            string longTextResult = GetFirstFiveCharacters("Программирование");
            Console.WriteLine("Длинная строка: " + longTextResult);

            string shortTextResult = GetFirstFiveCharacters("C#");
            Console.WriteLine("Короткая строка: " + shortTextResult);

            // Проверка задания 5
            Console.WriteLine("\n5. Объединение массива в предложение:");
            string[] wordList = { "Я", "изучаю", "C#", "в", "Нетологии" };
            StringBuilder sentenceResult = BuildSentenceFromArray(wordList);
            Console.WriteLine(sentenceResult);

            // Проверка задания 6
            Console.WriteLine("\n6. Замена слов в строке:");
            string originalText = "Кот сидит на ковре. Кот любит молоко.";
            string replacedText = ReplaceWordsInString(originalText, "Кот", "Пёс");
            Console.WriteLine("Было: " + originalText);
            Console.WriteLine("Стало: " + replacedText);

        }
    }
}
