using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataGenerator
{
    class Sales
    {
        static public void create_table_sales(StreamWriter Sales_out, int N)
        {
            int id;
            int id_client;
            int id_agent;
            int id_insure;
            DateTime begin = new DateTime();
            DateTime end = new DateTime();
            Random rand = new Random();
            for (int i = 1; i <= N; i++)
            {
                id = i;
                id_client = create_id_client(rand);
                id_agent = create_id_agent(rand);
                id_insure = create_id_insure(rand);
                create_date(rand, ref begin, ref end);
                Sales_out.WriteLine("{0}\t{1}\t{2}\t{3}\t{4:d}\t{5:d}\n", 
                    id, id_client, id_agent, id_insure, begin, end);
            }
        }

        static private void create_date(Random rand, ref DateTime begin, ref DateTime end)
        {
            int year, month, day;
            year = rand.Next(2000, 2017);
            month = rand.Next(1, 12);
            if (month == 2)
                day = rand.Next(1, 29);
            else
                day = rand.Next(1, 31);
            begin = new DateTime(year, month, day);
            year = begin.Year + rand.Next(50);
            end = new DateTime(year, month, day);
        }

        static private int create_id_insure(Random rand)
        {
            int t = rand.Next(1, Program.N_Insure);
            return t;
        }

        static private int create_id_client(Random rand)
        {
            int t = rand.Next(1, Program.N_Client);
            return t;
        }

        static private int create_id_agent(Random rand)
        {
            int t = rand.Next(1, Program.N_Agent);
            return t;
        }

    }
}
