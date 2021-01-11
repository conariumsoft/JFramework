using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Graphics
{
	using Polygon = List<Vector2>;
	using 

	public class Shape
	{
		public List<Vector2> Vertices { get; protected set; }



		public Shape()
		{
			Vertices = new List<Vector2>();
		}
	}

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


	public static class ShapeCache
	{
		private static Dictionary<string, Polygon> circleCache = new Dictionary<string, Polygon>();

		public static Polygon GetArc(float radius, int sides, float startingAngle, float radians)
		{
			List<Vector2> points = new List<Vector2>();

			points.AddRange(GetCircle(radius, sides));
			points.RemoveAt(points.Count - 1);

			double curAngle = 0.0;
			double anglePerSide = MathHelper.TwoPi / sides;

			while ((curAngle + (anglePerSide / 2.0)) < startingAngle)
			{
				curAngle += anglePerSide;

				points.Add(points[0]);
				points.RemoveAt(0);
			}

			points.Add(points[0]);
			int sidesInArc = (int)((radians / anglePerSide) + 0.5);

			points.RemoveRange(sidesInArc + 1, points.Count - sidesInArc - 1);

			return points;
		}

		public static List<Vector2> GetCircle(double radius, int sides)
		{
			String circleKey = radius + "x" + sides;

			if (circleCache.ContainsKey(circleKey))
				return circleCache[circleKey];

			List<Vector2> circleDef = new List<Vector2>();

			const double max = 2.0 * Math.PI;

			double step = max / sides;

			for (double theta = 0.0; theta < max; theta += step)
			{
				circleDef.Add(new Vector2((float)(radius * Math.Cos(theta)), (float)(radius * Math.Sin(theta))));
			}

			circleDef.Add(new Vector2((float)(radius * Math.Cos(0)), (float)(radius * Math.Sin(0))));

			circleCache.Add(circleKey, circleDef);

			return circleDef;
		}
	}
}
