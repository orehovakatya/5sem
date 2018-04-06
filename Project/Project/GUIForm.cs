using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using System.IO;
using System.Threading;

namespace Project
{
    public partial class GUIForm : Form
    {
        double n = 150;

        Point3D Center = new Point3D(50,50,50);

        List<Object> Object = new List<Object>();
       
        static Camera Camera = new Camera(new Point3D(50, 50, -500), new Vector3D(1, 0, 0), new Vector3D(0, 0, 1));

        public GUIForm()
        {
            InitializeComponent();
            try
            {
                LoadModel.load(ref Object, new StreamReader(Directory.GetCurrentDirectory()+"\\Aquarium.txt"));

                if (Math.Abs(Object[Object.Count - 1].refraction - 1) < 0.0001)
                    Object[Object.Count - 1].refraction = 1.0001;

                textBox2.Enabled = true;
                textBox2.Text = Convert.ToString(Object[0].absorp);

                LoadModel.load(ref Object, new StreamReader(Directory.GetCurrentDirectory() + "\\Water.txt"));

                button15.Enabled = true;
                button1.Enabled = true;
                textBox1.Enabled = true;
                textBox1.Text = Convert.ToString(Object[Trace.IND_WATER].absorp);

                Water.FullWater(Object[Trace.IND_WATER].tops);
                textBox3.Text = Convert.ToString("0.1");
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось загрузить исходные файлы");
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                return;
            }
            
        }

        /// <summary>
        /// Нарисовать
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            max_x = 0;

            //Считывание освещённости сцены
            try
            {
                String str = textBox3.Text.Replace(".", ",");
                Trace.LIGHT_SCENE = (Convert.ToDouble(str));
                if (Trace.LIGHT_SCENE > 1)
                    throw new Exception();
            }
            catch (Exception)
            {
                MessageBox.Show("ОШИБКА: неверно задан свет сцены");
                return;
            }

            //считывание к-та аквариума, если он был изменён
                double intens;
                try
                {
                    String str = textBox2.Text.Replace(".", ",");
                    intens = (Convert.ToDouble(str));
                    if (intens > 1)
                        throw new Exception();
                    Object[0].absorp = intens;
                }
                catch (Exception)
                {
                    MessageBox.Show("ВНИМАНИЕ: Изменения не будут учтены \n Коэффициент поглощения света стенками аквариума введён неверно.");
                }

            //Считывание к-та воды, если он был изменён
                try
                {
                    String str = textBox1.Text.Replace(".", ",");
                    intens = Convert.ToDouble(Convert.ToDouble(str));
                    Object[1].absorp = intens;
                }
                catch (Exception)
                {
                    MessageBox.Show("ВНИМАНИЕ: Изменения не будут учтены \n Коэффициент поглощения света жидкостью введён неверно.");
                }

            //Считывание света из таблицы
            try
            {
                if (!Light.ReadFromGrid(dataGridView1))
                    throw new Exception();
            }
            catch (Exception)
            {
                return;
            }

            Draw_image();

        }

        //Сдвиг вверх
        private void button2_Click(object sender, EventArgs e)
        {
            Vector3D Vector;
            Camera.P = new Point3D(Camera.P.X + n*Camera.U.X, Camera.P.Y + n * Camera.U.Y, Camera.P.Z + n* Camera.U.Z);

            Vector = new Vector3D(Center.X - Camera.P.X, Center.Y - Camera.P.Y, Center.Z - Camera.P.Z);
            Vector.Normalize();

            Camera.D = Vector;

            Vector = Vector3D.CrossProduct(Camera.D, Camera.R);
            Vector.Normalize();

            Camera.U = Vector;
            button1_Click(sender, e);
        }

        //Сдвиг вправо
        private void button5_Click(object sender, EventArgs e)
        {
            Vector3D Vector;
            Camera.P = new Point3D(Camera.P.X + n * Camera.R.X, Camera.P.Y + n * Camera.R.Y, Camera.P.Z + n * Camera.R.Z);

            Vector = new Vector3D(Center.X - Camera.P.X, Center.Y - Camera.P.Y, Center.Z - Camera.P.Z);
            Vector.Normalize();

            Camera.D = Vector;

            Vector = Vector3D.CrossProduct(Camera.U, Camera.D);
            Vector.Normalize();

            Camera.R = Vector;
            button1_Click(sender, e);
        }

        /// <summary>
        /// Сдвиг влево
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            Vector3D Vector;
            Camera.P = new Point3D(Camera.P.X - n * Camera.R.X, Camera.P.Y - n * Camera.R.Y, Camera.P.Z - n * Camera.R.Z);

