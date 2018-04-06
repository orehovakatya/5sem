using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataGenerator
{
    class Agent
    {
        static public void create_table_agent(StreamWriter Agent_out, int N)
        {
            int id;
            String surname;
            String name;
            String father;
            Random rand = new Random();
            for (int i = 1; i <= N; i++)
            {
                id = i;
                int m = rand.Next(0, 100);//для определения мужчины или женщины
                if (m % 2 == 0)
                {
                    //четное -- жен
                    surname = create_surname_woman(rand);
                    name = create_name(rand, Data.Woman_Name);
                    father = create_name(rand, Data.Woman_father);
                }
                else
                {
                    //нечетное -- муж
                    surname = create_surname_man(rand);
                    name = create_name(rand, Data.Man_Name);
                    father = create_name(rand, Data.Man_father);
                }
                Agent_out.WriteLine("{0}\t{1}\t{2}\t{3}\n", id, surname, name, father);
            }
        }

        static private String create_name(Random rand, List<String> Name)
        {
            String name;
            int t = rand.Next(0, Name.Count());
            name = Name[t];
            return name;
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
            return create_surname(rand)+"a";
        }
    }
}
