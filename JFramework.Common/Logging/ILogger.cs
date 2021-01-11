using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Common.Logging
{
	public interface ILogger
	{
		void Log(string message);
		void Log(string message, Color color);
		void WTF(string message);
		void Warn(string message);
		void Error(string message); // Not the same as throwing error, just displays error text in proper coloring.
	}
}
