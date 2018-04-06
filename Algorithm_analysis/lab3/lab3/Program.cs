using System;
using System.IO;
using System.Diagnostics;

namespace lab3
{
    class Program
    {
        static private Random rnd = new Random();

        static private void quicksort(ref int[] arr, int begin, int end)
        {
            int p;
            if (begin < end)
            {
                p = partition(ref arr,begin,end);
                quicksort(ref arr, begin, p - 1);
                quicksort(ref arr, p+1, end);
            }
        }

        static private int partition(ref int[] arr, int begin, int end)
        {
            int change;
            int pivot = arr[end];
            int i = begin;

            for (int j = begin; j< end;j++)
            {
                if (arr[j] <= pivot)
                {
                    change = arr[i];
                    arr[i] = arr[j];
                    arr[j] = change;
                    i++;
                }
            
            }
            change = arr[i];
            arr[i] = arr[end];
            arr[end] = change;
            return i;

            /*int j = end;
            int change;
            while (i <= j)
            {
                while (arr[i] < pivot)
                    i++;
                while (arr[j] > pivot)
                    j--;
                if (i <= j)
                {
                    change = arr[j];
                    arr[j] = arr[i];
                    arr[i] = change;
                    i++;
                    j--;
                }
            }
            return i;*/
        }


        /// <summary>
        /// Лучший для вставок - отсортированный
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        static private int[] better(int len)
        {
            int[] arr = new int[len];
            for (int i = 0; i < len; i++)
                arr[i] = i;
            return arr;
        }

        static private int[] worst(int len)
        {
            int[] arr = new int[len];
            for (int i = 0; i < len; i++)
                arr[i] = len - i;
            return arr;
        }

        private static void sort_insert(ref int[] arr, int len)
        {
            int key;
            int i;
            for (int j = 1; j < len; j++)
            {
                key = arr[j];
                i = j - 1;
                while ((i >= 0)&&(arr[i]> key))
                {
                    arr[i + 1] = arr[i];
                    i--;
                }
                arr[i + 1] = key;
            }
        }

        public static int[] random(int size)
        {
            int[] arr = new int[size];
            for (int i = 0; i < size; i++)
                arr[i] = rnd.Next(0, size);
            return arr;
        }

        static private void boble(ref int [] arr, int len)
        {
            int F;
            int change;
            for (int j = 0; j<len; j++)
            {
                F = 0;
                for (int i = 0; i < len-j-1;i++)
                {
                    if (arr[i]>arr[i+1])
                    {
                        change = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = change;
                        F = 1;
                    }
                }
                if (F == 0)
                    break;
            }
        }

        static void Main(string[] args)
        {
            
            Stopwatch t1 = new Stopwatch();
            Stopwatch t2 = new Stopwatch();
            Stopwatch t3 = new Stopwatch();

            int[] arr = random(2);
            boble(ref arr, 2);
            sort_insert(ref arr, 2);
            quicksort(ref arr, 0, 1);


            double time1;
            double time2;
            double time3;

            //Лучший случай
            Console.WriteLine("#####BETTER#####");
            StreamWriter Better = new StreamWriter(Directory.GetCurrentDirectory() + "\\better.txt");      
            for (int i = 100; i<= 1000; i+=100)
            {
                time1 = 0.0;
                time2 = 0.0;
                for (int j =0; j<10; j++)
                {
                    arr = better(i);
                    int[] tmp = arr;
                    t1.Reset();
                    t1.Start();
                    boble(ref tmp, i);
                    t1.Stop();

                    t2.Reset();
                    t2.Start();
                    sort_insert(ref tmp, i);
                    t2.Stop();
                    time1 += t1.ElapsedTicks;
                    time2 += t2.ElapsedTicks;

                }
                Better.WriteLine("{0} {1}", time1/10.0, time2/ 10.0);
                Console.WriteLine("{0} {1}", time1 / 10.0, time2 / 10.0);
            }
            Better.Close();
            Console.WriteLine("#####RANDOM#####");
            StreamWriter Random = new StreamWriter(Directory.GetCurrentDirectory() + "\\random.txt");
            for (int i = 100; i <= 1000; i += 100)
            {
                time1 = 0.0;
                time2 = 0.0;
                time3 = 0.0;             
                
                for (int j = 0; j < 10; j++)
                {
                    arr = random(i);
                    int[] tmp = new int[i];
                    Array.Copy(arr, tmp, i);
                    t1.Reset();
                    t1.Start();
                    boble(ref tmp, i);
                    t1.Stop();
                    time1 += t1.ElapsedTicks;

                    Array.Copy(arr, tmp, i);
                    t2.Reset();
                    t2.Start();
                    sort_insert(ref tmp, i);
                    t2.Stop();
                    time2 += t2.ElapsedTicks;

                    Array.Copy(arr, tmp, i);
                    t3.Reset();
                    t3.Start();
                    quicksort(ref tmp, 0, i - 1);
                    t3.Stop();
                    time3 += t3.ElapsedTicks;

                }
                Random.WriteLine("{0} {1} {2}", time1 / 10.0, time2 / 10.0, time3 / 10.0);
                Console.WriteLine("{0} {1} {2}", time1 / 10.0, time2 / 10.0, time3 / 10.0);
            }
            Random.Close();
            Console.WriteLine("#####WORST#####");
            StreamWriter Worst = new StreamWriter(Directory.GetCurrentDirectory() + "\\worst.txt");
            for (int i = 100; i <= 1000; i += 100)
            {
                time1 = 0.0;
                time2 = 0.0;
                time3 = 0.0;
                for (int j = 0; j < 10; j++)
                {
                    arr = random(i);
                    int[] tmp = new int[i];
                    Array.Copy(arr, tmp, i);
                    t1.Reset();
                    t1.Start();
                    boble(ref tmp, i);
                    t1.Stop();
                    time1 += t1.ElapsedTicks;

                    Array.Copy(arr, tmp, i);
                    t2.Reset();
                    t2.Start();
                    sort_insert(ref tmp, i);
                    t2.Stop();
                    time2 += t2.ElapsedTicks;

                    Array.Copy(arr, tmp, i);
                    t3.Reset();
                    t3.Start();
                    quicksort(ref tmp, 0, i - 1);
                    t3.Stop();
                    time3 += t3.ElapsedTicks;
                }
                Worst.WriteLine("{0} {1} {2}", time1 / 10.0, time2 / 10.0, time3 / 10.0);
                Console.WriteLine("{0} {1} {2}", time1 / 10.0, time2 / 10.0, time3 / 10.0);
            }
            Worst.Close();

            /*int[] arr = worst_for_buble(5);
            for (int i = 0; i < 5; i++)
                Console.Write("{0} ",arr[i]);
            Console.Write("\n");

            quicksort(ref arr, 0, 4);
            for (int i = 0; i < 5; i++)
                Console.Write("{0} ", arr[i]);
            Console.Write("\n");

            sort_insert(ref arr, 5);
            for (int i = 0; i < 5; i++)
                Console.Write("{0} ", arr[i]);
            Console.Write("\n");

            boble(ref arr, 5);
            for (int i = 0; i < 5; i++)
                Console.Write("{0} ", arr[i]);
            Console.Write("\n");*/
            Console.ReadLine();
        }
    }
}