            Vector = new Vector3D(Center.X - Camera.P.X, Center.Y - Camera.P.Y, Center.Z - Camera.P.Z);
            Vector.Normalize();

            Camera.D = Vector;

            Vector = Vector3D.CrossProduct(Camera.U, Camera.D);
            Vector.Normalize();

            Camera.R = Vector;
            button1_Click(sender, e);
        }

        /// <summary>
        /// Масштаб -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            Camera.P= new Point3D(Camera.P.X - n * Camera.D.X, Camera.P.Y - n * Camera.D.Y, Camera.P.Z - n * Camera.D.Z);
            button1_Click(sender, e);
        }

        /// <summary>
        /// Масштаб +
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            Camera.P = new Point3D(Camera.P.X + n * Camera.D.X, Camera.P.Y + n * Camera.D.Y, Camera.P.Z + n * Camera.D.Z);
            button1_Click(sender, e);
        }

        /// <summary>
        /// Сдвиг вниз
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            Vector3D Vector;
            Camera.P = new Point3D(Camera.P.X - n * Camera.U.X, Camera.P.Y - n * Camera.U.Y, Camera.P.Z - n * Camera.U.Z);

            Vector = new Vector3D(Center.X - Camera.P.X, Center.Y - Camera.P.Y, Center.Z - Camera.P.Z);
            Vector.Normalize();

            Camera.D = Vector;

            Vector = Vector3D.CrossProduct(Camera.D, Camera.R);
            Vector.Normalize();

            Camera.U = Vector;
            button1_Click(sender, e);
        }

        /// <summary>
        /// Загрузить свет
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button12_Click(object sender, EventArgs e)
        {
            if(Light.OpenAndLoad(openFileDialog1, dataGridView1))
                button12.Enabled = false;
        }

        /// <summary>
        /// Загрузить объект
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button15_Click(object sender, EventArgs e)
        {
            if (LoadModel.OpenAndLoad(ref Object, openFileDialog1))
            {
                if (!Water.ObjectInWater(Object[Object.Count() - 1].tops))
                {
                    MessageBox.Show("Объект не будет загружен, т.к. не находится полностью в воде");
                    Object.RemoveAt(Object.Count() - 1);
                    return;
                }
                button17.Enabled = true;
            }
        }

        //Удалить последний объект
        private void button17_Click(object sender, EventArgs e)
        {
            Object.RemoveAt(Object.Count() - 1);
            if (Object.Count() <= 2)
                button17.Enabled = false;
        }

        Bitmap btm;
        Color[] table;

        public void Draw_image()
        {
            int Cw =  pictureBox1.Width;//холст
            int Ch = pictureBox1.Height;//холст
            btm = new Bitmap(Cw, Ch);
            table = new Color[Cw * Ch];
            ThreadPool.SetMaxThreads(5,5);
            for (int x = 0; x < Cw; x++)
            {
                for (int y = 0; y < Ch; y++)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(CalculatePixel),new obj(x,y,Ch,Cw));
                }

            }
        }

        int max_x = 0;

        void CalculatePixel(object N)
        {
            obj ob = (obj)N;
            Vector3D Direction = (Vector3D.Add
                            (Vector3D.Add(Camera.D, Vector3D.Multiply((ob.x / Convert.ToDouble(ob.Cw) - 0.5), Camera.R)), Vector3D.Multiply((ob.y / Convert.ToDouble(ob.Ch) - 0.5), Camera.U)));

            Color Col = Trace.TraceRay(Camera.P,  Object, Direction, 1);
            table[ob.x + ob.y * ob.Ch] = Col;


            if ((ob.x == (ob.Cw - 1)) && (ob.y == (ob.Ch - 1)))
            {
                pictureBox1.Invoke(new MethodInvoker(
                    delegate ()
                    {
                        for (int x = 0; x < ob.Cw; x++)
                        {
                            for (int y = 0; y < ob.Ch; y++)
                            {
                                btm.SetPixel(x, ob.Ch - y - 1, table[x + y * ob.Ch]);
                            }

                        }
                        pictureBox1.Image = btm;
                        pictureBox1.Refresh();
                        progressBar1.Value = 0;
                    }));

            }
            else
            {
                if (ob.x > max_x)
                {
                    max_x = ob.x;
                    progressBar1.Invoke(new MethodInvoker(
                        delegate ()
                        {
                            progressBar1.Value = (int)((ob.x / (1f * ob.Cw)) * 100);
                        }));
                }
            }
        }
    }
    class obj
    {
        public int x;
        public int y;
        public int Ch;
        public int Cw;

        public obj(int X, int Y, int CH, int CW)
        {
            x = X;
            y = Y;
            Ch = CH;
            Cw = CW;
        }
        public obj() { }
    }
}
