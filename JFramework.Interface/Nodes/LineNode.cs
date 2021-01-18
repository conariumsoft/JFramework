using JFramework.Common;
using JFramework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Interface.Nodes
{
	public class LineNode : BaseNode
	{
		public LineNode(RectFace border) : base()
		{
			AssignToBorder(border);
		}

		protected void AssignToBorder(RectFace border)
		{
			if (border == RectFace.Top)
			{
				PointA = UICoords.FromScale(0, 0);
				PointB = UICoords.FromScale(1, 0);
			}
			if (border == RectFace.Bottom)
			{
				PointA = UICoords.FromScale(0, 1);
				PointB = UICoords.FromScale(1, 1);
			}
			if (border == RectFace.Left)
			{
				PointA = UICoords.FromScale(0, 0);
				PointB = UICoords.FromScale(0, 1);
			}
			if (border == RectFace.Right)
			{
				PointA = UICoords.FromScale(1, 0);
				PointB = UICoords.FromScale(1, 1);
			}
		}

		public RectFace Border { set => AssignToBorder(value); }

		public Color Color { get; set; }
		public float Thickness { get; set; }

		public UICoords PointA { get; set; }

		public UICoords PointB { get; set; }


		public Vector2 AbsolutePointA => Parent.AbsolutePosition + PointA.Pixels + (Parent.AbsoluteSize * PointA.Scale);
		public Vector2 AbsolutePointB => Parent.AbsolutePosition + PointB.Pixels + (Parent.AbsoluteSize * PointB.Scale);

		public override void Draw(GraphicsEngine GFX)
		{
			GFX.Line(Color, AbsolutePointA, AbsolutePointB, Thickness);
			base.Draw(GFX);
		}

		public override void Update(GameTime gt)
		{

			base.Update(gt);
		}

	}
}
