using JFramework.Common.Scripting;
using JFramework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Interface.Nodes
{
	public interface IFocusableNode
	{
		SimpleLuaEvent OnFocusGained { get; }
		SimpleLuaEvent OnFocusLost { get; }
		bool Focused { get; set; }
	}


	public class FocusList
	{
		public LinkedList<IFocusableNode> NodeList { get; set; }

		public LinkedListNode<IFocusableNode> CurrentNode { get; set; }

		public void Next()
		{
			CurrentNode = CurrentNode.Next;
		}
		public void Previous()
		{
			CurrentNode = CurrentNode.Previous;
		}
	}
}
