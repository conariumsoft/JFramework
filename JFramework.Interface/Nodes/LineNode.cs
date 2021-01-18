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

		private Vector2 GetAbsolutePointA()
		{
			if (Parent is IRectNode rect)
				return rect.AbsolutePosition + PointA.Pixels + (rect.AbsoluteSize * PointA.Scale);
			return Vector2.Zero;
		}
		private Vector2 GetAbsolutePointB()
		{
			if (Parent is IRectNode rect)
				return rect.AbsolutePosition + PointB.Pixels + (rect.AbsoluteSize * PointB.Scale);
			return Vector2.Zero;
		}

		public Vector2 AbsolutePointA => GetAbsolutePointA();
		public Vector2 AbsolutePointB => GetAbsolutePointB();

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
