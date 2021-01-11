using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;

namespace JFramework.Networking
{
	
	public class BytePacket<T> where T : Enum
	{
		public long Timestamp;
		public byte[] Payload = new byte[0];

		public T Type;

		public BytePacket(T type)
		{
			Type = type;
			Timestamp = DateTime.Now.Ticks;
		}
		public BytePacket(byte[] bytes)
		{
			int i = 0;
			Type = (T)Enum.ToObject(typeof(T), BitConverter.ToUInt32(bytes, 0));
			i += sizeof(uint);

			Timestamp = BitConverter.ToInt64(bytes, i);
			i += sizeof(long);

			Payload = bytes.Skip(i).ToArray();
		}

		public byte[] GetBytes()
		{
			int ptSize = sizeof(uint);
			int tsSize = sizeof(long);

			int i = 0;
			byte[] bytes = new byte[ptSize + tsSize + Payload.Length];
			// Type
			BitConverter.GetBytes(Convert.ToUInt32(Type)).CopyTo(bytes, i);
			i += ptSize;
			// Timestamp
			BitConverter.GetBytes(Timestamp).CopyTo(bytes, i);
			i += tsSize;
			// Payload
			Payload.CopyTo(bytes, i);
			i += Payload.Length;

			return bytes;
		}

		public void Send(UdpClient client, IPEndPoint receiver)
		{
			byte[] bytes = GetBytes();
			client.Send(bytes, bytes.Length, receiver);
		}
		public void Send(UdpClient client)
		{
			byte[] bytes = GetBytes();
			client.Send(bytes, bytes.Length);
		}
		protected string DumpHex(byte[] data, int index, int length)
		{
			StringBuilder bob = new StringBuilder();
			for (int i = 0; i < length; i++)
			{
				bob.Append(String.Format("{0:X2} ", data[i + index]));
			}
			return bob.ToString();
		}
	}

}
