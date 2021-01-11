using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JFramework.Common.Logging
{

	public class LogFile : ILogger
	{

		private static LogFile instance;
		private static LogFile getInstance()
		{
			if (instance == null)
				instance = new LogFile();
			return instance;
		}

		public static LogFile Current => getInstance();


		public string Timestamp { get; set; }

		public LogFile()
		{
			Timestamp = DateTime.Now.ToString("yy-MM-dd");
		}

		public void Log(string data, Color color) => Log(data);
		public void WTF(string data) => Write("WTF "+DateTime.Now.ToString("HH-mm-ss"), data);
		public void Warn(string data) => Write("Warning " + DateTime.Now.ToString("HH-mm-ss"), data);
		public void Error(string data) => Write("Error " + DateTime.Now.ToString("HH-mm-ss"), data);
		private void Write(string prefix, string data)
		{
			if (!Directory.Exists("logs"))
				Directory.CreateDirectory("logs");
			string txt = $"{prefix}: {data}\n";
			File.AppendAllText(Path.Combine("logs", $"log_{Timestamp}.txt"), $"{prefix}: {data}\n");
		}

		public void Log(string data) => Write(DateTime.Now.ToString("HH-mm-ss"), data);
	}
}
