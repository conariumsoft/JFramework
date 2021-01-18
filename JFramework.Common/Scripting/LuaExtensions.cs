using NLua;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace JFramework.Common.Scripting
{
	public static class LuaExtensions
	{
		public static LuaTable GetEmptyTable(this NLua.Lua state) => (LuaTable)state.DoString("return {}")[0];

		public static object CastTo(this Type type, object data)
		{
			var DataParam = Expression.Parameter(typeof(object), "data");
			var Body = Expression.Block(Expression.Convert(Expression.Convert(DataParam, data.GetType()), type));

			var Run = Expression.Lambda(Body, DataParam).Compile();
			var ret = Run.DynamicInvoke(data);
			return ret;

		}

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
						prop.SetValue(thing, CastTo(prop.PropertyType, kvp.Value));
					}
				}
			}
		}
	}
}
