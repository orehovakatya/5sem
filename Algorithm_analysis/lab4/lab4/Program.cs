using System;
using System.Diagnostics;
using System.Threading;

namespace lab4
{
    class Program
    {
        static public int[,] D; //результат перемножение поточных матриц
        static public int[,] A;
        static public int[,] B;
        static public int[,] C;

        static private Random rnd = new Random();
        public static int[,] create_matrix(int size)
        {
            int[,] matrix = new int[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    matrix[i, j] = rnd.Next(0, 10);
            return matrix;
        }

        static void Main(string[] args)
        {
            int N = 3;
            A = create_matrix(N);
            B = create_matrix(N);

            C = new int[N, N];
            Multiplication.Simple_Multiplication(ref C, N);

            D = new int[N, N];
            int thread_count = 2;
            Multiplication.bourder = new int[thread_count, 2];
            Multiplication.th = new Thread[thread_count];

            //Заполнение матрицы границ
            Multiplication.bourder[0, 0] = 0;
            Multiplication.bourder[0, 1] = 1;
            Multiplication.bourder[1, 0] = 1;
            Multiplication.bourder[1, 1] = 2;

            for (int k = 0; k < thread_count; k++)
            {
                Simple_Multiply tmp = new Simple_Multiply(2, 2, Multiplication.bourder[k, 0], Multiplication.bourder[k, 1]);
                Multiplication.th[k] = new Thread(tmp.Simple_Multiply_Thread);
            }


            Multiplication.Simple_Multiplication_Thread();

            Multiplication.Rows = new int[N];
            Multiplication.Column = new int[N];

            for (int k = 0; k < thread_count; k++)
            {
                Simple_Multiply tmp = new Simple_Multiply(2, 2, Multiplication.bourder[k, 0], Multiplication.bourder[k, 1]);
                Multiplication.th[k] = new Thread(tmp.Vinograd_Multiply_Thread);
            }

            Multiplication.Vinograd_Multiplication_Thread(N);

            Stopwatch t = new Stopwatch();
            double simple_t;
            double simple_t_thread;
            double vinograd_t;
            double vinograd_t_thread;

            while (true)
            {
                Console.WriteLine("Введите количество потоков или -1 для выхода");
                thread_count = Convert.ToInt32(Console.ReadLine());
                if (thread_count > 1)
                {
                    
                    for (int size = 100; size <= 1000; size += 100)
                    {
                        simple_t = 0.0;
                        simple_t_thread = 0.0;
                        vinograd_t = 0.0;
                        vinograd_t_thread = 0.0;
                        C = new int[size, size];
                        for (int i = 0; i < 10; i++)
                        {
                            A = create_matrix(size);
                            B = create_matrix(size);

                            t.Reset();
                            t.Start();
                            Multiplication.Simple_Multiplication(ref C, size);
                            t.Stop();
                            simple_t += t.ElapsedMilliseconds;

                            D = new int[size, size];

                            Multiplication.Rows = new int[size];
                            Multiplication.Column = new int[size];

                            t.Reset();
                            t.Start();
                            Multiplication.Vinograd(ref C, A, B, size, Multiplication.Rows, Multiplication.Column);
                            t.Stop();
                            vinograd_t += t.ElapsedMilliseconds;
                        }
                        Console.Write("Размер:{0}\n\t|Простой: {1}\n\t|Виноград:{2}\n", size, simple_t / 10.0, vinograd_t / 10.0);
                        for (thread_count = 2; thread_count <= 16; thread_count *= 2)
                        {
                            Multiplication.bourder = new int[thread_count, 2];
                            Multiplication.th = new Thread[thread_count];
                            simple_t_thread = 0.0;
                            vinograd_t_thread = 0.0;
                            for (int i = 0; i < 10; i++)
                            {
                                int begin = 0;
                                int h = size / thread_count;
                                //Заполнение матрицы границ
                                for (int k = 0; k < thread_count - 1; k++)
                                {
                                    Multiplication.bourder[k, 0] = begin;
                                    Multiplication.bourder[k, 1] = Multiplication.bourder[k, 0] + h;
                                    begin += h;
                                }
                                Multiplication.bourder[thread_count - 1, 0] = begin;
                                Multiplication.bourder[thread_count - 1, 1] = size;

                                for (int k = 0; k < thread_count; k++)
                                {
                                    Simple_Multiply tmp = new Simple_Multiply(size, size, Multiplication.bourder[k, 0], Multiplication.bourder[k, 1]);
                                    Multiplication.th[k] = new Thread(tmp.Simple_Multiply_Thread);
                                }

                                t.Reset();
                                t.Start();
                                Multiplication.Simple_Multiplication_Thread();
                                t.Stop();
                                simple_t_thread += t.ElapsedMilliseconds;

                                for (int k = 0; k < thread_count; k++)
                                {
                                    Simple_Multiply tmp = new Simple_Multiply(size, size, Multiplication.bourder[k, 0], Multiplication.bourder[k, 1]);
                                    Multiplication.th[k] = new Thread(tmp.Vinograd_Multiply_Thread);
                                }

                                t.Reset();
                                t.Start();
                                Multiplication.Vinograd_Multiplication_Thread(size);
                                t.Stop();
                                vinograd_t_thread += t.ElapsedMilliseconds;
                            }
                            Console.WriteLine("\tПоток:{0}\n\t|Простой:{1}\n\t|Виноград:{2}\n", thread_count, simple_t_thread / 10.0, vinograd_t_thread / 10.0);
                        }
                    }
                }
                else
                    break;
            }
        }
    }
}
