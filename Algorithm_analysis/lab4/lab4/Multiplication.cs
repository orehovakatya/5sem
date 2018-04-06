using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace lab4
{
    class Multiplication
    {
        static public int[] Rows;
        static public int[] Column;
        static public int[,] bourder;
        static public Thread[] th;

        public static void Simple_Multiplication(ref int[,] C,int size)
        {
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    C[i, j] = 0;
                    for (int k = 0; k < size; k++)
                        C[i, j] += Program.A[i, k] * Program.B[k, j];
                }
        }

        public static void Simple_Multiplication_Thread()
        {
            for (int i = 0; i < th.Length-1; i++)
                th[i].Start();
            for (int i = 0; i < th.Length - 1; i++)
                th[i].Join();
        }

       

        public static void Vinograd_Multiplication_Thread(int size)
        {

            for (int i = 0; i < size; i++)
            {
                Rows[i] = Program.A[i, 0] * Program.A[i, 1];
                for (int j = 1; j < size / 2; j++)
                    Rows[i] += Program.A[i, 2 * j] * Program.A[i, 2 * j + 1];
            }


            for (int i = 0; i < size; i++)
            {
                Column[i] = Program.B[0, i] * Program.B[1, i];
                for (int j = 1; j < size / 2; j++)
                    Column[i] += Program.B[2 * j, i] * Program.B[2 * j + 1, i];
            }
            for (int i = 0; i < th.Length - 1; i++)
                th[i].Start();
            for (int i = 0; i < th.Length - 1; i++)
                th[i].Join();
        }

        public static void Vinograd(ref int[,] C, int[,] A, int[,] B, int size, int[] Rows, int[] Column)
        {

            for (int i = 0; i < size; i++)
            {
                Rows[i] = A[i, 0] * A[i, 1];
                for (int j = 1; j < size / 2; j++)
                    Rows[i] += A[i, 2 * j] * A[i, 2 * j + 1];
            }


            for (int i = 0; i < size; i++)
            {
                Column[i] = B[0, i] * B[1, i];
                for (int j = 1; j < size / 2; j++)
                    Column[i] += B[2 * j, i] * B[2 * j + 1, i];
            }


            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    C[i, j] = -Rows[i] - Column[j];
                    for (int k = 0; k < size / 2; k++)
                        C[i, j] += (A[i, 2 * k + 1] + B[2 * k, j]) * (A[i, 2 * k] + B[2 * k + 1, j]);
                }
            if (size % 2 == 1)
            {
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                        C[i, j] += A[i, size - 1] * B[size - 1, j];
            }
        }
    }
}
