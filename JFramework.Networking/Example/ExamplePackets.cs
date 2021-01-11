using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Networking.Example
{
	// Example of how to use BytePacket
	public enum NetContext { Server, Client, Both }
	public class ProtocolAttribute : Attribute
	{
		public ProtocolAttribute(PKType type, NetContext context) { }
	}

	// Your template Identifier for these packets
	public enum PKType { Login, Logout, Say, RunCommand, Find }

	public class ExampleBasePacket : BytePacket<PKType>
	{
		public ExampleBasePacket(PKType type) : base(type) { }
	}

	[Protocol(PKType.Login, NetContext.Client)]
	public class ExampleLoginPacket : ExampleBasePacket
	{
		public ExampleLoginPacket() : base(PKType.Login) { }
	}

	// Done
}
