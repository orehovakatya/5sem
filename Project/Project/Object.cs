using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Drawing;

namespace Project
{
    class Object
    {
        private List<Point3D> Tops = new List<Point3D>();
        private List<Triangle> Triangle = new List<Triangle>();
        private Color Color = new Color();
        double Absorp;//поглощение
        double Refraction;//Приломление

        public Object(List<Point3D> tops, List<Triangle> triangle, Color color,double absorp, double refraction)
        {
            Tops = tops;
            Triangle = triangle;
            Color = color;
            Absorp = absorp;
            Refraction = refraction;
        }

        public Object() {}

        public List<Triangle> triangle
        {
            get { return Triangle; }
        }

        public List<Point3D> tops
        {
            get { return Tops; }
        }

        public Color color
        {
            get { return Color; }
        }

        public double absorp
        {
            get { return Absorp; }
            set { Absorp = value; }
        }

        public double refraction
        {
            get { return Refraction; }
            set { Refraction = value; }
        }
    }
}
