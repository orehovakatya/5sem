using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levenshteinch
{
    class functions
    {
        /// <summary>
        /// число, обозначающее бесконечность
        /// </summary>
        public static int max;

        /// <summary>
        /// Поиск редакционного расстояния с перестановками
        /// </summary>
        /// <param name="word1">Первое слово</param>
        /// <param name="word2">Второе слово</param>
        /// <param name="len1">Длина первого слова</param>
        /// <param name="len2">Длина второго слова</param>
        /// <returns>Наименьшее редакционное расстояние</returns>
        public static int distance_modify(string word1, string word2, int len1, int len2)
        {
            int d = 0;
            int[,] matr = new int[len1 + 1, len2 + 1];
            max = Math.Max(len1, len2) + 1;

            //заполнение нулевой строки
            for (int i = 0; i <= len1; i++)
                matr[i, 0] = i;

            //заполнение нулевого столбца
            for (int i = 0; i <= len2; i++)
                matr[0, i] = i;

            //заполнение матрицы
            for (int j = 1; j <= len2; j++)//по столбцам
                for (int i = 1; i <= len1; i++)//по строкам
                {
                    if ((j>=2)&&(i>=2))
                        matr[i, j] = Math.Min(Math.Min(matr[i - 1, j] + 1, matr[i, j - 1] + 1),
                        Math.Min(m(word1[i - 1], word2[j - 1]) + matr[i - 1, j - 1],
                        transpoze(word1,word2,i-1,j-1) +matr[i-2,j-2]));
                    else
                        matr[i, j] = Math.Min(Math.Min(matr[i - 1, j] + 1, matr[i, j - 1] + 1),
                        m(word1[i - 1], word2[j - 1]) + matr[i - 1, j - 1]);

                }

            /*for (int i = 0; i <= len1; i++)
            {
                for (int j = 0; j <= len2; j++)
                    Console.Write(matr[i, j].ToString() + " ");
                Console.Write("\n");
            }*/
            d = matr[len1, len2];
            return d;
        }

        /// <summary>
        /// Поиск редакционного расстояния с помощью матрицы
        /// </summary>
        /// <param name="word1">Первое слово</param>
        /// <param name="word2">Второе слово</param>
        /// <param name="len1">Длина первого слова</param>
        /// <param name="len2">Длина второго слова</param>
        /// <returns>Наименьшее редакционное расстояние</returns>
        public static int distance_matr(string word1, string word2, int len1, int len2)
        {
            int d = 0;
            int[,] matr = new int[len1+1,len2+1];
            
            //заполнение нулевой строки
            for (int i = 0; i <= len1; i++)
                matr[i,0] = i;
            
            //заполнение нулевого столбца
            for (int i = 0; i <= len2; i++)
                matr[0, i] = i;

            //заполнение матрицы
            for (int j = 1; j <= len2; j++)//по столбцам
                for (int i = 1; i <= len1; i++)//по строкам
                    matr[i, j] = Math.Min(Math.Min(matr[i-1,j]+1,matr[i,j-1]+1),
                        m(word1[i-1],word2[j-1])+matr[i-1,j-1]);

            /*for (int i = 0; i <= len1; i++)
            {
                for (int j = 0; j <= len2; j++)
                    Console.Write(matr[i, j].ToString() + " ");
                Console.Write("\n");
            }*/
            d = matr[len1, len2];
            return d;
        }

        /// <summary>
        /// Рекурсивный поиск расстояния Левенштейна(редакционное расстояние)
        /// </summary>
        /// <param name="word1">Первое слово</param>
        /// <param name="word2">Второе слово</param>
        /// <param name="len1">Длина первого слова(счет с 0)</param>
        /// <param name="len2">Длина второго слова(счет с 0)</param>
        /// <returns>Наименьшее расстояние Левенштейна</returns>
        public static int distance_rec(string word1, string word2, int len1, int len2)
        {
            if (len1 == -1)
            {
                if (len2 == -1)
                    return 0;
                return len2+1;
            }
            if (len2 == -1)
                return len1+1;
            int d = 0;
            d = Math.Min(distance_rec(word1, word2, len1, len2 - 1) + 1, Math.Min(
                distance_rec(word1, word2, len1 - 1, len2) + 1,
                distance_rec(word1, word2, len1 - 1, len2 - 1) + m(word1[len1], word2[len2])));
            return d;
        }

        /// <summary>
        /// Сравнение 2-х символов
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>0-символы равны, 1-символы различны</returns>
        public static int m(char a, char b)
        {
            if (a == b)
                return 0;
            return 1;
        }

        /// <summary>
        /// Коэффициент для транспозиции
        /// </summary>
        /// <param name="word1">Первое слово</param>
        /// <param name="word2">Второе слово</param>
        /// <param name="i">Индекс первого слова</param>
        /// <param name="j">Индекс второго слова</param>
        /// <returns></returns>
        private static int transpoze(string word1, string word2, int i, int j)
        {
            if ((word1[i] == word2[j-1])&&(word1[i-1]==word2[j]))
                return 1;
            return max;
        }

    }
}
