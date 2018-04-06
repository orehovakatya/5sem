using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Levenshteinch
{
    class Test
    {
        private static String[,] words = new String[,] {
        {"Word", "word" },
        {"word", "world"},
        {"word", "smwordly"},
        {"mother", "other"},
        {"word", ""},
        {"","word"},
        {"word", "word"},
        {"word", "wrod"},
        {"word", "drow"}};
        private static int N = 9;
        public static void Test_work()
        {
            int a, b, c;
            Console.Write("Levenshtein distance word1-word2 rec:matr:modify\n");
            for (int i = 0; i<N;i++)
            {
                    a = functions.distance_rec(words[i,0], words[i,1], words[i,0].Length - 1, words[i,1].Length - 1);
                    b = functions.distance_matr(words[i, 0], words[i, 1], words[i, 0].Length, words[i,1].Length);
                    c = functions.distance_modify(words[i, 0], words[i, 1], words[i, 0].Length, words[i, 1].Length);
                    Console.Write("Levenshtein distance "+words[i,0].ToString()+" - "+words[i,1].ToString()+" "+
                        a.ToString()+":"+b.ToString()+":"+c.ToString()+"\n");
            }
        }
        public static void Test_time()
        {
            Stopwatch t1 = new Stopwatch();
            Stopwatch t2 = new Stopwatch();
            Stopwatch t3 = new Stopwatch();
            Console.Write("rec\t matr\t mod\n");
            int d;
            for (int i = 0; i< N; i++)
            {
                t1.Reset();
                t2.Reset();
                t3.Reset();
                for (int j = 0; j < 100; j++)
                {
                    t1.Start();
                    d = functions.distance_rec(words[i,0], words[i,1], words[i,0].Length - 1, words[i,1].Length - 1);
                    t1.Stop();

                    t2.Start();
                    d = functions.distance_matr(words[i, 0], words[i, 1], words[i,0].Length, words[i,1].Length);
                    t2.Stop();

                    t3.Start();
                    d = functions.distance_modify(words[i, 0], words[i, 1], words[i, 0].Length, words[i, 1].Length);
                    t3.Stop();
                }
                Console.Write((t1.ElapsedTicks/100).ToString()+"\t "+
                    (t2.ElapsedTicks/100).ToString()+"\t "+
                    (t3.ElapsedTicks/100).ToString()+"\n");
            }
        }
    }
}
