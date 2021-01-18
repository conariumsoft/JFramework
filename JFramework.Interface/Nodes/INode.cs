using JFramework.Common.Scripting;
using JFramework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;

namespace JFramework.Interface.Nodes
{
	public class RootNodeControls
	{

		public virtual List<INode> Children { get; protected set; }

		public virtual void Draw(GraphicsEngine GFX)
		{
			foreach (INode child in Children)
				if (!child.Hidden)
					child.Draw(GFX);
		}

		public virtual void Update(GameTime gt)
		{
			foreach (INode child in Children)
				if (!child.ThinkingDisabled)
					child.Update(gt);
		}

		public INode FindFirstChildWithName(string name) => Children.First(t => t.Name == name);
		public List<INode> FindChildrenWithName(string name) => Children.FindAll(t => t.Name == name);

	}


	public class BaseNode : RootNodeControls, INode
	{
		// Lua Constructor Syntax Sugar
		public BaseNode(LuaTable properties) : this() => this.InitFromLuaPropertyTable(Script.CurrentScript.State, properties);
		public SimpleLuaEvent OnParentChanged = new SimpleLuaEvent();

		private INode _parent;
		public INode Parent
		{
			get => _parent;
			set
			{
				if (_parent != null)
					_parent.Children.Remove(this);
				_parent = value;
				_parent?.Children.Add(this);
			}
		}

		public BaseNode()
		{
			Children = new List<INode>();
		}

		
		public string Name { get; set; }
		public bool Hidden { get; set; }
		public Vector2 AnchorPoint { get; set; }


		private Vector2 GetAbsoluteSize()
		{
			if (Parent is IRectNode rect)
			{
				return new Vector2(
					Size.Pixels.X + (rect.AbsoluteSize.X * Size.Scale.X),
					Size.Pixels.Y + (rect.AbsoluteSize.Y * Size.Scale.Y)
				);
			}
			return Size.Pixels;
		}
		private Vector2 GetAbsolutePosition()
		{
			if (Parent is IRectNode rect)
			{
				return new Vector2(
					rect.AbsolutePosition.X + Position.Pixels.X + (rect.AbsoluteSize.X * Position.Scale.X) - (AnchorPoint.X * AbsoluteSize.X),
					rect.AbsolutePosition.Y + Position.Pixels.Y + (rect.AbsoluteSize.Y * Position.Scale.Y) - (AnchorPoint.Y * AbsoluteSize.Y)
				);
			}
			return Position.Pixels;
		}

		public virtual Vector2 AbsoluteSize => GetAbsoluteSize();
		public virtual Vector2 AbsolutePosition => GetAbsolutePosition();

		public UICoords Position { get; set; }
		public UICoords Size { get; set; }
		public bool ThinkingDisabled { get; set; }
	}

	public interface IRectNode
	{
		Vector2 AnchorPoint { get; }
		Vector2 AbsoluteSize { get; }
		Vector2 AbsolutePosition { get; }
	}

	public interface INode
    {
		INode FindFirstChildWithName(string name);
		List<INode> FindChildrenWithName(string name);

		void Draw(GraphicsEngine GFX);
		void Update(GameTime gt);

		List<INode> Children { get; }
		string Name { get; }
		bool Hidden { get; }
		bool ThinkingDisabled { get; }

		INode Parent { get; }
		


    }
}
