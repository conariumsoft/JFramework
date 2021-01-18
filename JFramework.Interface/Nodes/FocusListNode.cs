using JFramework.Common.Scripting;
using JFramework.Graphics;
using Microsoft.Xna.Framework;
using NLua;
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
		public FocusList()
		{
			NodeList = new LinkedList<IFocusableNode>();
		}


		public LinkedList<IFocusableNode> NodeList { get; set; }

		public LinkedListNode<IFocusableNode> CurrentNode { get; set; }

		private void EnableFocusOnCurrentNode()
		{
			CurrentNode.Value.OnFocusGained.Invoke();
			CurrentNode.Value.Focused = true;
		}
		private void DisableFocusOnCurrentNode()
		{
			CurrentNode.Value.OnFocusLost.Invoke();
			CurrentNode.Value.Focused = false;
		}

		public void Next()
		{
			DisableFocusOnCurrentNode();
			CurrentNode = CurrentNode.Next;
			EnableFocusOnCurrentNode();	
		}
		public void Previous()
		{
			DisableFocusOnCurrentNode();
			CurrentNode = CurrentNode.Previous;
			EnableFocusOnCurrentNode();
				
		}

		public void Add(IFocusableNode node) => NodeList.AddLast(node);
	}
}
