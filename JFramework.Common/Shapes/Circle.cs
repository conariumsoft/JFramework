using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Common.Shapes
{
	public class Circle : Shape
	{

		public Circle(double radius, int sides) : base()
		{
			const double max = 2.0 * Math.PI;

			double step = max / sides;

			for (double theta = 0.0; theta < max; theta += step)
			{
				Vertices.Add(new Vector2((float)(radius * Math.Cos(theta)), (float)(radius * Math.Sin(theta))));
			}

			Vertices.Add(new Vector2((float)(radius * Math.Cos(0)), (float)(radius * Math.Sin(0))));
		}
	}
}
