using NLua;
using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Common.Scripting
{
	public class Script
	{
		public const string LuaUtilityFunctionsHeader = @"
import('JFramework', 'JFramework.Common.Logging')

local function list(clrlist) // custom iterator for List<T> types
	local it = clrlist:GetEnumerator()
	return function()
		local has = it:MoveNext()
		if has then 
			return it.Current
		end
	end
end

_G.list = list
_G.log = function(text)
	LogFile.Current.Log(text);
end
";
		public static Lua GlobalScriptEnvironment = new Lua();

		public Lua State { get; private set; }

		public static Script Load(string filepath)
		{
			var script = new Script();
			script.State.DoFile(filepath);
			return script;
		}

		public Script()
		{
			State = new Lua();
			State.LoadCLRPackage();
			State["_G.script"] = this;
			State.DoString(LuaUtilityFunctionsHeader);

		}

		public Script(string source) : base()
		{
			this.State.DoString(source);
		}
	}
}
