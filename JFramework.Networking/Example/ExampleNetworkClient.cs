using JFramework.Common.Generic;
using JFramework.Networking.Udp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace JFramework.Networking.Example
{
	public class ExampleNetworkClient : SharedNetworkSubsystem<PKType>
	{
		public const ushort DEFAULT_PORT = 7777;

		public override string DeviceName => "Network Client";

		public static IPEndPoint GetEndPointFromAddress(string hostname)
		{
			bool success = IPEndPoint.TryParse(ParseHostnameShortcuts(hostname), out IPEndPoint output);
			if (success)
				return output;
			throw new Exception($"Invalid IP Endpoint! {hostname}, {ParseHostnameShortcuts(hostname)}");
		}

		public static string ParseHostnameShortcuts(string hostname)
		{
			hostname = hostname.Trim();
			hostname = Networking.Utilities.ReplaceLocalHostWithLoopbackAddress(hostname);
			hostname = Networking.Utilities.ReplaceEmptyPortWithDefault(hostname, DEFAULT_PORT);

			return hostname;
		}

		public IPEndPoint ServerAddress { get; private set; }

		public string ServerIPAddress { get; private set; }
		public int ServerPort { get; private set; }

		public ExampleNetworkClient(string hostname) : base()
		{
			running = new ThreadSafeValue<bool>(false);
			var endpoint = GetEndPointFromAddress(hostname);// = ParseHostnameShortcuts(hostname);

			ServerAddress = endpoint;

			IncomingMessages = new ConcurrentQueue<NetworkMessage<PKType>>();
			OutgoingMessages = new ConcurrentQueue<NetworkMessage<PKType>>();
			ServerPort = endpoint.Port;
			ServerIPAddress = endpoint.Address.ToString();

			UdpSocket = new UdpClient(ServerIPAddress, ServerPort);


			//UdpSocket.Connect(endpoint);
			//IOControlFixICMPBullshit();
		}
	}
}
