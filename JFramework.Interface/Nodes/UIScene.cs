using JFramework.Common.Scripting;
using JFramework.Graphics;
using Microsoft.Xna.Framework;
using NLua;
using System;
using System.Collections.Generic;

namespace JFramework.Interface.Nodes
{
    public class UIScene : RootNodeControls, IRectNode, INode
    {
		public UIScene()  {
			Children = new List<INode>();
		}
		public UIScene(LuaTable properties) : this() => this.InitFromLuaPropertyTable(Script.CurrentScript.State, properties);

		public FocusList FocusList { get; set; }

		public bool Hidden { get; set; }
		public bool ThinkingDisabled { get; set; }
		public string Name { get; set; }

		public Vector2 AnchorPoint => Vector2.Zero;
		public Vector2 AbsoluteSize => GraphicsEngine.Instance.WindowSize;
		public Vector2 AbsolutePosition => Vector2.Zero;
		public INode Parent { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }


	}
}
