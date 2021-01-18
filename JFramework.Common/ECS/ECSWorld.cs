using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Common.ECS
{
	public class ECSWorld
	{
		public List<System> Systems { get; set; }
		public List<Entity> Entities { get; set; }


		public void Update()
		{
			foreach (var system in Systems)
				foreach (var entity in Entities)
					if (system.IsResponsible(entity))
						system.Update(entity);
		}
	}
}
