using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQ01
{
    class Program
    {
        static void Main(string[] args)
        {
            // Пример #1.
            List<int> numbers = new List<int>() { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            IEnumerable<int> Query1 =
                from num in numbers
                where num < 3 || num > 7
                select num;
            foreach (int n in Query1)
            {
                Console.WriteLine(n);
            }

            // Пример #2.
            string[] fruits = { "apple", "strawberry", "grape", "peach", "banana" };
            IEnumerable<string> Query2 =
                from fruit in fruits
                where fruit[0] == 'g'
                select fruit;
            foreach (string s in Query2)
            {
                Console.WriteLine(s);
            }

            // Пример #3.
            string[] words = { "apple", "strawberry", "grape", "peach", "banana" };
            var Query3 =
                from word in words
                where word[0] == 'g'
                select word;
            foreach (string s in Query3)
            {
                Console.WriteLine(s);
            }

            // Пример #4.
            object[] data = { "one", 2, 3, "four", "five", 6 };
            var Query4 = data.OfType<string>();
            foreach (var s in Query4)
            {
                Console.WriteLine(s);
            }
            Console.ReadLine();
        }
    }
}
