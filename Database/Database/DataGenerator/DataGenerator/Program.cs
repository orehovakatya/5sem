using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataGenerator
{
    class Program
    {
        //Файлы для вывода
        public const String FNAME_INSURE_OUT = "out\\Insure.txt";   //Страховки
        public const String FNAME_AGENT_OUT = "out\\Agent.txt";     //Агенты
        public const String FNAME_CLIENT_OUT = "out\\Client.txt";   //Клинты
        public const String FNAME_SALES_OUT = "out\\Sales.txt";     //Продажи страховок

        public const int N_Insure = 1000;
        public const int N_Agent = 1000;
        public const int N_Client = 1000;
        public const int N_Sales = 5000;

        static void Main(string[] args)
        {
            

            StreamWriter Insure_out = new StreamWriter(FNAME_INSURE_OUT, false, System.Text.Encoding.GetEncoding(1251));
            StreamWriter Agent_out = new StreamWriter(FNAME_AGENT_OUT, false, System.Text.Encoding.GetEncoding(1251));
            StreamWriter Client_out = new StreamWriter(FNAME_CLIENT_OUT, false, System.Text.Encoding.GetEncoding(1251));
            StreamWriter Sales_out = new StreamWriter(FNAME_SALES_OUT, false, System.Text.Encoding.GetEncoding(1251));

            Data.fill();

            Insure.create_table_insure(Insure_out, N_Insure);
            Agent.create_table_agent(Agent_out, N_Agent);
            Client.create_table_client(Client_out, N_Client);
            Sales.create_table_sales(Sales_out, N_Sales);

            Insure_out.Close();
            Agent_out.Close();
            Client_out.Close();
            Sales_out.Close();

            Console.WriteLine("OK");
            Console.ReadLine();
        }
    }
}
