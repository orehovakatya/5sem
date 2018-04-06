using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Media3D;

namespace Project
{
    class Trace
    {

        private static double INf = 1E+10;
        private static double FISH = 99;
        private static double EPS_NULL = 1E-5;
        public static double LIGHT_SCENE = 0.1;
        public static int IND_WATER = 1;
        static private double eps = 0.001;
        static private int MAX_DEPTH = 10;
        static public List<Light> Light = new List<Project.Light>();
        
        /// <summary>
        /// Поиск ближайшего пересечения
        /// </summary>
        /// <param name="clos_obj">Ближайший объект</param>
        /// <param name="clos_tr">Ближайший треугольник</param>
        /// <param name="clos_P">Точка пересечения</param>
        /// <param name="O">Точка начала луча</param>
        /// <param name="Object">Список объектов</param>
        /// <param name="D">Направление поискового вектора</param>
        /// <param name="d">Минимальное расстояние до объекта(расстояние рамки)</param>
        static private bool ClosestIntersection(ref int index, ref Triangle clos_tr, ref Point3D clos_P, Point3D O, List<Object> Object, Vector3D D, double closest_t, double d)
        {
            int id = -1;
            for(int i =0; i<Object.Count();i++)
            {
                Point3D P = new Point3D();
                Object obj = Object[i];
                foreach (Triangle tr in obj.triangle)
                {
                    double t = IntersectRayTriangle(D, obj.tops[tr.A], obj.tops[tr.B], obj.tops[tr.C], O, tr.Normal, ref P, d);
                    if ((t > d) && (t < closest_t))
                    {
                        closest_t = t;
                        id = i;
                        clos_tr = tr;
                        clos_P = P;
                    }
                }
            }
            if (id == -1)
                return false;
            index = id;
            return true;
        }



