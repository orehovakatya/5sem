using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Project
{
    class MyStack
    {
        //Запоминаем точку пересечения, индекс объекта, материал, прозрачность
        private Point3D p;
        private int index;
        private double absorp;
        private Vector3D N;

        public Vector3D Normal
        {
            set { N = value; }
            get { return N; }
        }

        public Point3D P
        {
            set { p = value; }
            get { return p; }
        }

        public int Index
        {
            set { index = value;}
            get { return index; }
        }

        public double Absorb
        {
            set { absorp = value;}
            get { return absorp; }
        }

        public MyStack(Point3D Point, int index, double absorb, Vector3D normal)
        {
            P = Point;
            Index = index;
            Absorb = absorb;
            Normal = normal;
        }
    }
}
