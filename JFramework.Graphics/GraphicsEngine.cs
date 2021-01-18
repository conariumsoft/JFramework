using JFramework.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Graphics
{
	public class GraphicsEngine : IGraphicsLayer
	{

		public SpriteFont DefaultFont { get; set; }
		public static GraphicsEngine Instance { get; private set; }

		public Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();

		struct TextureDef
		{
			public string Path { get; set; }
			public string Name { get; set; }
			public TextureDef(string name, string path)
			{
				Name = name;
				Path = path;
			}
		}


		private Texture2D pixel;


		public void Initialize()
		{
			SpriteBatch = new SpriteBatch(GraphicsDevice);

			// create pixel
			pixel = new Texture2D(GraphicsDevice, 1, 1);
			pixel.SetData<Color>(new Color[] { Color.White });

		}


		public Vector2 WindowSize => Game.Window.ClientBounds.Size.ToVector2();
		public SpriteSortMode SpriteSortMode { get; set; }
		public BlendState BlendState { get; set; }
		public SamplerState SamplerState { get; set; }
		public DepthStencilState DepthStencilState { get; set; }
		public RasterizerState RasterizerState { get; set; }
		public Effect Shader { get; set; }
		public Matrix Matrix { get; set; }

		public Game Game { get; private set; }
		public ContentManager ContentManager { get; set; }
		public SpriteBatch SpriteBatch { get; set; }
		public GraphicsDevice GraphicsDevice { get; set; }
		public GraphicsDeviceManager GraphicsDeviceManager { get; set; }

		public bool ContentLoaded { get; private set; }

		public float GraphicsTimer { get; set; }

		public float LoadingDelay { get; set; }

		public GraphicsEngine(Game game)
		{
			Game = game;
			Instance = this;
			LoadingDelay = 0.03f;
		}


		public void Update(GameTime gt)
		{

		}

		public void Clear(Color color) => GraphicsDevice.Clear(color);
		public void End() => SpriteBatch.End();
		public void Begin(SpriteSortMode sorting = SpriteSortMode.Deferred, BlendState blending = null, SamplerState sampling = null, DepthStencilState depthStencil = null,
			RasterizerState rasterizing = null, Effect effect = null, Matrix? transform = null)
		{
			SpriteBatch.Begin(sorting, blending, sampling, depthStencil, rasterizing, effect, transform);
		}
		public void Arc(Color color, Vector2 center, float radius, int sides, Rotation startingAngle, Rotation radians, float thickness = 1)
		{
			List<Vector2> arc = ShapeCache.GetArc(radius, sides, startingAngle.Radians, radians.Radians);
			Polygon(color, center, arc, thickness);
		}
		public void Circle(Color color, Vector2 position, double radius, int sides = 12, float thickness = 1)
		{
			List<Vector2> c = ShapeCache.GetCircle(radius, sides);
			Polygon(color, position, c, thickness);
		}
		public void Line(Color color, Vector2 point, float length, Rotation angle, float thickness = 1)
		{
			Vector2 origin = new Vector2(0f, 0.5f);
			Vector2 scale = new Vector2(length, thickness);
			SpriteBatch.Draw(pixel, point, null, color, angle.Radians, origin, scale, SpriteEffects.None, 0);
		}
		public void Line(Color color, Vector2 point1, Vector2 point2, float thickness = 1)
		{
			float distance = Vector2.Distance(point1, point2);
			float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);

			float expanded = (float)Math.Floor(angle * Math.PI);
			float backDown = expanded / (float)Math.PI;

			Line(color, point1, distance, Rotation.FromRad(angle), thickness);
		}
		public void OutlineRect(Color color, Vector2 position, Vector2 size, float thickness = 2.0f)
		{
			Line(color, position, position + new Vector2(0, size.Y), thickness);
			Line(color, position, position + new Vector2(size.X, 0), thickness);
			Line(color, position + new Vector2(size.X, 0), position + size, thickness);
			Line(color, position + new Vector2(0, size.Y), position + new Vector2(size.X, size.Y), thickness);
		}
		public void Polygon(Color color, Vector2 position, List<Vector2> points, float thickness = 1)
		{
			if (points.Count < 2)
				return;

			for (int i = 1; i < points.Count; i++)
				Line(color, points[i - 1] + position, points[i] + position, thickness);
		}
		public void Polygon(Color color, List<Vector2> points, float thickness = 1)
		{
			if (points.Count < 2)
				return;

			for (int i = 1; i < points.Count; i++)
				Line(color, points[i - 1], points[i], thickness);
		}
		public void Rect(Color color, Vector2 position, Vector2 size) => Rect(color, position, size, Rotation.Zero);
		public void Rect(Color color, Vector2 position, Vector2 size, Rotation rotation)
		{
			SpriteBatch.Draw(
				pixel,
				new Rectangle(position.ToPoint(), size.ToPoint()),
				null,
				color, rotation.Degrees, new Vector2(0, 0), SpriteEffects.None, 0
			);
		}
		public void Sprite(Texture2D texture, Vector2 position) => Sprite(texture, position, Color.White);
		public void Sprite(Texture2D texture, Vector2 position, Color color) => SpriteBatch.Draw(texture, position, color);
		public void Sprite(Texture2D texture, Vector2 position, Rectangle? quad, Color color) => SpriteBatch.Draw(texture, position, quad, color);
		public void Sprite(Texture2D texture, Vector2 position, Rectangle? quad, Color color, Rotation rotation, Vector2 origin, Vector2 scale, SpriteEffects efx, float layer) =>
			SpriteBatch.Draw(texture, position, quad, color, rotation.Radians, origin, scale, efx, layer);
		public void Sprite(Texture2D texture, Vector2 position, Rectangle? quad, Color color, Rotation rotation, Vector2 origin, float scale, SpriteEffects efx, float layer) =>
			SpriteBatch.Draw(texture, position, quad, color, rotation.Radians, origin, scale, efx, layer);
		public void Text(string text, Vector2 position) => Text(DefaultFont, text, position);
		public void Text(string text, Vector2 position, Color color) => SpriteBatch.DrawString(DefaultFont, text, position, color);
		public void Text(SpriteFont font, string text, Vector2 position) => Text(font, text, position, Color.White, TextXAlignment.Left, TextYAlignment.Top);
		public void Text(SpriteFont font, string text, Vector2 position, Color color, TextXAlignment textX = TextXAlignment.Left, TextYAlignment textY = TextYAlignment.Top)
		{
			float xoffset = 0;
			float yoffset = 0;

			Vector2 bounds = font.MeasureString(text);

			if (textX == TextXAlignment.Center)
				xoffset = bounds.X / 2;
			if (textX == TextXAlignment.Right)
				xoffset = bounds.X;

			if (textY == TextYAlignment.Center)
				yoffset = bounds.Y / 2;
			if (textY == TextYAlignment.Bottom)
				yoffset = bounds.Y;

			SpriteBatch.DrawString(font, text, position - new Vector2(xoffset, yoffset), color);
		}
	}
}
