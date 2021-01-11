using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Console
{
	public interface IConsoleCommandCaller {
	
	}

	public class JConsole : GameComponent, IConsoleCommandCaller
	{
		public static JConsole Instance;

		public void RegisterCommand(Command command) => Commands.Add(command);

		public delegate void CommandHandler(JConsole console, Command command, params string[] args);


		public JConsole(Game game) : base(game)
		{

		}
	}
}
