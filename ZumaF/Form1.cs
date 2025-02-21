using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zuma
{
    public partial class Form1 : Form
    {
        Timer timer = new Timer();
        Bitmap off;
        Bezhier obj = new Bezhier();
        List<Ball> balls = new List<Ball>();
        public Frog F;
        public Ball M;
        int k = 0;
        Ball B;
        static List<CurveBall> List = new List<CurveBall>();
        int flag1 = 0;
        int ct = 0;
        Bitmap inter = new Bitmap("interface.png");
        Bitmap map = new Bitmap("map.png");

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            timer.Tick += Timer_Tick;
            timer.Start();

        }

        private void Create()
        {
            CurveBall CB = new CurveBall();
            CB.x = obj.ControlPoints[0].X;
            CB.y = obj.ControlPoints[0].Y;
            //CB.t -= 0.01;
            List.Add(CB);
        }

        public void Timer_Tick(object sender, EventArgs e)
        {
            if (List.Count < 20)
            {
                if (List.Count == 0) 
                    Create();
                else
                {
                    var last = List[List.Count - 1];
                    var dx = last.x - obj.ControlPoints[0].X;
                    var dy = last.y - obj.ControlPoints[0].Y;
                    var d = Math.Sqrt(dx * dx + dy * dy);

                    if (d > last.B.Width)
                    {
                        Create();
                    }
                }
            }
            for (int i = 0; i < balls.Count; i++)
            {
                balls[i].Move();
            }
            CheckColli();
            for (int i = 0; i < List.Count; i++)
            {
                List[i].Move(obj, List, i);
            }
            DrawBuff(this.CreateGraphics());
        }
        public void Explosion(int j)
        {
            int Check = 1;
            int Cat = 0;
            int Dog = 0;
            for(int h=j+1;h<List.Count;h++)
            {

                if (List[j].c== List[h].c)
                {
                    Check++;

                }
                else
                {
                    Cat = h;
                    break;
                }
               


            }
            for (int h = j-1; h>= 0; h--)
            {

                if (List[j].c == List[h].c)
                {

                    Check++;
                }
                else
                {
                    Dog = h+1;
                    break;
                }
            }

            if (Check >= 3)
            {
                for(int Delete =0;Delete<Check;Delete++)
                {
                    List.RemoveAt(Dog);
                }
                try
                {
                    while (true)
                    {

                        var dx2 = List[Dog].x - List[Dog + 1].x;
                        var dy2 = List[Dog].y - List[Dog + 1].y;
                        var d2 = Math.Sqrt(dy2 * dy2 + dx2 * dx2);
                        if (d2 <= List[Dog].B.Width)
                        {
                            break;
                        }
                        for (int k = Dog; k >= 0; k--)
                        {
                            List[k].t -= 0.005f;
                            var point = obj.CalcCurvePointAtTime(List[k].t);
                            List[k].x = point.X;
                            List[k].y = point.Y;
                        }
                    }
                    Explosion(Dog);
                }
                catch (Exception) { }
               

            }

        }
        private void CheckColli()
        {

            for(int i = 0 ; i < balls.Count ; i++)
            {
                for(int j=0 ; j < List.Count ; j++)
                {
                    var dx = balls[i].x - List[j].x;
                    var dy = balls[i].y - List[j].y;
                    var d = Math.Sqrt(dy * dy + dx * dx);
                    if (d <= balls[i].img.Width)
                    {
                        //MessageBox.Show(j.ToString());
                        CurveBall CB = new CurveBall();
                        CB.x = List[j].x;
                        CB.y = List[j].y;
                        CB.c = balls[i].c;
                        CB.B=balls[i].img;
                        CB.t = List[j].t;
                    
                        List.Insert(j, CB);
                        balls.RemoveAt(i);
                        Explosion(j);
                        return;
                    }
                }
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            F.Theta = (float)Math.Atan2(e.Y - F.y, e.X - F.x);
            DrawBuff(this.CreateGraphics());
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                M = F.Change();
                M.x = F.x - M.img.Width / 2;
                M.y = F.y - M.img.Height / 2;
                M.start = new Point((int)(F.x), (int)(F.y));
                M.end = new Point(e.X, e.Y);
                M.Move(); M.Move(); M.Move();
                balls.Add(M);
                //playSimpleSound(flag1=1);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawBuff(this.CreateGraphics());
        }

        private void playSimpleSound(int flag)
        {
            /*if (flag == 1)
            {
                SoundPlayer simpleSound = new SoundPlayer("Shoot.wav");
                simpleSound.Play();
            }*/
            flag = 0;
            if(flag == 0)
            {
                SoundPlayer simpleSound = new SoundPlayer("Main.wav");
                simpleSound.Play();
            }
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
           off= new Bitmap(ClientSize.Width, ClientSize.Height);
            F = new Frog(Width / 2, Height / 6);
            obj.SetControlPoint(new Point(677, 166));
            obj.SetControlPoint(new Point(685, 749));
            obj.SetControlPoint(new Point(1262, 877));
            obj.SetControlPoint(new Point(1563, 386));
            obj.SetControlPoint(new Point(1562, 172));
           // playSimpleSound(flag1);
        }

        private void DrawScene(Graphics g)
        {
            g.Clear(Color.White);
            g.DrawImage(map, 0, 0, Width, Height);
            g.DrawImage(inter, 0, 0, Width, Height);
            F.Draw(g);
            obj.DrawCurve(g);
            for(int i = 0; i < List.Count; i++)
            {
                List[i].Draw(g);
            }
            //g.DrawLine(Pens.DarkGreen,  F.x, F.y , F.x, F.y + F.Height + 100);
            for (int i = 0; i < balls.Count; i++)
            {
                balls[i].Draw(g);
            }
            g.FillEllipse(Brushes.Red, F.x , F.y, 10, 10);
        }

        private void DrawBuff(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }

    }
}
