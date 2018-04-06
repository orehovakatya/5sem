using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Project
{
    class Camera
    {
        private Point3D Position = new Point3D(0,0,0);
        private Vector3D Up = new Vector3D(0,0,0);
        private Vector3D Dir = new Vector3D(0,0,0);
        private Vector3D Right = new Vector3D(0,0,0);

        public Camera(Point3D position, Vector3D u, Vector3D d)
        {
            Position = position;
            Up = Vector3D.Divide(u,u.Length);
            Dir = Vector3D.Divide(d, d.Length);
            Right = Vector3D.CrossProduct(Up, Dir);
            Right = Vector3D.Divide(Right, Right.Length);  
        }

        public Point3D P
        {
            get { return Position; }
            set { Position = value; }
        }

        public Vector3D U
        {
            get { return Up; }
            set
            {
                Up = value;
                Right = Vector3D.CrossProduct(Up, Dir);
                Right = Vector3D.Divide(Right, Right.Length);
            }
        }

        public Vector3D D
        {
            get { return Dir; }
            set
            {
                Dir = value;
                Right = Vector3D.CrossProduct(Up, Dir);
                Right = Vector3D.Divide(Right, Right.Length);
            }
        }

        public Vector3D R
        {
            get { return Right; }
            set { Right = value; }
        }

    }
}
