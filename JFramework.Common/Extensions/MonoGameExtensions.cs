﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Common.Extensions
{
	public static class MonoGameExtensions
	{
		public static Vector2 RoundTo(this Vector2 og, int decimalplaces)
		{
			return new Vector2((float)Math.Round(og.X, decimalplaces), (float)Math.Round(og.Y, decimalplaces));
		}

		public static Vector2 GetY(this Vector2 vec) => new Vector2(0, vec.Y);

		public static Vector2 GetX(this Vector2 vec) => new Vector2(vec.X, 0);

		public static Vector2 LookAt(this Vector2 origin, Vector2 goal) => (goal - origin).Unit();

		public static Vector2 Unit(this Vector2 vector)
		{
			Vector2 copy = vector;
			copy.Normalize();
			return copy;
		}
		public static float Distance(this Vector2 a, Vector2 b) => (a - b).Length();

		public static Color Inverse(this Color color)
		{
			int r = 0xFFFFFF - color.R;
			int g = 0xFFFFFF - color.G;
			int b = 0xFFFFFF - color.B;
			return new Color(r, g, b);
		}

		public static float GetTextWidth(this string text, SpriteFont font)
		{
			return font.MeasureString(text).X;
		}


		public static float GetDelta(this GameTime gt)
		{
			return (float)gt.ElapsedGameTime.TotalSeconds;
		}

		public static Vector2 Lerp(this Vector2 a, Vector2 b, float alpha)
		{
			return new Vector2(
				MathematicsExtensions.Lerp(a.X, b.X, alpha),
				MathematicsExtensions.Lerp(a.Y, b.Y, alpha)
			);
		}

		public static Color Sub(this Color a, Color b)
		{
			return new Color(a.R - b.R, a.G - b.G, a.B - b.B);
		}

		public static Color Add(this Color a, Color b)
		{
			return new Color(a.R + b.R, a.G + b.G, a.B + b.B);
		}
	}
}
