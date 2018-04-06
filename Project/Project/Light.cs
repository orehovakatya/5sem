using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.IO;
using System.Windows.Forms;

namespace Project
{
    class Light
    {
        Point3D Point;
        double Intensity;

        public Point3D point
        {
            set { Point = value; }
            get { return Point; }
        }

        public double intensity
        {
            set { Intensity = value; }
            get { return Intensity; }
        }

        public Light(Point3D point, double intens)
        {
            Point = point;
            Intensity = intens;
        }

        /// <summary>
        /// Загружает свет из файла в таблицу
        /// </summary>
        /// <param name="Data">Таблица</param>
        /// <param name="file">Файл</param>
        /// <returns></returns>
        private static int load_light(ref DataGridView Data, StreamReader file)
        {
            if (file == null)
                return -1;

            string line = file.ReadLine();
            if (line == null)
            {
                file.Close();
                return -1;
            }

            int count = 0;
            try
            {
                count = Convert.ToInt32(line);
            }
            catch
            {
                MessageBox.Show("Неверно задано количество ламп");
                return -1;
            }

            string[] str;
            Point3D point;
            double intensity;

            for (int i = 0; i< count;i++)
            {
                try
                {
                    str = (file.ReadLine().Split(' '));
                    point = new Point3D(Convert.ToDouble(str[0]), Convert.ToDouble(str[1]), Convert.ToDouble(str[2]));
                    line = file.ReadLine();
                    intensity = Convert.ToDouble(line);
                    Data.Rows.Add(point, intensity);
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка файла");
                    return -1;
                }
                
            }
            return 1;

        }

        /// <summary>
        /// Загрузка света из файла в таблицу
        /// </summary>
        /// <param name="openFileDialog1"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static bool OpenAndLoad(OpenFileDialog openFileDialog1, DataGridView Data)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                //StreamReader sr = new StreamReader("D:\\5sem\\Project\\Box.txt");
                if (Light.load_light(ref Data, sr) == -1)
                {
                    Data.Rows.Clear();
                    MessageBox.Show("Не удалось загрузить свет");
                    return false;
                }
                MessageBox.Show("Свет загружен");
                return true;
            }
            return false; ;
        }


        /// <summary>
        /// Считывание света из таблицы в список
        /// </summary>
        /// <param name="Data">Таблица</param>
        public static bool ReadFromGrid(DataGridView Data)
        {
            try
            {
                Trace.Light.Clear();
                Point3D P;
                double intens;
                string[] str;
                int[] DelIndex = new int[Data.RowCount];
                for (int j = 0; j < Data.RowCount; j++)
                    DelIndex[j] = 0;
                double all_int = Trace.LIGHT_SCENE;
                List<Light> Current = new List<Light>();

                int i = 0;

                while (i < Data.RowCount-1)
                {
                    str = Convert.ToString(Data[0, i].Value).Split(';');
                    P = new Point3D(Convert.ToDouble(str[0]), Convert.ToDouble(str[1]), Convert.ToDouble(str[2]));
                    //если не в воде
                    if (!Water.InWater(P))
                    {
                        DelIndex[i] = 1;
                        i++;
                        continue;
                    }
                    intens = Convert.ToDouble(Data[1, i].Value);
                    Trace.Light.Add(new Light(P, intens));
                    all_int += intens;
                    i++;
                }

                all_int = 1 / (1f*all_int);
                foreach (var lamp in Trace.Light)
                {
                        lamp.intensity *= all_int;
                }

                for (int j = 0; j < Data.RowCount; j++)
                {
                    if (DelIndex[j] == 1)
                        Data.Rows.RemoveAt(j);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка в таблице");
                Trace.Light.Clear();
                return false;
            }
            return true;
        }

    }
}
