using JFramework.Common.Scripting;
using JFramework.Graphics;
using Microsoft.Xna.Framework;
using NLua;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace JFramework.Interface.Nodes
{
	public class RectNode : BaseNode, IFocusableNode, IRectNode
	{
		public Color Color { get; set; }
		public SimpleLuaEvent OnFocusGained { get; private set;}
		public SimpleLuaEvent OnFocusLost { get; private set; }
		public bool Focused { get; set; }

		public RectNode() : base()
		{
			OnFocusGained = new SimpleLuaEvent();
			OnFocusLost = new SimpleLuaEvent();
			Children = new List<INode>();
		}
		public RectNode(LuaTable properties) : this() => this.InitFromLuaPropertyTable(Script.CurrentScript.State, properties);

		public override void Update(GameTime gt)
		{
			base.Update(gt);
		}

		public override void Draw(GraphicsEngine GFX)
		{
			GFX.Rect(Color, AbsolutePosition, AbsoluteSize);
			base.Draw(GFX);
		}
	}
}
