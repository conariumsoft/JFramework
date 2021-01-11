using JFramework.Networking.Udp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Networking.Example
{

	

	// Simple server constructed from an example packet type
	public class ExampleNetworkServer : SharedNetworkSubsystem<PKType>
	{
		public override string DeviceName => "NetworkServer";

		public ExampleNetworkServer(int port) : base()
		{
			IncomingMessages = new ConcurrentQueue<NetworkMessage<PKType>>();
			OutgoingMessages = new ConcurrentQueue<NetworkMessage<PKType>>();
		}
	}
}
