using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HomeWork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ЗАДАНИЕ 1 
            int[] fib = { 0, 1, 1, 2, 3, 5, 8, 13 };

            // ЗАДАНИЕ 2 
            string[] months = {"January", "February", "March", "April", "May", "June",
                             "July", "August", "September", "October", "November", "December"};

            // ЗАДАНИЕ 3 
            int[,] matrix = {
                {2, 3, 4},
                {4, 9, 16},
                {8, 27, 64}
            };

            // ЗАДАНИЕ 4 

            double[][] jaggedArray = new double[3][];
            jaggedArray[0] = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0 };
            jaggedArray[1] = new double[] { Math.E, Math.PI };
            jaggedArray[2] = new double[]
                        {
                Math.Log10(1),
                Math.Log10(10),
                Math.Log10(100),
                Math.Log10(1000)
                        };


            // ЗАДАНИЕ 5 и 6 

            int[] array = { 1, 2, 3, 4, 5 };
            int[] array2 = { 7, 8, 9, 10, 11, 12, 13 };

            Array.Copy(array, 0, array2, 2, 3);
            Console.WriteLine(array2[0] + " " + array2[1] + " " + array2[2] + " " +
              array2[3] + " " + array2[4] + " " + array2[5] + " " + array2[6]);
          
            Array.Resize(ref array, array.Length * 2);
            Console.WriteLine(array.Length);


        }

    }
}
    
        