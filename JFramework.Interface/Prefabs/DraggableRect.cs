using JFramework.Graphics;
using JFramework.Interface.Nodes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Interface.Prefabs
{
	public class DraggableRect : INode
	{
		public RectNode Rectangle { get; set; }
		public MouseListenerNode MouseListener { get; set; }

		public List<INode> Children => Rectangle.Children;

		public string Name => Rectangle.Name;

		public bool Hidden => Rectangle.Hidden;

		public Vector2 AnchorPoint => Rectangle.AnchorPoint;

		public Vector2 AbsoluteSize => Rectangle.AbsoluteSize;

		public Vector2 AbsolutePosition => Rectangle.AbsolutePosition;

		public INode Parent { get; set; }
		public bool ThinkingDisabled { get; }

		public DraggableRect()
		{
			MouseListener = new MouseListenerNode();
			MouseListener.Parent = this;
		}

		public INode FindFirstChildWithName(string name)=>  Rectangle.FindFirstChildWithName(name);


		public List<INode> FindChildrenWithName(string name)=> Rectangle.FindChildrenWithName(name);

		public void Draw(GraphicsEngine GFX) => Rectangle.Draw(GFX);

		public void Update(GameTime gt)=>Rectangle.Update(gt);
	}
}
