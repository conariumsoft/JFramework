using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Networking.Messaging
{
	public class MessageServer
	{
		public delegate void ClientMessageCallback(MessageUser user, string data);
		public void SendToClient()
		{

		}

		public void SendToAllClients()
		{

		}

		public void SendToAllExcept()
		{

		}

		public void OnMessageFromClient()
		{

		}

		public void OnClientRequest(string message, ClientMessageCallback callback)
		{

		}

	}
}
