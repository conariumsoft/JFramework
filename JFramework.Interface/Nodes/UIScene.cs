using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Interface.Nodes
{
    public class UIScene : IDrawable
    {
        public int DrawOrder { get; }
        public bool Visible { get; }

        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;

        public void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
