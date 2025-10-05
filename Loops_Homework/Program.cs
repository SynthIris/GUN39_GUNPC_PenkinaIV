namespace HomeWork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Задание 1

            Console.WriteLine("Fibonacci Numbers");

            int[] fibo = new int[10];
            fibo[0] = 0;
            fibo[1] = 1;

            Console.WriteLine(fibo[0]);
            Console.WriteLine(fibo[1]);

            for (int i = 2; i < fibo.Length; i++)
            {
                fibo[i] = fibo[i - 1] + fibo[i - 2];
                Console.WriteLine(fibo[i]);
            }

            //Задание 2

            Console.WriteLine("Odd Numbers");

            for (int i = 2; i <= 20; i += 2)
            {
                Console.WriteLine(i);
            }

            //Задание 3

            Console.WriteLine("Multiplication Table");

            for (int i = 1; i <= 5; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    Console.WriteLine($"{i} * {j} = {i * j}");
                }
            }

            //Задание 4 

            string password = "qwerty";
            string userInput;

            do
            {
                Console.WriteLine("Enter Password: ");
                userInput = Console.ReadLine();

                if (userInput != password)
                {
                    Console.WriteLine("Access Denied");
                }

            } while (userInput != password);

            Console.WriteLine("Access Granted!");
        }
    }

}
