using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataGenerator
{
    class Client
    {
        static public void create_table_client(StreamWriter Client_out, int N)
        {
            List<String> Passports = new List<string>();
            int id;
            String passport;
            String surname;
            String name;
            String father;
            DateTime birthday;
            Random rand = new Random();
            for (int i = 0; i < N; i++)
            {
                passport = create_passport(rand);
                while (Passports.Contains(passport))
                {
                    passport = create_passport(rand);
                }
                Passports.Add(passport);
            }
            for (int i = 1; i <= N; i++)
            {
                id = i;
                int m = rand.Next(0, 100);//для создани мужчины или женщины
                if (m % 2 == 0)
                {
                    //четное -- жен
                    surname = create_surname_woman(rand);
                    name = create_name(rand, Data.Woman_Name);
                    father = create_name(rand,Data.Woman_father);
                }
                else
                {
                    //нечетное -- муж
                    surname = create_surname_man(rand);
                    name = create_name(rand,Data.Man_Name);
                    father = create_name(rand,Data.Man_father);
                }
                birthday = create_birthday(rand);
                passport = Passports[i - 1];
                Client_out.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5:d}\n", 
                    id, passport, surname, name, father, birthday);
            }
        }

        static private String create_passport(Random rand)
        {
            String passport = "";
            int t;
            for (int i = 0; i< 10; i++)
            {
                t = rand.Next(0, 10);
                passport += Convert.ToString(t);
            }
            return passport;
        }

        static private String create_name(Random rand, List<String> Name)
        {
            String name;
            int t = rand.Next(0, Name.Count());
            name = Name[t];
            return name;
        }

        static private DateTime create_birthday(Random rand)
        {
            DateTime date;
            int year, month, day;
            year = rand.Next(1917, 2000);
            month = rand.Next(1, 12);
            if (month == 2)
                day = rand.Next(1, 29);
            else
                day = rand.Next(1, 31);
            return date = new DateTime(year, month, day);
        }
        static private String create_surname(Random rand)
        {
            String surname;
            int t = rand.Next(0, Data.Surname.Count());
            surname = Data.Surname[t];
            return surname;
        }
        static private String create_surname_man(Random rand)
        {
            return create_surname(rand);
        }
        static private String create_surname_woman(Random rand)
        {
            return create_surname(rand) + "a";
        }
    }
}
