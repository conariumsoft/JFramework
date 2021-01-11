using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Networking
{

	public static class Utilities
	{
		public static string ReplaceLocalHostWithLoopbackAddress(string input)
		{
			return input.Replace("localhost", "127.0.0.1");
		}

		public static string ReplaceEmptyPortWithDefault(string input, ushort port)
		{
			if (!input.Contains(":"))
				return input + ":" + port.ToString();
			return input;
		}
	}
}
