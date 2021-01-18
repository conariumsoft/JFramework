using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Common.ECS
{
	public interface IComponent { }

	public struct SoundComponent : IComponent
	{
		public SoundEffect Source { get; set; }
		public float MasterVolume { get; set; }
	}
	public struct SpriteComponent : IComponent
	{
		public Texture2D Sprite { get; set; }
		public List<Rectangle> Frames { get; set; }
		public Color DefaultColor { get; set; }
		public float SpriteFrameAnimation { get; set; }
	}
	public struct PersistentComponent : IComponent
	{
		public string File { get; set; }
	}
	public struct HealthComponent : IComponent
	{
		public float Health { get; set; }
		public float MaxHealth { get; set; }
		public float RegenRate { get; set; }
	}
}