        /// <summary>
        /// Трассировка луча
        /// </summary>
        /// <param name="O">Точка начала луча</param>
        /// <param name="Object">Список объектов</param>
        /// <param name="D">Направление луча</param>
        /// <param name="d">Расстояние до рамки</param>
        /// <param name="depth">Глубина рекурсии</param>
        /// <param name="n1">Текущий к-т преломления</param>
        /// <returns></returns>
        static public Color TraceRay(Point3D O, List<Object> Object, Vector3D D, double d)
        {
            List<MyStack> stack = new List<MyStack>();

            Triangle clos_tr = null;
            Point3D clos_P = new Point3D();
            Point3D begin = O;
            int depth = 0;
            List<double> refraction = new List<double>();
            refraction.Add(1.0);
            double n1_n2;

            double intense = LIGHT_SCENE;

            int ind = -1;

            //Поиск всех точек пересечения
            while (depth < MAX_DEPTH)
            {
                if (ClosestIntersection(ref ind, ref clos_tr, ref clos_P, begin, Object, D, INf, d))
                {
                    //Если есть пересечение
                    Vector3D V = D;
                    stack.Add(new MyStack(clos_P,ind, Object[ind].absorp,clos_tr.Normal));
                    if (Object[ind].absorp > FISH)
                        break;

                    if (refraction[refraction.Count -1] == Object[ind].refraction)
                    {
                        n1_n2 = Object[ind].refraction /(1f* refraction[refraction.Count - 2]);
                    }
                    else
                    {
                        n1_n2 = refraction[refraction.Count - 1] / (1f * Object[ind].refraction);
                    }
                    //Вычисляем приломлённый луч.

                    if (Refracted_Ray(ref D, n1_n2, V, clos_tr.Normal))
                    {
                        if (refraction[refraction.Count - 1] == Object[ind].refraction)
                            refraction.RemoveAt(refraction.Count - 1);
                        else
                            refraction.Add(Object[ind].refraction);

                    }
                    else
                    {
                        break;
                        //D = V;
                        //Reflected_Ray(ref D, V, clos_tr.Normal);
                    }
                        //D = V;
                        //Reflected_Ray(ref D, V, clos_tr.Normal);
                    depth++;
                    begin = clos_P;
                    d = EPS_NULL;
                }
                else
                    break;
            }

            if (stack.Count() < 1)
                return Color.Black;

            //Вычисление интенсивности
            stack.Reverse();
            List<MyStack> stack_for_light = new List<MyStack>();

            List<double> list_intensity = new List<double>();

            for (int k = 0; k < stack.Count(); k++)
            {
                MyStack i = stack[k];
                double al_intens = LIGHT_SCENE;
                foreach(Light lamp in Light)
                {
                    double intensity = lamp.intensity;
                    stack_for_light.Add(new MyStack(lamp.point, IND_WATER, Object[IND_WATER].absorp,i.Normal));
                    begin = lamp.point;
                    ind = -1;
                    //поиск пересечения от источника света
                    D = new Vector3D(i.P.X - begin.X, i.P.Y - begin.Y, i.P.Z - begin.Z);
                    while (D.Length > eps)
                    {
                        
                        if (ClosestIntersection(ref ind, ref clos_tr, ref clos_P, begin, Object, D, 1-eps, eps))
                        {
                            //Если врезались в рыбку
                            if (Object[ind].absorp > FISH)
                            {
                                /*intensity = 0.0;
                                stack_for_light.Clear();
                                break;*/
                                stack_for_light.Add(new MyStack(i.P, i.Index, IND_WATER, i.Normal));
                                break;
                            }
                            else
                            {
                                //Если врезались в какую-то среду то добавляем в стек
                                stack_for_light.Add(new MyStack(clos_P, ind, Object[ind].absorp, clos_tr.Normal));
                                begin = clos_P;
                                D = new Vector3D(i.P.X - begin.X, i.P.Y - begin.Y, i.P.Z - begin.Z);
                            }
                        }
                        else
                        {
                            if (ind == -1)
                                stack_for_light.Add(new MyStack(i.P, i.Index, IND_WATER, i.Normal));
                            //если пересечения не произошло
                            //между водой и пересечением нет объектов (т.е. пересечение-вода или пересечение - рыбка)

                            break;
                        }
                    }


                    if (stack_for_light.Count() > 0)
                    {
                        if ((stack_for_light[stack_for_light.Count() - 1].Absorb == i.Absorb)&&(stack_for_light[stack_for_light.Count() - 1].Index == i.Index))
                            stack_for_light.Add(i);
                        for (int item = 0; item < stack_for_light.Count(); item += 2)
                        {
                            Point3D A = stack_for_light[item].P;
                            Point3D B = stack_for_light[item + 1].P;
                            double x = new Vector3D(B.X - A.X, B.Y - A.Y, B.Z - A.Z).Length;
                            intensity = intensity * Math.Exp(-x * stack_for_light[item].Absorb);
                        }
                    }
                    D = new Vector3D(i.P.X - lamp.point.X, i.P.Y - lamp.point.Y, i.P.Z - lamp.point.Z);
                    D.Normalize();
                    if (Vector3D.DotProduct(D, i.Normal) < 0)
                        D.Negate();
                    al_intens += intensity*(Vector3D.DotProduct(D,i.Normal));
                    stack_for_light.Clear();
                }
                list_intensity.Add(al_intens);  
            }


            List<ColorList> color_table = new List<ColorList>();
            Color back_color = Color.Black;

            if (stack[0].Absorb > FISH)
            {
                color_table.Add(new ColorList(Object[stack[0].Index].color,list_intensity[0]));
                back_color = Object[stack[1].Index].color;
            }
            else
            {
                color_table.Add(new ColorList(back_color, list_intensity[0]));
                back_color = Object[stack[0].Index].color;
            }

            double lamda;
            for (int i = 1; i<stack.Count();i+=2)
            {
                Point3D A = stack[i].P;
                Point3D B = stack[i - 1].P;
                lamda = Math.Exp(-stack[i].Absorb*(new Vector3D(A.X-B.X, A.Y - B.Y, A.Z - B.Z).Length));
                color_table.Add(new ColorList(mix(color_table[color_table.Count()-1].Color,back_color,lamda,1), color_table[color_table.Count() - 1].Intensity*lamda+list_intensity[i]));
                if (i < stack.Count()-1)
                    back_color = Object[stack[i+1].Index].color;
            }

            color_table.Reverse();
            intense = color_table[0].Intensity;
            back_color = color_table[0].Color;
            if (intense > 1)
                intense = 1.0;
            return Color.FromArgb(Convert.ToInt32(back_color.R*intense), Convert.ToInt32(back_color.G*intense), Convert.ToInt32(back_color.B*intense));
        }

        /// <summary>
        /// Вычисление приломлённого луча
        /// </summary>
        /// <param name="R">Приломлённый луч</param>
        /// <param name="n1_n2">Относительный к-т приломления</param>
        /// <param name="V">Падающий луч</param>
        /// <param name="N">Нормаль в точке падения</param>
        /// <returns>true-есть приломлённый луч; false - приломлённого луча нет</returns>
        static private bool Refracted_Ray(ref Vector3D R, double n1_n2, Vector3D V, Vector3D N)
        {
            double a = Math.Pow(V.Length, 2) - Math.Pow(n1_n2, 2) * Math.Pow(Vector3D.CrossProduct(V, N).Length, 2);
            if (a < -0.01)
                return false;
            R = Vector3D.Multiply(Math.Pow(n1_n2, 2), V) + Vector3D.Multiply(
                n1_n2 * (Math.Sign(Vector3D.DotProduct(V, N)) * Math.Sqrt(Math.Pow(V.Length, 2) - Math.Pow(n1_n2, 2) * Math.Pow(Vector3D.CrossProduct(V, N).Length, 2)) -
                n1_n2 * Vector3D.DotProduct(V, N)), N);
            R.Normalize();
            return true;
        }

