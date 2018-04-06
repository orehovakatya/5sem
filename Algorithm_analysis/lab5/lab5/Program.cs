using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace lab5
{
    class Program
    {
        static Queue<int> queue1;
        static Queue<int> queue2;
        static int count = 0, len;
        static int[] input;
        static int[] output;
        static object locker = new object();
        static bool work = true;

        //*2
        static void conv1()
        {
            int temp = 0;
            for (int i = 0; i < len; i++)
            {
                temp = input[i] *2;
                
                lock (locker)
                {
                    queue1.Enqueue(temp);
                }
                Console.WriteLine("1. Взял: " + input[i] + ", положил: " + temp);
            }
        }

        //-10
        static void conv2()
        {
            int temp;
            while (work)
            {
                if (queue1.Count != 0)
                {
                    lock (locker)
                    {
                        temp = queue1.Dequeue();
                        queue2.Enqueue(temp - 10);
                    }
                    Console.WriteLine("\t2. Взял: " + temp + ", положил: " + (temp -10));
                }
            }
        }

    //^2
    static void conv3()
    {
        int temp2;
        while (work)
        {
            if (queue2.Count != 0)
            {
                lock (locker)
                {
                    temp2 = queue2.Dequeue();
                }
                //for (int j = 0; j < pr; j++)
                //    temp2 += temp2 * temp2 - 4;
                output[count] = temp2*temp2;
                Console.WriteLine("\t\t3. Взял: " + temp2 + ", положил: " + (output[count]));
                count++;
                if (count == len)
                    work = false;
            }
        }
    }

        static void f1(ref int[] mas)
        {
            for (int i = 0; i < mas.Length; i++)
                mas[i] = mas[i] * 2;
        }

        static void f2(ref int[] mas)
        {
            for (int i = 0; i < mas.Length; i++)
                mas[i] = mas[i] - 10;
        }

        static void f3(ref int[] mas)
        {
            for (int i = 0; i < mas.Length; i++)
                mas[i] = mas[i] *mas[i];
        }

        static void Main(string[] args)
        {
            
            Stopwatch t = new Stopwatch();

            double time1 = 0.0, time2 = 0.0;
            //Console.Write("Входная строка: ");
            for (int i= 10;i<= 10;i += 10)
            {
                queue1 = new Queue<int>();
                queue2 = new Queue<int>();
                count = 0;
                len = i;
                input = new int[len];
                output = new int[len];
                locker = new object();
                work = true;

                for (int j = 0; j < len; j++)
                {
                    input[j] = j;
                    //Console.Write("{0} ",mas[i]);
                }
                //Console.Write("\n");

                Thread th1 = new Thread(conv1);
                Thread th2 = new Thread(conv2);
                Thread th3 = new Thread(conv3);

                t.Reset();
                t.Start();
                th1.Start();
                th2.Start();
                th3.Start();

                th1.Join();
                th2.Join();
                th3.Join();
                t.Stop();
                time1 = t.ElapsedTicks;


                for (int j = 0; j < len; j++)
                {
                    input[j] = j;
                    //Console.Write("{0} ",mas[i]);
                }

                
                t.Start();
                f1(ref input);
                f2(ref input);
                f3(ref input);
                t.Stop();
                time2 = t.ElapsedTicks;

                

                /*Console.Write("Выходная строка: ");
                for (int i = 0; i<len;i++)
                {
                    Console.Write("{0} ",mas[i]);
                }*/
                Console.WriteLine("Длина: {0},Время конвеера: {1}, Время последовательной обработки: {2}",len, time1, time2);
            }

            Console.Read();
        }
    }
}
