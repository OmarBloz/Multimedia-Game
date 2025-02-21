using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Zuma
{
    public class Frog
    {
        public  Bitmap img= new Bitmap("frog.png");
        public float Theta;
        public float x, y;
        Ball B1 = new Ball();
        Ball B2=new Ball();
        public Frog(int x,int y)
        {
            this.x = x;
            this.y = y;
            B1.x = x;
            B1.y = y + img.Height / 2;
            B2.x = x;
            B2.y= y - img.Height / 2;
        }

        public Ball Change()
        {
            Ball B3 = new Ball();
            B3.x = B2.x;
            B3.y = B2.y;
            Ball M = B1;
            B2.x = B1.x;
            B2.y = B1.y;
            B1 = B2;
            B2 = B3;

           
            return M;
        }

        public void Draw(Graphics g)
        {
            
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            Graphics g2 = Graphics.FromImage(bmp);
            g2.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);
            g2.RotateTransform((float)(Theta * 180 / Math.PI) - 90);
            g2.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);
            g2.DrawImage(img, 0, 0, img.Width, img.Height);
            g.DrawImage(bmp, x - img.Width / 2, y - img.Height / 2, img.Width, img.Height);

            //////////////////////////////
            bmp = new Bitmap(B1.img.Width, B1.img.Height);
            g2 = Graphics.FromImage(bmp);
            g2.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);
            g2.RotateTransform((float)(Theta * 180 / Math.PI - 90));
            g2.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);
            g2.DrawImage(B1.img,0,0,B1.img.Width,B1.img.Height);
            float x1= (float)(B1.img.Width*Math.Cos(Theta) + x), y1= (float)(B1.img.Width * Math.Sin(Theta)+y);
            g.DrawImage(bmp, x1-B1.img.Width/2, y1-B1.img.Width/2 , B1.img.Width, B1.img.Height);
            ////////////////////////////////////////////
            ///
            bmp = new Bitmap(B2.img.Width/2, B2.img.Height/2);
            g2 = Graphics.FromImage(bmp);
            g2.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);
            g2.RotateTransform((float)(Theta * 180 / Math.PI - 90));
            g2.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);
            g2.DrawImage(B2.img, 0, 0, B2.img.Width/2, B2.img.Height/2);
            float x2 = (float)(((-B2.img.Width)) * Math.Cos(Theta) + x), y2 = (float)(((-B2.img.Width)+3)* Math.Sin(Theta) + y);
            g.DrawImage(bmp, x2-B2.img.Width/4, y2-B2.img.Width/4, B2.img.Width/2, B2.img.Height/2);


         

        }
    }
}
