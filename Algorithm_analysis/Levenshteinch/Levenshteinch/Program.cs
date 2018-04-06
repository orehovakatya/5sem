using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levenshteinch
{
    class Program
    {
        static void Main(string[] args)
        {
            string word1 = Console.ReadLine();
            string word2 = Console.ReadLine();
            int len1, len2;
            len1 = word1.Length;
            len2 = word2.Length;

            functions.max = Math.Max(len1, len2) + 1;
            int d = functions.distance_rec(word1, word2, len1 - 1, len2 - 1);//вычитаем 1 для правильной индексации
            int d1 = functions.distance_matr(word1, word2, len1, len2);
            int dmod = functions.distance_modify(word1, word2, len1, len2);

            Console.Write("Levenshtein distance recursion " + d.ToString() + "\n");
            Console.Write("Levenshtein distance matrix " + d1.ToString() + "\n");
            Console.Write("Levenshtein distance matrix modify " + dmod.ToString() + "\n");
            //Test.Test_work();
            Console.Write("*****************************************\n");
            //Test.Test_time();
            Console.Read();
        }
    }
};
