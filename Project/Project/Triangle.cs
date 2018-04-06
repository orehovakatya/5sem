using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Media3D;

namespace Project
{
    /// <summary>
    /// Описывает один треугольник
    /// </summary>
    class Triangle
    {
        private int[] mesh = new int[3];
        private Vector3D normal = new Vector3D();
        

        public Vector3D Normal
        {
            get { return normal; }
            set { normal = value; }
        }

        public int A
        {
            get { return mesh[0];}
            set { mesh[0] = value; }
        }

        public int B
        {
            get { return mesh[1]; }
            set { mesh[1] = value; }
        }

        public int C
        {
            get { return mesh[2]; }
            set { mesh[2] = value; }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="mas">массив смежных точек</param>
        /// <param name="norm">нормаль</param>
        public Triangle(int[] mas, Vector3D norm)
        {
            mesh = mas;
            normal = norm;
        }

        public Triangle(int[] mas, Point3D A, Point3D B, Point3D C)
        {
            mesh = mas;
            normal = Vector3D.CrossProduct(new Vector3D(B.X - A.X, B.Y - A.Y, B.Z - A.Z), new Vector3D(C.X - A.X, C.Y - A.Y, C.Z - A.Z));
            normal.Normalize();
        }
    }
}