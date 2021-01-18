using JFramework.Interface.Nodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace JFramework.Interface
{
	public static class ImportUI
	{
		public static string ToXML(this object obj)
		{
			XmlSerializer s = new XmlSerializer(obj.GetType());
			using (StringWriter writer = new StringWriter())
			{
				s.Serialize(writer, obj);
				return writer.ToString();
			}
		}
		public static string ToXML(this object obj, Type[] extras)
		{
			XmlSerializer s = new XmlSerializer(obj.GetType(), extras);
			using (StringWriter writer = new StringWriter())
			{
				s.Serialize(writer, obj);
				return writer.ToString();
			}
		}

		public static T FromXml<T>(this string data)
		{
			XmlSerializer s = new XmlSerializer(typeof(T));
			using (StringReader reader = new StringReader(data))
			{
				object obj = s.Deserialize(reader);
				return (T)obj;
			}
		}

		public static UIScene ImportFromXML(string filepath)
		{
			XmlSerializer xs = new XmlSerializer(typeof(UIScene));
			using (var sr = new StreamReader(filepath))
			{
				return (UIScene)xs.Deserialize(sr);
			}
		}

		public static void SaveToXML(this UIScene node, string filepath)
		{
			XmlSerializer xs = new XmlSerializer(typeof(UIScene));

			TextWriter tw = new StreamWriter(filepath);
			xs.Serialize(tw, node);
		}

	}
}
