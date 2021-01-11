using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Common.AssetManagement
{
    public class TextureManager
    {
        public static TextureManager Instance { get; private set; }

        public TextureManager()
        {
            Instance = this;
        }

    }
}
