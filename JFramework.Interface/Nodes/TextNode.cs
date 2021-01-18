using JFramework.Common.Scripting;
using JFramework.Graphics;
using JFramework.TestGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NLua;
using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Interface.Nodes
{
	public class TextNode : BaseNode, INode
	{
		// Lua Constructor Syntax Sugar
		public TextNode(LuaTable properties) : this() => this.InitFromLuaPropertyTable(Script.CurrentScript.State, properties);

		public TextNode()
		{
			Text = "TextNode";
			Font = FontManager.Arial;
		}
		
		public string Text { get; set; }
		public bool TextWrap { get; set; }
		public Color TextColor { get; set; }
		public SpriteFont Font { get; set; }
		public TextXAlignment XAlignment { get; set; }
		public TextYAlignment YAlignment { get; set; }
		public override Vector2 AbsoluteSize => Parent.AbsoluteSize;
		public override Vector2 AbsolutePosition => Parent.AbsolutePosition;


		public bool TextWrapping { get; private set; }
		public int TextWrappingCount { get; private set; }

		StringBuilder sb = new StringBuilder();
		public string WrapText(SpriteFont spritefont, string text, float maxwidth)
		{
			sb.Clear();
			string[] words = text.Split(' ');
			float lineWidth = 0f;
			float spaceWidth = spritefont.MeasureString(" ").X;

			TextWrappingCount = 0;


			foreach(string word in words)
			{
				Vector2 size = spritefont.MeasureString(word);

				if (lineWidth+size.X < maxwidth)
				{
					sb.Append(word + " ");
					lineWidth += size.X + spaceWidth;
				}
				else
				{
					sb.Append("\n" + word + " ");
					lineWidth = size.X + spaceWidth;
					TextWrappingCount++;
				}
			}
			return sb.ToString();
		}

		public Vector2 GetTextDimensions()
		{
			return Font.MeasureString(Text);
		}

		public override void Draw(GraphicsEngine GFX)
		{
			string DisplayedText = "";

			if (Text != null)
				DisplayedText = Text;
			if (TextWrap)
				DisplayedText = WrapText(Font, Text, AbsoluteSize.X);

			Vector2 textDim = Font.MeasureString(DisplayedText);
			Vector2 TextOutputPosition = AbsolutePosition;

			// Text Alignment
			if (XAlignment == TextXAlignment.Center)
				TextOutputPosition += new Vector2((AbsoluteSize.X / 2) - (textDim.X / 2), 0);
			if (XAlignment == TextXAlignment.Right)
				TextOutputPosition += new Vector2(AbsoluteSize.X - textDim.X, 0);

			if (YAlignment == TextYAlignment.Center)
			{
				TextOutputPosition += new Vector2(0, (AbsoluteSize.Y / 2) - (textDim.Y / 2));
			}
			if (YAlignment == TextYAlignment.Bottom)
			{
				TextOutputPosition += new Vector2(0, AbsoluteSize.Y - textDim.Y);
			}
			TextOutputPosition.Floor();
			GFX.Text(Font, DisplayedText, TextOutputPosition, TextColor);

			base.Draw(GFX);
		}

		public override void Update(GameTime gt)
		{
			
		}
	}
}
