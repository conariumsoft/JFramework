using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JFramework.Graphics
{
	public static class GraphicsExtensions
	{

		public static void TakeScreenshot(this GraphicsDevice device) => TakeScreenshot(device, $"{DateTime.Now.ToFileTime()}.png");

		public static void TakeScreenshot(this GraphicsDevice GraphicsDevice, string filename)
		{

			Color[] colors = new Color[GraphicsDevice.Viewport.Width * GraphicsDevice.Viewport.Height];

			GraphicsDevice.GetBackBufferData<Color>(colors);

			using (Texture2D tex2D = new Texture2D(GraphicsDevice, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height))
			{

				Directory.CreateDirectory("Screenshots");
				tex2D.SetData<Color>(colors);
				if (string.IsNullOrEmpty(filename))
				{
					filename = Path.Combine("Screenshots", filename);
				}
				using (FileStream stream = File.Create(filename))
				{
					tex2D.SaveAsPng(stream, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
				}
			}
		}
	}
}
