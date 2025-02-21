using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zuma
{
    public class CurveBall
    {
        public Bitmap B;
        public static Random R = new Random();
        public string[] S = new string[] {"blue.png", "green.png", "yellow.png", "grey.png", "red.png", "pink.png"};
        public float t;
        public float x, y;
        public int c;

       public CurveBall()
        {
            c = R.Next(0, 6);
            this.B = new Bitmap(S[c]);
            

        }

        /*public void Create(float x,float y)
        {
            CurveBall CB= new CurveBall();
            CB. = x;
            CB. = y;
            List.Add(CurveBall);

        }*/
        public void Draw(Graphics g)
        {

            g.DrawImage(B, x-10, y-10,B.Width,B.Height);

        }
        public void Move(Bezhier curve, List<CurveBall> CB, int i)
        {
            if (i < 2)
            {
                t += curve.t_inc;
                var Pt = curve.CalcCurvePointAtTime(t);
                x = Pt.X;
                y = Pt.Y;
            }
            else
            {
                if (CB[i] != null && CB[i - 1] != null)
                {
                    var dx = CB[i].x - CB[i - 1].x;
                    var dy = CB[i].y - CB[i - 1].y;

                    var d = Math.Sqrt(dy * dy + dx * dx);

                    if (d > CB[i].B.Width)
                    {
                        t += curve.t_inc;
                        var Pt = curve.CalcCurvePointAtTime(t);
                        x = Pt.X;
                        y = Pt.Y;
                    }
                }
            }
        }
    }
}
