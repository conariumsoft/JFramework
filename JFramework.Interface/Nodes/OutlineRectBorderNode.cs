using JFramework.Common;
using JFramework.Common.Scripting;
using JFramework.Graphics;
using Microsoft.Xna.Framework;
using NLua;
using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Interface.Nodes
{
	
	public class OutlineRectBorderNode : BaseNode
	{
		public Color Color { get; set; }
		public float Thickness { get; set; }

		public OutlineRectBorderNode(LuaTable properties) : this() => this.InitFromLuaPropertyTable(Script.CurrentScript.State, properties);
		public OutlineRectBorderNode() : base()
		{

		}

		public override void Draw(GraphicsEngine GFX)
		{
			if (Parent is IRectNode rect)
			{
				GFX.OutlineRect(Color, rect.AbsolutePosition, rect.AbsoluteSize, Thickness);
				base.Draw(GFX);
			}
			
		}
		public override void Update(GameTime gt)
		{
			base.Update(gt);
		}
	}
}
