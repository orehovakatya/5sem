using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class Simple_Multiply
    {
        private int begin;
        private int end;
        private int c;
        private int b;

        public Simple_Multiply(int b, int c, int begin, int end)
        {
            this.begin = begin;
            this.end = end;
            this.c = c;
            this.b = b;
        }

        public void Simple_Multiply_Thread()
        {
            for (int i = begin; i < end; i++)
                for (int j = 0; j < c; j++)
                {
                    Program.D[i, j] = 0;
                    for (int k = 0; k < b; k++)
                        Program.D[i, j] += Program.A[i, k] *Program.B[k, j];
                }
        }

        public void Vinograd_Multiply_Thread()
        {
            for (int i = begin; i < end; i++)
                for (int j = 0; j < c; j++)
                {
                    Program.D[i, j] = -Multiplication.Rows[i] - Multiplication.Column[j];
                    for (int k = 0; k < b / 2; k++)
                        Program.D[i, j] += (Program.A[i, 2 * k + 1] + Program.B[2 * k, j]) * (Program.A[i, 2 * k] + Program.B[2 * k + 1, j]);
                }
            if (b % 2 == 1)
            {
                for (int i = begin; i < end; i++)
                    for (int j = 0; j < c; j++)
                        Program.D[i, j] += Program.A[i,b - 1] * Program.B[b - 1, j];
            }
        }
    }
}
