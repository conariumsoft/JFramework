using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Common.ECS
{
	public class Entity
	{
		public List<IComponent> Components { get; set; }

		public bool Has<T>() where T : IComponent
		{
			foreach (var comp in Components)
				if (comp is T)
					return true;
			return false;
		}
		public T Get<T>() where T : IComponent
		{
			foreach (var comp in Components)
				if (comp is T)
					return (T)comp;
			throw new Exception();
		}
	}
}
