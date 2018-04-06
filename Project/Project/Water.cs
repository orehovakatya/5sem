using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Project
{
    class Water
    {
        //Ограничивающие координаты
        static private double x;
        static private double x1;
        static private double y;
        static private double y1;
        static private double z;
        static private double z1;

        /// <summary>
        /// Заполнение ограничивающих координат воды
        /// </summary>
        /// <param name="Tops"></param>
        static public void FullWater(List<Point3D> Tops)
        {
            //Для Х
            if (!Tops.Any())
                return;
            double a = Tops[0].X;
            double b = 0;
            int i = 0;
            while(i<Tops.Count())
            {
                if (a != Tops[i].X)
                {
                    b = Tops[i].X;
                    break;
                }
                i++;
            }
            if (a < b)
            {
                x = a;
                x1 = b;
            }
            else
            {
                x = b;
                x1 = a;
            }
            

            //Для У
            a = Tops[0].Y;
            b = 0;
            i = 0;
            while (i < Tops.Count())
            {
                if (a != Tops[i].Y)
                {
                    b = Tops[i].Y;
                    break;
                }
                i++;
            }
            if (a < b)
            {
                y = a;
                y1 = b;
            }
            else
            {
                y = b;
                y1 = a;
            }

            //Для Z
            a = Tops[0].Z;
            b = 0;
            i = 0;
            while (i < Tops.Count())
            {
                if (a != Tops[i].Z)
                {
                    b = Tops[i].Z;
                    break;
                }
                i++;
            }
            if (a < b)
            {
                z = a;
                z1 = b;
            }
            else
            {
                z = b;
                z1 = a;
            }
        }

        /// <summary>
        /// Проверка принадлежности точки воде
        /// </summary>
        /// <param name="P"></param>
        /// <returns></returns>
        static public bool InWater(Point3D P)
        {
            if ((x<P.X)&&(P.X<x1))
                if ((y<P.Y)&&(P.Y<y1))
                    if ((z < P.Z) && (P.Z < z1))
                        return true;
            return false;
        }

        /// <summary>
        /// Проверка принадлежности объекта воде
        /// </summary>
        /// <param name="Tops"></param>
        /// <returns></returns>
        static public bool ObjectInWater(List<Point3D> Tops)
        {
            foreach (var point in Tops)
            {
                if (!InWater(point))
                    return false;
            }
            return true;
        }
    }
}
