using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix_Multiplication
{
    class CreateMatrix
    {
        /// <summary>
        /// Создает квадратную матрицу
        /// </summary>
        /// <param name="size">размер матрицы</param>
        /// <returns>матрица</returns>
        public static int[,] create_matrix(int size)
        {
            int[,] matrix = new int[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    matrix[i,j] = Program.rnd.Next(0, 10);
            return matrix;
        }
    }
}
