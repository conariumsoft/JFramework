using CaveGame.Core.Generic;
using JFramework.Common.Extensions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JFramework.Networking.Udp
{

	public enum PacketType
	{
		Varg
	}

	public class Packet2 : BytePacket<PacketType>
	{
		public Packet2() : base(PacketType.Varg)
		{

		}
	}

   public class SharedNetworkSubsystem<T> where T: Enum
   {
        public const int NAP_TIME_MILLISECONDS = 1;
        public const int SIO_UDP_CONNRESET = -1744830452;


        protected ThreadSafeValue<bool> running { get; set; }
        protected ConcurrentQueue<NetworkMessage<T>> IncomingMessages { get; set; }
        protected ConcurrentQueue<NetworkMessage<T>> OutgoingMessages { get; set; }

        public bool IsRunning => running.Value;

        public virtual string DeviceName { get; }

        public virtual DateTime LatestReceiveTimestamp { get; protected set; }
        public virtual DateTime LatestSendTimestamp { get; protected set; }
        public virtual int PacketsReceived { get; protected set; }
        public virtual int PacketsSent { get; protected set; }
        public virtual int TotalBytesSent { get; protected set; }
        public virtual int TotalBytesReceived { get; protected set; }
        public virtual int BytesSentPerSecond { get; protected set; }
        public virtual int BytesReceivedPerSecond { get; protected set; }


        public virtual int Port { get; protected set; }

        protected int InternalReceiveCount { get; set; }
        protected int InternalSendCount { get; set; }

        private float counter = 0;

        protected UdpClient UdpSocket { get; set; }

        protected void IOControlFixICMPBullshit()
        {
            UdpSocket.Client.IOControl(
                (IOControlCode)SIO_UDP_CONNRESET,
                new byte[] { 0, 0, 0, 0 },
                null
            );
        }

        private void ResetByteCounters()
        {
            BytesSentPerSecond = InternalSendCount;
            BytesReceivedPerSecond = InternalReceiveCount;
            InternalSendCount = 0;
            InternalReceiveCount = 0;
            counter = 0;
        }

        public SharedNetworkSubsystem()
        {
            IncomingMessages = new ConcurrentQueue<NetworkMessage<T>>();
            OutgoingMessages = new ConcurrentQueue<NetworkMessage<T>>();
           // IOControlFixICMPBullshit();
            
        }

        public virtual void Update(GameTime gt)
        {
            counter += gt.GetDelta();
            if (counter > (1.0f))
                ResetByteCounters();
        }

        public bool HaveIncomingMessage() => !IncomingMessages.IsEmpty;

        public NetworkMessage<T> GetLatestMessage()
        {
            NetworkMessage<T> msg;
            bool success = IncomingMessages.TryDequeue(out msg);

            if (success)
                return msg;
            throw new Exception("No Message Queued! Used HaveIncomingMessage() to check!");
        }

        protected virtual void ReadIncomingPackets()
        {
            bool canRead = UdpSocket.Available > 0;
            if (canRead)
            {

                IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = UdpSocket.Receive(ref ep);

                NetworkMessage<T> nm = new NetworkMessage<T>();
                nm.Sender = ep;
                nm.Packet = new BytePacket<T>(data);
                nm.ReceiveTime = DateTime.Now;

                IncomingMessages.Enqueue(nm);
                PacketsReceived++;
                TotalBytesReceived += nm.Packet.Payload.Length;
                InternalReceiveCount += nm.Packet.Payload.Length;
                LatestReceiveTimestamp = DateTime.Now;

            }
        }

        private void FlushOutgoingPackets()
        {
            int outQCount = OutgoingMessages.Count;


           
            // write out queued messages
            for (int i = 0; i < outQCount; i++)
            {
                NetworkMessage<T> packet;
                bool have = OutgoingMessages.TryDequeue(out packet);

                if (have)
                {
                    if (packet.TargetAddress == null)
                        packet.Packet.Send(UdpSocket);    
                    else
                        packet.Packet.Send(UdpSocket, packet.TargetAddress);

                    PacketsSent++;
                    TotalBytesSent += packet.Packet.Payload.Length;
                    InternalSendCount += packet.Packet.Payload.Length;
                }
            }
        }

        private void NetworkThreadLoop()
        {
            //Output.Log($"{DeviceName} thread started on {UdpSocket.Client.LocalEndPoint} {running.Value}");
            while (running.Value)
            {
                bool canRead = UdpSocket.Available > 0;
                int outgoingMessageQueueCount = OutgoingMessages.Count;
                
                ReadIncomingPackets();
                FlushOutgoingPackets();

                // if nothing happened, take a nap
                if (!canRead && (outgoingMessageQueueCount == 0))
                    Thread.Sleep(NAP_TIME_MILLISECONDS);
            }

           // GameConsole.Log($"{DeviceName} thread finished, cleaning resources...");
            UdpSocket.Close();
            //UdpSocket.Dispose();

        }

        public void Start()
        {
            running.Value = true;
            Task.Factory.StartNew(NetworkThreadLoop);
        }

        public void Close()
        {
            running.Value = false;
            //GameConsole.Log($"closing {DeviceName}");
        }

        protected void Send(BytePacket<T> packet, IPEndPoint target)
        {

            OutgoingMessages.Enqueue(new NetworkMessage<T> { 
                Packet = packet,  
                TargetAddress = target,
            });
            LatestSendTimestamp = DateTime.Now;
        }

        protected void Send(BytePacket<T> packet)
        {
            OutgoingMessages.Enqueue(new NetworkMessage<T> { Packet = packet });
            LatestSendTimestamp = DateTime.Now;
        }

    }
}
