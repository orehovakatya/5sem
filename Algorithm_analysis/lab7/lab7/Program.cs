using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace lab7
{
    class Program
    {
        static Random rand = new Random();

        struct array_t
        {
            public double[] arr;
            public int size;
        }

        struct solution_t
        {
            public int l;
            public array_t route;
        }

        struct matrix_t
        {
            public double[,] matr;
            public int n; // TODO: Заменить на size
            public int m;
        }

        struct ant_t
        {
            public int start_city;
            public int curr_city;
            public int Lk;
            public array_t route;
            public array_t Jk;
        }

        static ant_t[] create_ant_array(int num_of_cities)
        {
            ant_t[] ant_arr = new ant_t[num_of_cities];
            for (int i = 0; i < num_of_cities; i++)
                ant_arr[i] = create_ant(i, num_of_cities);
            return ant_arr;
        }

        static array_t create_array(int size)
        {
            array_t array = new array_t();
            array.arr = new double[size];
            array.size = size;
            return array;
        }

        static ant_t create_ant(int start_pos, int num_of_cities)
        {
            ant_t ant = new ant_t();
            ant.start_city = start_pos;
            ant.curr_city = start_pos;
            ant.Lk = 0;
            ant.route = create_array(num_of_cities);
            ant.Jk = create_array(num_of_cities);
            return ant;
        }

        static matrix_t create_matrix(int n, int m)
        {
            matrix_t matrix = new matrix_t();
            matrix.matr = new double[n, m];
            matrix.m = m;
            matrix.n = n;
            return matrix;
        }

        static void recalc_weight(ref matrix_t weight, matrix_t pheromon, matrix_t visib, double a, double b)
        {
            int N = weight.n;
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    weight.matr[i,j] = Math.Pow(pheromon.matr[i,j], a)*Math.Pow(visib.matr[i,j], b);
        }

        static void init_ant(ref ant_t ant)
        {
            int start = ant.start_city;
            ant.curr_city = start;
            ant.Lk = 0;
            for (int i = 0; i < ant.route.size; i++)
            {
                ant.route.arr[i] = -1;
                ant.Jk.arr[i] = 1;
            }
            ant.route.arr[0] = start;
            ant.Jk.arr[start] = 0;
        }

        static int choose_next(array_t prob)
        {
            int i = 0;
            double r = rand.NextDouble();
            if (r == 0)
            {
                while (prob.arr[i++] <= 0) ;
                return --i;
            }
            while (r > 0)
                r -= prob.arr[i++];

            return --i;
        }

        static double length_of_route(matrix_t adj_mat, array_t route)
        {
            double length = 0;
            for (int i = 0; i < route.size - 1; i++)
                length += adj_mat.matr[(int)route.arr[i],(int)route.arr[i + 1]];
            return length;
        }

        static void add_pheromon(matrix_t d_pheromon, array_t route, int Lk, int q)
        {
            float d_fer = (float)q / (float)Lk;
            for (int i = 0; i < route.size - 1; i++)
                d_pheromon.matr[(int)route.arr[i],(int)route.arr[i + 1]] += d_fer;
        }

        static void gogo_ant(ref ant_t ant, matrix_t adj_mat, matrix_t weight, matrix_t d_pheromon, int q)
        {
            int N = weight.n;
            int i = 1;
            int next = 0;
            array_t prob = create_array(N);
            while (i < N)
            {

                double sum_weight = 0;
                for (int j = 0; j < N; j++)
                    sum_weight += weight.matr[ant.curr_city,j] * ant.Jk.arr[j];
                for (int j = 0; j < N; j++)
                {
                    prob.arr[j] = weight.matr[ant.curr_city,j] / sum_weight * ant.Jk.arr[j];
                }
                next = choose_next(prob);
                ant.curr_city = next;
                ant.Jk.arr[next] = 0;
                ant.route.arr[i++] = next;
            }
            ant.Lk = (int)(length_of_route(adj_mat, ant.route));
            add_pheromon(d_pheromon, ant.route, ant.Lk, q);
        }


        static void copy_array(array_t dst, array_t src)
        {
            if (dst.size == src.size)
            {
                for (int i = 0; i<dst.size; i++)
                    dst.arr[i] = src.arr[i];
            }
        }

        static void recalc_pheromon(ref matrix_t pheromon, double p)
        {
            p = 1 - p;
            int N = pheromon.n;
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                {
                    pheromon.matr[i, j] = pheromon.matr[i, j] * p;
                   
                }
        }

        static solution_t solve(matrix_t adj_mat, double a, double b, double p, int q, int t_max)
        {
            int N = adj_mat.n;

            ant_t[] ants = create_ant_array(N); 

            matrix_t pheromon = create_matrix(N, N);
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    pheromon.matr[i,j] = 0.5;

            matrix_t visib = create_matrix(N, N);
            for (int i = 0; i < N; i++)
                for (int j = i; j < N; j++)
                    if (i != j)
                    {
                        visib.matr[i,j] = 1 / adj_mat.matr[i,j];
                        visib.matr[j,i] = visib.matr[i,j];
                    }
                    else
                        visib.matr[i,j] = 1000;

            matrix_t weight = create_matrix(N, N);
            recalc_weight(ref weight, pheromon, visib, a, b);
            
            int best_l = int.MaxValue;
            array_t route = create_array(N);

            for (int t = 0; t < t_max; t++)
            {
                for (int k = 0; k < N; k++)
                {
                    init_ant(ref ants[k]);
                    gogo_ant(ref ants[k], adj_mat, weight, pheromon, q);
                }
                int best = -1;
                for (int i = 0; i < N; i++)
                    if (ants[i].Lk < best_l)
                    {
                        best = i;
                        best_l = ants[i].Lk;
                    }
                if (best != -1)
                    copy_array(route, ants[best].route);

                recalc_pheromon(ref pheromon,  p);
                recalc_weight(ref weight, pheromon, visib, a, b);
            }

            solution_t solv = new solution_t();
            solv.l = best_l;
            solv.route = route;
            return solv;
        }

        static double[,] create_matr(int size)
        {
            double[,] array = new double[size, size];
            for (int i = 0; i < size; i++)
                for (int j = i; j< size; j++)
                {
                    if (i != j)
                    {
                        array[i, j] = rand.Next(1, 10);
                        array[j,i] = array[i, j];
                    }
                    else
                        array[i, j] = 0;
                }
            return array;
        } 

        static void Main(string[] args)
        {
            //double[,] array = new double[,] { {0, 1, 3, 6}, {1, 0, 2, 5}, {3, 2, 0, 4}, {6, 5, 4, 0} };
            double[,] array = create_matr(50);
            matrix_t matr = new matrix_t();
            matr.matr = array;
            matr.m = 49;
            matr.n = 49;

            int q = 50;
            solution_t sol;
            for (double i = 0.0; i <= 1.01; i += 0.1)
            {

                sol = solve(matr, i, 1 - i, 0.5, q, 200);
                Console.WriteLine("Alpha: {0}\nBetta: {1}\nДлина: {2}\n-----------------------", i, 1 - i, sol.l);

            }
            for (int i = 100; i <= 1000; i += 100)
            {

                sol = solve(matr, 0.5, 0.5, 0.5, q, i);
                Console.WriteLine("Время жизни: {0}\nДлина: {1}\n-----------------------", i, sol.l);
            }
            Console.Read();


        }
    }
}
