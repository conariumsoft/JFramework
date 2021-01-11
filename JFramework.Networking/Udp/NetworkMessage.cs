using System;
using System.Net;

namespace JFramework.Networking.Udp
{
	public class NetworkMessage<T> where T: Enum
	{
		public IPEndPoint Sender { get; set; }
		public BytePacket<T> Packet { get; set; }
		public DateTime ReceiveTime { get; set; }
		public IPEndPoint TargetAddress { get; set; }
	}
}
