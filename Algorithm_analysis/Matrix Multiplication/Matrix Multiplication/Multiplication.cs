using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix_Multiplication
{
    class Multiplication
    {
        public static void Simple_Multiplication(ref int[,]C, int[,] A, int[,] B, int size)
        {
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    C[i, j] = 0;
                    for (int k = 0; k < size; k++)
                        C[i, j] = C[i, j] + A[i,k] * B[k,j];
                }
        }

        public static void Vinograd(ref int[,] C,int[,] A, int[,] B, int size, int [] Rows, int [] Column)
        {

            for (int i = 0; i < size; i++)
            {
                Rows[i] = A[i, 0] * A[i, 1];
                for (int j = 1; j < size/2; j++)
                    Rows[i] = Rows[i]+ A[i, 2 * j] * A[i, 2 * j + 1];
            }


            for (int i = 0; i < size; i++)
            {
                Column[i] = B[0, i] * B[1, i];
                for (int j = 1; j < size/2; j++)
                    Column[i] = Column[i]+ B[2 * j, i] * B[2 * j + 1, i];
            }
            

            for(int i = 0; i< size; i++)
                for (int j = 0; j<size;j++)
                {
                    C[i, j] = -Rows[i] - Column[j];
                    for (int k = 0; k < size/2; k++)
                        C[i, j] = C[i, j]+(A[i,2*k+1]+B[2*k,j]) * (A[i,2*k]+B[2*k+1,j]);
                }
            if (size % 2 == 1)
            {
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                        C[i, j] = C[i, j]+ A[i,size-1] * B[size-1,j];
            }
        }

        public static void Vinograd_Better(ref int[,] C, int[,] A, int[,] B, int size, int[] Rows, int[] Column)
        {
            int d = size / 2;
            int new_size = size - 1;

            for (int i = 0; i < size; i++)
            {
                Rows[i] = A[i, 0] * A[i, 1];
                for (int j = 2; j < new_size; j+=2)
                    Rows[i] += A[i, j] * A[i, j + 1];
            }

            for (int i = 0; i < size; i++)
            {
                Column[i] = B[0, i] * B[1, i];
                for (int j = 2; j < new_size; j+=2)
                    Column[i] += B[j, i] * B[j + 1, i];
            }

            if (size % 2 == 1)
            {
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                    {
                        C[i, j] = -Rows[i] - Column[j] + A[i, size - 1] * B[size - 1, j];
                        for (int k = 0; k < d; k++)
                            C[i, j] += (A[i, 2 * k + 1] + B[2 * k, j]) * (A[i, 2 * k] + B[2 * k + 1, j]);
                    }
            }
            else
            {
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                    {
                        C[i, j] = -Rows[i] - Column[j];
                        for (int k = 0; k < d; k++)
                            C[i, j] += (A[i, 2 * k + 1] + B[2 * k, j]) * (A[i, 2 * k] + B[2 * k + 1, j]);
                    }
            }
        }
    }
}
