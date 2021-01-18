using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Common.Shapes
{
	public class Arc : Shape
	{
		public Arc(float radius, int sides, float startingAngle, float radians) : base()
		{

			Vertices.AddRange(new Circle(radius, sides).Vertices);
			Vertices.RemoveAt(Vertices.Count - 1);

			double curAngle = 0.0;
			double anglePerSide = MathHelper.TwoPi / sides;

			while ((curAngle + (anglePerSide / 2.0)) < startingAngle)
			{
				curAngle += anglePerSide;

				Vertices.Add(Vertices[0]);
				Vertices.RemoveAt(0);
			}

			Vertices.Add(Vertices[0]);
			int sidesInArc = (int)((radians / anglePerSide) + 0.5);

			Vertices.RemoveRange(sidesInArc + 1, Vertices.Count - sidesInArc - 1);
		}

		public void Draw()
		{

		}
	}
}
