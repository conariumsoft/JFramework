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
	public class RectNode : BaseNode
	{
		public Color Color { get; set; }
		
		public RectNode() : base()
		{
			Children = new List<INode>();
		}
		public RectNode(LuaTable properties) : this() => this.InitFromLuaPropertyTable(Script.CurrentScript.State, properties);


		public override void Draw(GraphicsEngine GFX)
		{
			GFX.Rect(Color, AbsolutePosition, AbsoluteSize);
			base.Draw(GFX);
		}
	}
}
