using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataGenerator
{
    class Data
    {
        static public String[] Sphere = {
            "Страхование жизни",
            "Страхование от несчастных случаев",
            "Страхование от болезней",
            "Медицинское страхование",
            "Страхование предпринимательских рисков",
            "Страхование финансовых рисков",
            "Страхование имущества предприятий",
            "Страхование имущества организаций",
            "Страхование имущества граждан",
            "Транспортное страхование",
            "Cтрахование военнослужащих",
            "Cтрахование пассажиров"};

        static public String[] Male =
        {
            "Мужской",
            "Женский"
        };

        static public List<String> Surname = new List<String>();
        static public List<String> Man_Name = new List<String>();
        static public List<String> Woman_Name = new List<String>();
        static public List<String> Woman_father = new List<String>();
        static public List<String> Man_father = new List<String>();


        static public void fill()
        {
            StreamReader surname_in = new StreamReader("in\\Surname.txt", System.Text.Encoding.GetEncoding(1251));
            String line;
            while ((line = surname_in.ReadLine()) != null)
                Surname.Add(line);
            surname_in.Close();

            StreamReader name_in = new StreamReader("in\\Man_name.txt", System.Text.Encoding.GetEncoding(1251));
            while ((line = name_in.ReadLine()) != null)
                Man_Name.Add(line);
            name_in.Close();

            name_in = new StreamReader("in\\Woman_name.txt", System.Text.Encoding.GetEncoding(1251));
            while ((line = name_in.ReadLine()) != null)
                Woman_Name.Add(line);
            name_in.Close();

            name_in = new StreamReader("in\\Woman_father.txt", System.Text.Encoding.GetEncoding(1251));
            while ((line = name_in.ReadLine()) != null)
                Woman_father.Add(line);
            name_in.Close();

            name_in = new StreamReader("in\\Man_father.txt", System.Text.Encoding.GetEncoding(1251));
            while ((line = name_in.ReadLine()) != null)
                Man_father.Add(line);
            name_in.Close();
        }


    }
}