        /// <summary>
        /// Вычисление отражённого луча
        /// </summary>
        /// <param name="M">Отраженный луч</param>
        /// <param name="V">Падающий луч</param>
        /// <param name="Normal">Тщрмаль в точке падения</param>
        static void Reflected_Ray(ref Vector3D M, Vector3D V, Vector3D Normal)
        {
            M = V - Vector3D.Multiply(2, Vector3D.Multiply(Vector3D.DotProduct(V, Normal), Normal));
            M.Normalize();
        }

        /// <summary>
        /// Наложение одного цвета на второй
        /// </summary>
        /// <param name="col1">Первый цвет</param>
        /// <param name="col2">Накладывается на второй</param>
        /// <param name="alpha1">Прозрачность первого цвета</param>
        /// <param name="alpha2">Прозрачность второго цвета</param>
        /// <returns>Итоговый цвет</returns>
        static Color mix(Color col1, Color col2, double alpha1, double alpha2)
        {
            int out_a =Convert.ToInt32(alpha1 + alpha2 * (1 - alpha1));
            if (out_a == 0)
                return Color.FromArgb(out_a, 0, 0, 0);
            int R =Convert.ToInt32((col1.R*alpha1+col2.R* alpha2 * (1- alpha1))/out_a);
            int G =Convert.ToInt32((col1.G * alpha1 + col2.G * alpha2 * (1 - alpha1)) / out_a);
            int B =Convert.ToInt32((col1.B * alpha1 + col2.B * alpha2 * (1 - alpha1)) / out_a);
            return Color.FromArgb(R,G,B);
        }

        /// <summary>
        /// Поиск пересечения луча с треугольником
        /// </summary>
        /// <param name="O">Точка начала луча</param>
        /// <param name="D">Вектор направления луча</param>
        /// <param name="triangl"></param>
        /// <returns>Коэффициент t</returns>
        /// 
        static private double IntersectRayTriangle(Vector3D D, Point3D A, Point3D B, Point3D C, Point3D O,Vector3D normal, ref Point3D P, double d)
        {
            double t =IntersectRayPlane(O,A,D,normal);
            if ((t < d)||(t<=0))
                return -1;
            //Р - точка пересечения луча и плоскости
            P = new Point3D(O.X+t * D.X,O.Y+ t * D.Y,O.Z+ t * D.Z);

            Vector3D AP = new Vector3D(P.X-A.X, P.Y - A.Y, P.Z - A.Z);
            Vector3D AC = new Vector3D(C.X - A.X, C.Y - A.Y, C.Z - A.Z);
            Vector3D AB = new Vector3D(B.X - A.X, B.Y - A.Y, B.Z - A.Z);
            Vector3D BC = new Vector3D(C.X - B.X, C.Y - B.Y, C.Z - B.Z);
            Vector3D BP = new Vector3D(P.X - B.X, P.Y - B.Y, P.Z - B.Z);

            double ABC = (Vector3D.CrossProduct(AB, AC)).Length;
            double ABP = (Vector3D.CrossProduct(AB, AP)).Length;
            double BPC = (Vector3D.CrossProduct(BC, BP)).Length;
            double APC = (Vector3D.CrossProduct(AP, AC)).Length;

            if (Math.Abs(ABC - ABP - BPC - APC) > eps)
                t = -1;
            return t;
        }

        /// <summary>
        /// Пересечение луча и плоскости
        /// </summary>
        /// <param name="Camera">Камера</param>
        /// <param name="A">Точка треугольника</param>
        /// <param name="D">Направляющий вектор</param>
        /// <param name="normal">Вектор нормали треугольника</param>
        /// <returns>к-т t</returns>
        static private double IntersectRayPlane(Point3D O, Point3D A, Vector3D D,Vector3D normal)
        {
            Vector3D a = new Vector3D(A.X, A.Y, A.Z);
            Vector3D b = new Vector3D(O.X, O.Y, O.Z);
            double t = (Vector3D.DotProduct(normal, a) - Vector3D.DotProduct(normal, b)) / Vector3D.DotProduct(normal, D);
            return t;
        }
    }
}
