using JFramework.Common.Scripting;
using JFramework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Interface.Nodes
{


	public class BaseNode : INode
	{
		public SimpleLuaEvent OnParentChanged = new SimpleLuaEvent();

		private INode _parent;
		public INode Parent
		{
			get => _parent;
			set { 
				
			}
		}

		private List<INode> _children;
		public List<INode> Children
		{
			get { }
			set { }

		}
	}


    public interface INode
    {

		INode FindFirstChildWithName(string name);
		List<INode> FindChildrenWithName(string name);

		void Draw(GraphicsEngine GFX);
		void Update(GameTime gt);

		Vector2 AnchorPoint { get; set; }
		Vector2 AbsoluteSize { get; }
		Vector2 AbsolutePosition { get; }



		INode Parent { get; }
		List<INode> Children { get; }
		string Name { get; }
		bool Visible { get; }


    }
}
