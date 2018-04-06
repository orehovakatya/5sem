using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Project
{
    class ColorList
    {
        private Color col;
        private double intensity;

        public Color Color
        {
            set { col = value; }
            get { return col; }
        }

        public double Intensity
        {
            set { intensity = value; }
            get { return intensity; }
        }

        public ColorList(Color color, double intensity)
        {
            Color = color;
            Intensity = intensity;
        }

    }
}
