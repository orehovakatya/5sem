using System;
using System.Collections.Generic;
using System.Threading;

namespace lab8
{
    class Program
    {
        static Queue<double> queue1 = new Queue<double>();
        static int count = 0, len;
        static object locker = new object();
        static bool work = true;
        static Random rand = new Random();
        static double Sum = 0.0;
        static double midle;

        //Производитель чисел
        static void conv1()
        {
            double temp;
            for (int i = 0; i < len; i++)
            {
                temp = 10*rand.NextDouble();//[0-10)
                lock (locker)
                {
                    queue1.Enqueue(temp);
                    Console.WriteLine("1.Работает: положил: {0} из {1}", i+1, len);
                }
                
            }
        }

        //Считает среднее арифметическое
        static void conv2()
        {
            double temp;
            while (work)
            {
                if (queue1.Count != 0)
                {
                    lock (locker)
                    {
                        temp = queue1.Dequeue();
                        Sum += temp;
                        count++;
                        midle = Sum / count;
                    }
                    Console.WriteLine("2.Работает: вычислил {0} осталось {1}. Среднее арифметическое: {2}",count,queue1.Count,midle);
                    if (count == len)
                        work = false;
                }
            }
        }



        static void Main(string[] args)
        {
            Thread th1 = new Thread(conv1);
            Thread th2 = new Thread(conv2);
            len = 40;

            th1.Start();
            th2.Start();

            th1.Join();
            th2.Join();

            Console.ReadLine();
        }
    }
}
