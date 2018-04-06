using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media.Media3D;
using System.Windows.Forms;
using System.Drawing;

namespace Project
{
    class LoadModel
    {
        /// <summary>
        /// Считывает модель из файла
        /// </summary>
        /// <param name="Object"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static int load(ref List<Object> Object, StreamReader file)
        {
            if (file == null)
                return -1;
            string line = file.ReadLine();
            if (line == null)
            {
                file.Close();
                return -1;
            }
            string[] mas;
            int TopsNum;
            int MeshNum;
            try
            {
                mas = line.Split(' ');
                TopsNum = Convert.ToInt32(mas[0]); //кол-во вершин
                MeshNum = Convert.ToInt32(mas[1]); //кол-во треугольников
            }
            catch (Exception)
            {
                MessageBox.Show("Неверное количество вершин или треугольников");
                return -1;
            }

            List<Point3D> Tops = new List<Point3D>(); //вершины
            List<Triangle> Triangle = new List<Triangle>(); //треугольники
            List<int[]> Mesh = new List<int[]>(); //массив смежных точек
            Color Color = new Color();
            double Absorp;//поглощение
            double Refraction;//Приломление

            try
            {
                for (int i = 0; i < TopsNum; i++)
                {
                    mas = file.ReadLine().Split(' ');
                    Tops.Add(new Point3D(Convert.ToDouble(mas[0]), Convert.ToDouble(mas[1]), Convert.ToDouble(mas[2])));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка при чтении точек");
                return -1;
            }
            try
            {
                for (int i = 0; i < MeshNum; i++)
                {
                    mas = file.ReadLine().Split(' ');
                    Mesh.Add(new int[3] { Convert.ToInt32(mas[0]), Convert.ToInt32(mas[1]), Convert.ToInt32(mas[2]) });
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка при чтении граней");
                return -1;
            }
            try
            {
                mas = file.ReadLine().Split(' ');
                Color = Color.FromArgb(Convert.ToInt32(mas[0]), Convert.ToInt32(mas[1]), Convert.ToInt32(mas[2]));
                Absorp = Convert.ToDouble(file.ReadLine());
                Refraction = Convert.ToDouble(file.ReadLine());

            }
            catch
            {
                MessageBox.Show("Не удалось прочитать дополнительные параметры");
                return -1;
            }

            for (int i = 0; i < MeshNum; i++)
            {
                Triangle.Add(new Triangle(Mesh[i], Tops[Mesh[i][0]], Tops[Mesh[i][1]], Tops[Mesh[i][2]]));
            }
            Object.Add(new Object(Tops, Triangle, Color, Absorp, Refraction));
            return 1;
        }


        /// <summary>
        /// Считывает модель из диалога
        /// </summary>
        /// <param name="Object"></param>
        /// <param name="openFileDialog1"></param>
        /// <returns></returns>
        public static bool OpenAndLoad(ref List<Object> Object, OpenFileDialog openFileDialog1)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                int count = Object.Count();
                if (LoadModel.load(ref Object, sr) != 1)
                {
                    sr.Close();
                    if (Object.Count() > count)
                        Object.RemoveAt(Object.Count() - 1);
                    MessageBox.Show("Не удалось считать файл");
                    sr.Close();
                    return false;
                }
                MessageBox.Show("Файл загружен");
                sr.Close();
                return true;
            }
            return false;
        }

    }
}
