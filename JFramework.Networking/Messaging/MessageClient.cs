using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Networking.Messaging
{
	public class MessageClient
	{
		public delegate void MessageCallback(string data);

		public void RequestAsync(string message, string data, MessageCallback callback)
		{

		}

		public string Request(string message, string data)
		{
			return "";

		}

		public string SendToServer(string message, string data)
		{
			return "";
		}


		public void OnMessageFromServer(string message, MessageCallback callback)
		{

		}
	}
}
