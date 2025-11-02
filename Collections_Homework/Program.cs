using System;
using System.Collections.Generic;

namespace Collections_Homework
{
    internal class Program
    {
        private class ListTask
        {
            private readonly List<string> _myList = new List<string> { "Яблоко", "Груша", "Слива" };

            public void TaskLoop()
            {
                Console.WriteLine("Работа со списком");
                Console.WriteLine("Для выхода введите 'выход'");

                while (true)
                {
                    Console.WriteLine("\nТекущий список:");
                    for (int i = 0; i < _myList.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {_myList[i]}");
                    }

                    Console.Write("Введите новую строку для добавления в конец: ");
                    string input = Console.ReadLine();

                    if (input == "выход") break;
                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("Пустая строка не добавляется!");
                        continue;
                    }

                    _myList.Add(input);
                    Console.WriteLine("Добавлено в конец!");

                    Console.Write("Введите строку для вставки в середину: ");
                    input = Console.ReadLine();

                    if (input == "выход") break;
                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("Пустая строка не добавляется!");
                        continue;
                    }

                    int middle = _myList.Count / 2;
                    _myList.Insert(middle, input);
                    Console.WriteLine("Добавлено в середину!");
                }
            }
        }

        private class DictionaryTask
        {
            private readonly Dictionary<string, int> _students = new Dictionary<string, int>
            {
                ["Анна"] = 5,
                ["Максим"] = 4,
                ["София"] = 3
            };

            public void TaskLoop()
            {
                Console.WriteLine("Словарь");
                Console.WriteLine("Для выхода введите 'выход'");

                while (true)
                {
                    Console.WriteLine("\nЧто вы хотите сделать?");
                    Console.WriteLine("1 - Добавить студента");
                    Console.WriteLine("2 - Найти студента");
                    Console.Write("Ваш выбор: ");

                    string choice = Console.ReadLine();
                    if (choice == "выход") break;

                    if (choice == "1")
                    {
                        AddStudent();
                    }
                    else if (choice == "2")
                    {
                        FindStudent();
                    }
                    else
                    {
                        Console.WriteLine("Неизвестная команда!");
                    }
                }
            }

            private void AddStudent()
            {
                Console.Write("Введите имя студента: ");
                string name = Console.ReadLine();
                if (string.IsNullOrEmpty(name)) return;

                Console.Write("Введите оценку (от 2 до 5): ");
                string gradeInput = Console.ReadLine();

                if (int.TryParse(gradeInput, out int grade))
                {
                    if (grade >= 2 && grade <= 5)
                    {
                        _students[name] = grade;
                        Console.WriteLine($"Студент {name} добавлен с оценкой {grade}");
                    }
                    else
                    {
                        Console.WriteLine("Оценка должна быть от 2 до 5!");
                    }
                }
                else
                {
                    Console.WriteLine("Нужно ввести число!");
                }
            }

            private void FindStudent()
            {
                Console.Write("Введите имя студента для поиска: ");
                string name = Console.ReadLine();
                if (string.IsNullOrEmpty(name)) return;

                if (_students.TryGetValue(name, out int grade))
                {
                    Console.WriteLine($"Студент {name} имеет оценку: {grade}");
                }
                else
                {
                    Console.WriteLine($"Студент {name} не найден!");
                }
            }
        }

        private class LinkedListTask
        {
            private class Node
            {
                public string Data;
                public Node Next;
                public Node Previous;

                public Node(string data)
                {
                    Data = data;
                    Next = null;
                    Previous = null;
                }
            }

            private Node _first;
            private Node _last;

            public void TaskLoop()
            {
                Console.WriteLine("Двусвязный список");
                Console.WriteLine("Создайте список из 3-6 элементов");
                Console.WriteLine("Для выхода введите 'выход'");

                List<string> tempItems = new List<string>();

                for (int i = 0; i < 6; i++)
                {
                    Console.Write($"Введите элемент {i + 1}: ");
                    string input = Console.ReadLine();

                    if (input == "выход") break;

                    if (!string.IsNullOrEmpty(input))
                    {
                        tempItems.Add(input);
                    }

                    if (tempItems.Count >= 3 && i < 5)
                    {
                        Console.Write("Добавить еще элемент? (да/нет): ");
                        string answer = Console.ReadLine();
                        if (answer == "нет" || answer == "no") break;
                    }
                }
 
                foreach (string item in tempItems)
                {
                    AddToLinkedList(item);
                }

                Console.WriteLine("\nСписок в прямом порядке:");
                PrintForward();

                Console.WriteLine("\nСписок в обратном порядке:");
                PrintBackward();
            }

            private void AddToLinkedList(string data)
            {
                Node newNode = new Node(data);

                if (_first == null)
                {
                    _first = newNode;
                    _last = newNode;
                }
                else
                {
                    _last.Next = newNode;
                    newNode.Previous = _last;
                    _last = newNode;
                }
            }

            private void PrintForward()
            {
                Node current = _first;
                while (current != null)
                {
                    Console.WriteLine($"- {current.Data}");
                    current = current.Next;
                }
            }

            private void PrintBackward()
            {
                Node current = _last;
                while (current != null)
                {
                    Console.WriteLine($"- {current.Data}");
                    current = current.Previous;
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Домашнее задание по коллекциям");
            Console.WriteLine("==============================");

            while (true)
            {
                Console.WriteLine("\nВыберите задание:");
                Console.WriteLine("1 - Работа со списком (List)");
                Console.WriteLine("2 - Словарь студентов (Dictionary)");
                Console.WriteLine("3 - Двусвязный список (LinkedList)");
                Console.WriteLine("0 - Выход из программы");
                Console.Write("Ваш выбор: ");

                string input = Console.ReadLine();

                if (input == "0" || input == "выход" || input == "exit")
                {
                    Console.WriteLine("До свидания!");
                    break;
                }

                if (int.TryParse(input, out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            RunFirstTask();
                            break;
                        case 2:
                            RunSecondTask();
                            break;
                        case 3:
                            RunThirdTask();
                            break;
                        default:
                            Console.WriteLine("Неверный номер задания!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Нужно ввести число!");
                }
            }
        }

        private static void RunFirstTask()
        {
            ListTask task = new ListTask();
            task.TaskLoop();
        }

        private static void RunSecondTask()
        {
            DictionaryTask task = new DictionaryTask();
            task.TaskLoop();
        }

        private static void RunThirdTask()
        {
            LinkedListTask task = new LinkedListTask();
            task.TaskLoop();
        }
    }
}
