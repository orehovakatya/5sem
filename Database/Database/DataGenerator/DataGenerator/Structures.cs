using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    class Structures 
    {
        /// <summary>
        /// Страховки
        /// </summary>
        public struct Insure
        {
            public int id;
            public String sphere;
            public double price;
            public double payments;
        }

        public struct Agent
        {
            public int id;
            public String surname;
            public String name;
            public String patronymic;
        }

        public struct Client
        {
            public int id;
            public long passport;
            public String surname;
            public String name;
            public String patronymic;
            //DateTime date1 = new DateTime(2015, 7, 20, 18, 30, 25); год - месяц - день - час - минута - секунда
            public DateTime birthday;
            public String nationality;
        }

        public struct Sales
        {
            public int id;
            public int id_client;
            public int id_agent;
            public int insure;
            DateTime begin;
            DateTime end;
        }

    }
}
