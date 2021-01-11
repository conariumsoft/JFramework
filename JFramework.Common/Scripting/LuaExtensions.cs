using NLua;
using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Common.Scripting
{
	public static class LuaExtensions
	{
		public static LuaTable GetEmptyTable(this NLua.Lua state) => (LuaTable)state.DoString("return {}")[0];


		public static void InitFromLuaPropertyTable(this object thing, Lua environment, LuaTable table)
		{
			foreach (KeyValuePair<object, object> kvp in environment.GetTableDict(table))
			{
				if (kvp.Key is string keyString)
				{
					var prop = thing.GetType().GetProperty(keyString);
					if (prop != null)
					{
#if AUTOCASTING_DEBUG
						Debug.WriteLine("PropertySet {0} to {1} on {2}", keyString, kvp.Value.ToString(), thing.ToString());
#endif
						prop.SetValue(thing, Cast(prop.PropertyType, kvp.Value));
					}
				}
			}
		}
	}
}
