using JFramework.Common.Scripting;
using JFramework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Interface.Nodes
{
	public enum InputType
	{
		Mouse,
		Keyboard,
		Controller,
		Touch
	}
	public class LuaButtonActivationArgs : LuaEventArgs
	{

	}
	public class LuaButtonActivationEvent : LuaEvent<LuaButtonActivationArgs>
	{
		public InputType Input { get; set; }
	}

	public class ButtonInputListenerNode : INode
	{
		public bool IsMouseOver { get; private set; }
		public LuaButtonActivationEvent OnClicked = new LuaButtonActivationEvent();
		public LuaButtonActivationEvent OnReleased = new LuaButtonActivationEvent();
		public SimpleLuaEvent OnMouseEnter = new SimpleLuaEvent();
		public SimpleLuaEvent OnMouseExit = new SimpleLuaEvent();
		public SimpleLuaEvent OnSelected = new SimpleLuaEvent();
		public SimpleLuaEvent OnUnselected = new SimpleLuaEvent();

		
		public string Name { get; }
		public bool Hidden { get; }
		public Vector2 AnchorPoint => Vector2.Zero;
		public Vector2 AbsoluteSize => Parent.AbsoluteSize;
		public Vector2 AbsolutePosition => Parent.AbsolutePosition;
		public INode Parent { get; set; }
		public bool ThinkingDisabled { get; set; }

		public void Draw(GraphicsEngine GFX) { }

		#region Unused
		public List<INode> Children => throw new NotImplementedException();
		public List<INode> FindChildrenWithName(string name) => throw new NotImplementedException();
		public INode FindFirstChildWithName(string name) => throw new NotImplementedException();
		#endregion

		public void Update(GameTime gt)
		{
			
		}
	}
}
