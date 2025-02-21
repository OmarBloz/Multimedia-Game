using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zuma
{
   public class Ball
    {
        public Bitmap img;
        public static Random R = new Random();
        public string[] S = new string[] { "blue.png", "green.png", "yellow.png", "grey.png", "red.png", "pink.png" };
        public float x, y;
        public Point start;
        public Point end;
        public int c;
        public Ball()
        {
            c = R.Next(0, 6);
          img = new Bitmap(S[c]);
        }

        public void Draw(Graphics g)
        {
            img.MakeTransparent(img.GetPixel(0, 0));
            g.DrawImage(img, x, y);

        }

        public void Draw(Graphics g,int Width,int Height)
        {
            img.MakeTransparent(img.GetPixel(0, 0));
            g.DrawImage(img, x , y, Width, Height);
        }

        public void Move()
        {
            var M = this;
            float Speed = 15;
            float xs = start.X, xe = end.X;
            float ys = start.Y, ye = end.Y;
            float dx = xe - xs;
            float dy = ye - ys;
            float m = dy / dx;

            if (Math.Abs(dx) > Math.Abs(dy))
            {
                if (xs < xe && ys < ye)
                {
                    M.x += Speed;
                    M.y += m * Speed;
                    if (M.x >= xe)
                    {
                       
                    }
                }
                else if (xs < xe && ys > ye)
                {
                    M.x += Speed;
                    M.y += m * Speed;
                    if (M.x >= xe)
                    {

                    }
                }
                else if (xs > xe && ys < ye)
                {
                    M.x -= Speed;
                    M.y -= m * Speed;
                    if (M.x <= xe)
                    {
                       
                    }

                }

                else
                {
                    M.x -= Speed;
                    M.y -= m * Speed;
                    if (M.x <= xe)
                    {
                       
                    }
                }
            }
            else
            {
                if (xs < xe && ys < ye)
                {
                    M.y += Speed;
                    M.x += 1 / m * Speed;
                    if (M.y >= ye)
                    {
                    
                    }

                }
                else if (xs < xe && ys > ye)
                {
                    M.y -= Speed;
                    M.x -= 1 / m * Speed;
                    if (M.y <= ye)
                    {
                       
                    }

                }
                else if (xs > xe && ys < ye)
                {
                    M.y += Speed;
                    M.x += 1 / m * Speed;
                    if (M.y >= ye)
                    {
                      


                    }
                }

                else
                {
                    M.y -= Speed;
                    M.x -= 1 / m * Speed;
                    if (M.y <= ye)
                    {
                     

                    }
                }
            }

        }
    }
}
