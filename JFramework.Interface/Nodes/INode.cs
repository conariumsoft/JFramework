using JFramework.Common.Scripting;
using JFramework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;
using System.Xml.Serialization;

namespace JFramework.Interface.Nodes
{
	public class RootNodeControls
	{

		[XmlIgnore]
		public virtual List<INode> Children { get; protected set; }
		public virtual void Draw(GraphicsEngine GFX)
		{
			foreach (INode child in Children)
				child.Draw(GFX);
		}

		public virtual void Update(GameTime gt)
		{
			foreach (INode child in Children)
				child.Update(gt);
		}

		public INode FindFirstChildWithName(string name) => Children.First(t => t.Name == name);
		public List<INode> FindChildrenWithName(string name) => Children.FindAll(t => t.Name == name);

	}


	public class BaseNode : RootNodeControls, INode
	{

		public override void Draw(GraphicsEngine gfx)
		{

			gfx.Circle(new Color(0, 0, 1.0f), AbsolutePosition, 2);
			gfx.Circle(new Color(1, 1, 0.0f), AbsolutePosition + AbsoluteSize, 2);
			gfx.Circle(new Color(0, 1, 0.0f), AbsolutePosition + (AnchorPoint * AbsoluteSize), 2);
			//gfx.Text($"abs pos{this.AbsolutePosition} size{this.AbsoluteSize}", AbsolutePosition);
			//gfx.Text($"{this.Children.Count} children", AbsolutePosition + new Vector2(0, 12));
			base.Draw(gfx);
		}

		// Lua Constructor Syntax Sugar
		public BaseNode(LuaTable properties) : this() => this.InitFromLuaPropertyTable(Script.CurrentScript.State, properties);
		public SimpleLuaEvent OnParentChanged = new SimpleLuaEvent();

		// The parent thing
		// Don't forget to port to it's own class?
		// so you don't have to duplicate it everywhere?
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


		public virtual Vector2 AbsoluteSize => new Vector2(
			Size.Pixels.X + (Parent.AbsoluteSize.X * Size.Scale.X),
			Size.Pixels.Y + (Parent.AbsoluteSize.Y * Size.Scale.Y)
		);
		public virtual Vector2 AbsolutePosition => new Vector2(
			Parent.AbsolutePosition.X + Position.Pixels.X + (Parent.AbsoluteSize.X* Position.Scale.X) - (AnchorPoint.X* AbsoluteSize.X),
			Parent.AbsolutePosition.Y + Position.Pixels.Y + (Parent.AbsoluteSize.Y* Position.Scale.Y) - (AnchorPoint.Y* AbsoluteSize.Y)
		);

		public UICoords Position { get; set; }
		public UICoords Size { get; set; }
		public bool ThinkingDisabled { get; set; }
	}

	public interface IRectNode
	{
		
	}

	public interface INode
    {
		Vector2 AnchorPoint { get; }
		Vector2 AbsoluteSize { get; }
		Vector2 AbsolutePosition { get; }
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
