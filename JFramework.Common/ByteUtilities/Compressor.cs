using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace JFramework.Common.ByteUtilities
{
	public static class Compressor
	{
		public static byte[] Compress(byte[] data, CompressionLevel level = CompressionLevel.Optimal)
		{
			MemoryStream output = new MemoryStream();
			using (DeflateStream dstream = new DeflateStream(output, level))
			{
				dstream.Write(data, 0, data.Length);
			}
			return output.ToArray();
		}
		public static byte[] Decompress(byte[] data)
		{
			MemoryStream input = new MemoryStream(data);
			MemoryStream output = new MemoryStream();

			using (DeflateStream dstream = new DeflateStream(input, CompressionMode.Decompress))
			{
				dstream.CopyTo(output);
			}
			return output.ToArray();
		}
	}
}
