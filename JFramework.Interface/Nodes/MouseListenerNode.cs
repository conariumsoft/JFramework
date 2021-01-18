using JFramework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Interface.Nodes
{
	public class MouseListenerNode : INode
	{

		public List<INode> Children => throw new NotImplementedException();
		public string Name { get; }
		public bool Hidden { get; }
		public Vector2 AnchorPoint => Vector2.Zero;
		public Vector2 AbsoluteSize => Vector2.Zero;
		public Vector2 AbsolutePosition => Vector2.Zero;
		public INode Parent { get; set; }
		public bool ThinkingDisabled { get; }

		public void Draw(GraphicsEngine GFX) { }

		public List<INode> FindChildrenWithName(string name) => throw new NotImplementedException();

		public INode FindFirstChildWithName(string name) => throw new NotImplementedException();

		public void Update(GameTime gt)
		{
			
		}
	}
}
