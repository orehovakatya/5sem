using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Matrix_Multiplication
{
    class Program
    {
        static public Random rnd = new Random();
        static void Main(string[] args)
        {
            int[,] A;
            int[,] B;
            int[,] C = new int[2,2];
            int[] Rows = new int[2];
            int[] Column = new int[2];
            A = CreateMatrix.create_matrix(2);
            B = CreateMatrix.create_matrix(2);
            Multiplication.Simple_Multiplication(ref C,A, B, 2);
            Multiplication.Vinograd(ref C ,A, B, 2, Rows, Column);
            Multiplication.Vinograd_Better(ref C,A, B, 2,Rows,Column);
            double time1 = 0.0;
            double time2 = 0.0;
            double time3 = 0.0;
            Stopwatch t1 = new Stopwatch();
            Stopwatch t2 = new Stopwatch();
            Stopwatch t3 = new Stopwatch();
            StreamWriter File = new StreamWriter(Directory.GetCurrentDirectory() + "\\file.txt");
            for (int size = 100; size <= 1000; size += 100)
            {
                Console.Write("begin {0}", size);
                A = CreateMatrix.create_matrix(size);
                B = CreateMatrix.create_matrix(size);
                C = new int[size, size];
                Rows = new int[size];
                Column = new int[size];
                time1 = 0.0;
                time2 = 0.0;
                time3 = 0.0;

                Console.Write(" begin simple");
                for (int i = 0; i < 10; i++)
                {
                    t1.Reset();
                    t1.Start();
                    Multiplication.Simple_Multiplication(ref C, A, B, size);
                    t1.Stop();
                    time1 += t1.ElapsedMilliseconds;
                }

                Console.Write(" begin vinograd");
                for (int i = 0; i < 10; i++)
                {
                    t2.Reset();
                    t2.Start();
                    Multiplication.Vinograd(ref C, A, B,size, Rows, Column);
                    t2.Stop();
                    time2 += t2.ElapsedMilliseconds;
                }

                Console.Write(" begin better");
                t3.Reset();
                for (int i = 0; i < 10; i++)
                {
                    t3.Reset();
                    t3.Start();
                    Multiplication.Vinograd_Better(ref C, A, B, size, Rows, Column);
                    t3.Stop();
                    time3 += t3.ElapsedMilliseconds;
                }

                File.WriteLine("{0} {1} {2}",time1/10.0, time2/10.0, time3/10.0);
                Console.WriteLine("{0} {1} {2}", time1 / 10.0, time2 / 10.0, time3 / 10.0);
                //Console.WriteLine("end {0}",size);
            }
            File.Close();
            File = new StreamWriter(Directory.GetCurrentDirectory() + "\\file1.txt");
            for (int size = 101; size <= 1001; size += 100)
            {
                Console.Write("begin {0}", size);
                A = CreateMatrix.create_matrix(size);
                B = CreateMatrix.create_matrix(size);
                C = new int[size, size];
                Rows = new int[size];
                Column = new int[size];
                time1 = 0.0;
                time2 = 0.0;
                time3 = 0.0;

                Console.Write(" begin simple");
                for (int i = 0; i < 10; i++)
                {
                    t1.Reset();
                    t1.Start();
                    Multiplication.Simple_Multiplication(ref C, A, B, size);
                    t1.Stop();
                    time1 += t1.ElapsedMilliseconds;
                }

                Console.Write(" begin vinograd");
                for (int i = 0; i < 10; i++)
                {
                    t2.Reset();
                    t2.Start();
                    Multiplication.Vinograd(ref C, A, B, size, Rows, Column);
                    t2.Stop();
                    time2 += t2.ElapsedMilliseconds;
                }

                Console.Write(" begin better");
                t3.Reset();
                for (int i = 0; i < 10; i++)
                {
                    t3.Reset();
                    t3.Start();
                    Multiplication.Vinograd_Better(ref C, A, B, size, Rows, Column);
                    t3.Stop();
                    time3 += t3.ElapsedMilliseconds;
                }

                File.WriteLine("{0} {1} {2}", time1 / 10.0, time2 / 10.0, time3 / 10.0);
                Console.WriteLine("{0} {1} {2}", time1 / 10.0, time2 / 10.0, time3 / 10.0);
                //Console.WriteLine("end {0}",size);
            }
            File.Close();
            Console.ReadLine();
        }
    }
}
