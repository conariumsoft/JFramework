using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace JFramework.Networking.Messaging
{
	public enum NullEnum
	{
		Default
	}
	public class MessagingPacket : BytePacket<NullEnum>
	{
		public string Command { get; set; }
		public string Message { get; set; }

		public MessagingPacket(string command, string message) : base(NullEnum.Default)
		{
			Command = command;
			Message = message;
		}
	}

	public class MessageUser
	{
		public IPEndPoint Endpoint { get; set; }
	}

}
