using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Common.Shapes
{
	public class Shape
	{
		public List<Vector2> Vertices { get; protected set; }


		public Shape()
		{
			Vertices = new List<Vector2>();
		}

		public Shape(IEnumerable<Vector2> vertices)
		{

		}
	}
}
