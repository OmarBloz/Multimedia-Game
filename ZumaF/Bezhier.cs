﻿using System;
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
    public class Bezhier
    {
		public List<Point> ControlPoints;

		public float t_inc = 0.001f;

		public Color cl = Color.DarkTurquoise;
		public Color clr1 = Color.Black;
		public Color ftColor = Color.Black;




		public Bezhier()
		{
			ControlPoints = new List<Point>();
		}


		private float Factorial(int n)
		{
			float res = 1.0f;

			for (int i = 2; i <= n; i++)
				res *= i;

			return res;
		}

		private float C(int n, int i)
		{
			float res = Factorial(n) / (Factorial(i) * Factorial(n - i));
			return res;
		}

		private double Calc_B(float t, int i)
		{
			int n = ControlPoints.Count - 1;
			double res = C(n, i) *
							Math.Pow((1 - t), (n - i)) *
							Math.Pow(t, i);
			return res;
		}

		public Point GetPoint(int i)
		{
			return ControlPoints[i];
		}

		public PointF CalcCurvePointAtTime(float t)
		{
			PointF pt = new PointF();
			for (int i = 0; i < ControlPoints.Count; i++)
			{
				float B = (float)Calc_B(t, i);
				pt.X += B * ControlPoints[i].X;
				pt.Y += B * ControlPoints[i].Y;
			}

			return pt;
		}

		private void DrawControlPoints(Graphics g)
		{
			Font Ft = new Font("System", 10);
			for (int i = 0; i < ControlPoints.Count; i++)
			{
				g.FillEllipse(Brushes.Black,
								ControlPoints[i].X - 5,
								ControlPoints[i].Y - 5, 10, 10);


			}
		}

		public int isCtrlPoint(int XMouse, int YMouse)
		{
			Rectangle rc;
			for (int i = 0; i < ControlPoints.Count; i++)
			{
				rc = new Rectangle(ControlPoints[i].X - 5, ControlPoints[i].Y - 5, 10, 10);
				if (XMouse >= rc.Left && XMouse <= rc.Right && YMouse >= rc.Top && YMouse <= rc.Bottom)
				{
					return i;
				}
			}
			return -1;
		}

		public void ModifyCtrlPoint(int i, int XMouse, int YMouse)
		{
			Point p = ControlPoints[i];

			p.X = XMouse;
			p.Y = YMouse;
			ControlPoints[i] = p;
		}

		public void SetControlPoint(Point pt)
		{
			ControlPoints.Add(pt);
		}



		private void DrawCurvePoints(Graphics g)
		{
			if (ControlPoints.Count <= 0)
				return;

			PointF curvePoint;
			for (float t = 0.0f; t <= 1.0; t += t_inc)
			{
				curvePoint = CalcCurvePointAtTime(t);
				g.FillEllipse(Brushes.DarkTurquoise,
								curvePoint.X - 4, curvePoint.Y - 4,
								8, 8);
			}
		}

		public void DrawCurve(Graphics g)
		{
			DrawControlPoints(g);
			DrawCurvePoints(g);
		}


	}
}

