using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataGenerator
{
    class Insure
    {
        static public void create_table_insure (StreamWriter Insure_out, int N)
        {
            int id;
            String sphere;
            int price;
            int payments;
            Random rand = new Random();
            for (int i = 1; i <= N; i++)
            {
                id = i;
                sphere = create_sphere(rand);
                price = create_price(rand);
                payments = create_payments(rand, price);
                Insure_out.WriteLine("{0}\t{1}\t{2}\t{3}\n",id,sphere,price,payments);
            }
        }

        static private String create_sphere(Random rand_sphere)
        {
            int temp;
            temp = rand_sphere.Next(0, Data.Sphere.Count());
            return Data.Sphere[temp];
        }

        static private int create_price(Random rand_money)
        {
            int temp;
            //temp = Convert.ToDouble(rand.Next(10000)) / 100; //от 0 до 100 с точностью для сотых
            temp = rand_money.Next(1000, 10000);
            return temp;
        }

        static private int create_payments(Random rand_money, int price)
        {
            int temp;
            temp = rand_money.Next(1000,10000) + price;
            return temp;
        }

    }
}
