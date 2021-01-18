using JFramework.Common.Scripting;
using JFramework.Graphics;
using JFramework.Interface.Nodes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace JFramework.TestGame
{


    public class Game1 : Game
    {

		

		Script menuScript;

		UIScene Scene;


		public GraphicsEngine GFX { get; private set; }
        public GraphicsDeviceManager GraphicsDeviceManager { get; private set; }

        public Game1()
        {

			IsMouseVisible = true;
			//Content.RootDirectory = "Assets";
			Window.AllowUserResizing = true;
			Window.AllowAltF4 = true;

			Script.IncludeDefault(Snippets.ScriptHeader);
			Script.IncludeDefault(Snippets.LogToFileFunction);

			GraphicsDeviceManager = new GraphicsDeviceManager(this)
			{
				PreferredBackBufferWidth = 900,
				PreferredBackBufferHeight = 500,
				SynchronizeWithVerticalRetrace = false,
				IsFullScreen = false,
				PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8
			};
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

			
			//menuScript = Script.Load("menu.lua");
			
			//Debug.WriteLine(retval[0]);
			//Scene = (UIScene)retval[0];
			//Debug.WriteLine(Scene);

		}

        protected override void Initialize()
        {
			base.Initialize();

			GFX = new GraphicsEngine(this)
			{
				ContentManager = Content,
				GraphicsDevice = GraphicsDevice,
				GraphicsDeviceManager = GraphicsDeviceManager,
				SpriteBatch = new SpriteBatch(GraphicsDevice)
			};
			GFX.Initialize();

			menuScript = new Script();
			Scene = menuScript.State.DoFile("scripts/menu.lua")[0] as UIScene;
			//Scene = new UIScene();
		}

        protected override void LoadContent() {}

        protected override void Update(GameTime gameTime)
        {
			Scene.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
			//GraphicsDevice.Clear(Color.CornflowerBlue);
			GFX.Clear(Color.Gray);
			// TODO: Add your drawing code here
			GFX.Begin();
			Scene.Draw(GFX);
			GFX.End();
            base.Draw(gameTime);
        }
    }
}
