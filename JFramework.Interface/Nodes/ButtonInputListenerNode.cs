using JFramework.Common.Scripting;
using JFramework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NLua;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
	public enum MouseButton
	{
		Left,
		Middle,
		Right,
		XButton1,
		XButton2,

	}

	public class LuaButtonActivationArgs : LuaEventArgs
	{

	}
	public class LuaButtonActivationEvent : LuaEvent<LuaButtonActivationArgs>
	{
		public InputType Input { get; set; }
	}

	public class MouseClickLuaEventArgs : LuaEventArgs
	{
		public MouseButton Button { get; set; }

		public MouseClickLuaEventArgs(MouseButton button)
		{
			Button = button;
		}
	}
	public class MouseClickLuaEvent : LuaEvent<MouseClickLuaEventArgs>
	{
		
	}

	public class ButtonInputListenerNode : BaseNode
	{
		public ButtonInputListenerNode() { }
		public ButtonInputListenerNode(LuaTable properties) : this() => this.InitFromLuaPropertyTable(Script.CurrentScript.State, properties);

		public bool IsMouseOver { get; private set; }
		public  bool IsMouseFocused { get; private set; }
		public bool IsTouched { get; private set; }



		private bool IsFocusedParent()
		{
			if (Parent is IFocusableNode focusable)
				return focusable.Focused;
			return false;
		}
		public bool IsButtonFocused => IsFocusedParent();

		public LuaButtonActivationEvent OnClicked = new LuaButtonActivationEvent();
		public LuaButtonActivationEvent OnReleased = new LuaButtonActivationEvent();
		public SimpleLuaEvent OnMouseEnter = new SimpleLuaEvent();
		public SimpleLuaEvent OnMouseExit = new SimpleLuaEvent();

		public SimpleLuaEvent OnLMBClick = new SimpleLuaEvent();
		public SimpleLuaEvent OnLMBRelease = new SimpleLuaEvent();

		public MouseClickLuaEvent OnMouseDown = new MouseClickLuaEvent();
		public MouseClickLuaEvent OnMouseUp = new MouseClickLuaEvent();

		public SimpleLuaEvent OnSelected = new SimpleLuaEvent();
		public SimpleLuaEvent OnUnselected = new SimpleLuaEvent();


		public LuaButtonActivationEvent OnFocus = new LuaButtonActivationEvent();
		public LuaButtonActivationEvent OnFocusLost = new LuaButtonActivationEvent();

		public override Vector2 AbsoluteSize => Parent.AbsoluteSize;
		public override Vector2 AbsolutePosition => Parent.AbsolutePosition;
		

		public override void Draw(GraphicsEngine GFX) { }

		#region Unused
		public override List<INode> Children => throw new NotImplementedException();
		#endregion

		public bool IsMouseStateInside(MouseState mouse)=> (mouse.X > AbsolutePosition.X && mouse.Y > AbsolutePosition.Y
			&& mouse.X < (AbsolutePosition.X + AbsoluteSize.X)
			&& mouse.Y < (AbsolutePosition.Y + AbsoluteSize.Y));


		public bool IsMouseInsideNow() => IsMouseStateInside(Mouse.GetState());

		protected MouseState prevMouse = Mouse.GetState();


		ButtonState prevLMBState;
		ButtonState prevMMBState;
		ButtonState prevRMBState;

		public override void Update(GameTime gt)
		{
			var state = Mouse.GetState();
			//if (IsMouseInside(Parent))
			
			IsMouseOver = IsMouseStateInside(state);

			
			if (IsMouseOver && !IsMouseStateInside(prevMouse))
				OnMouseEnter.Invoke(); // mouse just entered

			if (!IsMouseOver && IsMouseStateInside(prevMouse))
				OnMouseExit.Invoke(); // mouse just left

			
			if (IsMouseOver)
			{
				if (state.LeftButton != prevLMBState) {
					if (IsMouseOver && state.LeftButton == ButtonState.Pressed)
					{
						OnLMBClick.Invoke();
						OnMouseDown.Invoke(new(MouseButton.Left));
					}
					if (state.LeftButton == ButtonState.Released)
					{
						OnLMBRelease.Invoke();
						OnMouseUp.Invoke(new(MouseButton.Left));
					}
				}
				if (state.RightButton != prevRMBState)
				{
					if (state.RightButton == ButtonState.Pressed)
						OnMouseDown.Invoke(new(MouseButton.Right));
					if (state.RightButton == ButtonState.Released)
						OnMouseUp.Invoke(new(MouseButton.Right));
				}
				if (state.MiddleButton != prevMMBState)
				{
					if (state.MiddleButton == ButtonState.Pressed)
						OnMouseDown.Invoke(new(MouseButton.Middle));
					if (state.MiddleButton == ButtonState.Released)
						OnMouseUp.Invoke(new(MouseButton.Middle));
				}

			}


			prevLMBState = state.LeftButton;
			prevRMBState = state.RightButton;
			prevMMBState = state.MiddleButton;

			prevMouse = state;
		}
	}
}
