using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Common.ECS
{
	public class System
	{
		public virtual bool IsResponsible(Entity entity)
		{
			if (entity.Has<SpriteComponent>() && entity.Has<PersistentComponent>())
				return true;
			return false;
		}
		public virtual void Update(Entity entity)
		{
			SpriteComponent comp = entity.Get<SpriteComponent>();
			comp.SpriteFrameAnimation += 0.1f;
		}
	}
	public class SoundSystem : System
	{
		public override bool IsResponsible(Entity entity)
		{
			if (entity.Has<SoundComponent>())
				return true;
			return false;
		}
		public override void Update(Entity entity)
		{
			SoundComponent comp = entity.Get<SoundComponent>();

			if (entity.Has<HealthComponent>())
			{
				var hp_component = entity.Get<HealthComponent>();
			}
		}
	}
}
