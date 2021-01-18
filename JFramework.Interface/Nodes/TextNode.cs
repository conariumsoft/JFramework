using JFramework.Common.Scripting;
using JFramework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NLua;
using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Interface.Nodes
{
	public class TextNode : INode
	{
		// Lua Constructor Syntax Sugar
		public TextNode(LuaTable properties) : this() => this.InitFromLuaPropertyTable(Script.CurrentScript.State, properties);

		public TextNode()
		{

		}
		
		public string Text { get; set; }
		public Color TextColor { get; set; }

		public SpriteFont Font { get; set; }

		public TextXAlignment XAlignment { get; set; }
		public TextYAlignment YAlignment { get; set; }
		public List<INode> Children { get; }
		public string Name { get; }
		public bool Hidden { get; }
		public Vector2 AnchorPoint { get; }
		public Vector2 AbsoluteSize { get; }
		public Vector2 AbsolutePosition { get; }
		public INode Parent { get; }
		public bool ThinkingDisabled { get; }

		public INode FindFirstChildWithName(string name)
		{
			throw new NotImplementedException();
		}

		public List<INode> FindChildrenWithName(string name)
		{
			throw new NotImplementedException();
		}

		public void Draw(GraphicsEngine GFX)
		{
			throw new NotImplementedException();
		}

		public void Update(GameTime gt)
		{
			throw new NotImplementedException();
		}
	}
}
