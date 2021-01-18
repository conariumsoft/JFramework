using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using NLua;
using System;
using System.Collections.Generic;
using System.Text;

namespace JFramework.Common.Scripting
{
	public static class Snippets
	{

		public const string ScriptHeader =
			GrabFunction + 
			ListIteratorFunction + 
			EnhancedPrintFunction +
			LogToFileFunction + 
			DefaultImports;

		public const string GrabFunction = @"
function _G.grab(namespace, assembly)
	assembly = assembly or 'JFramework'
	import(assembly, namespace);
end";


		public const string EnhancedPrintFunction = @"
--[[_G.oldprint = print
function _G.print(...)
	local data = {...}
	for i, v in pairs(data) do
		if type(v) == 'table' then
			print(v)
		end
	end
end]]
import('System', 'System.Diagnostics');
function _G.debug(...)
	Debug.WriteLine(...);
end

";

		public const string ListIteratorFunction = @"
local function list(clrlist) -- custom iterator for List<T> types
local it = clrlist:GetEnumerator()
return function()
	local has = it:MoveNext()
		if has then 
			return it.Current
		end
	end
end
_G.list = list
";
		public const string LogToFileFunction = @"
_G.log = function(text)
	LogFile.Current.Log(text);
end
";
		public const string DefaultImports = @"
import('MonoGame.Framework', 'Microsoft.Xna.Framework')
grab('JFramework.Common');
grab('JFramework.Common.Logging');
grab('JFramework.Interface');
grab('JFramework.Interface.Nodes');
";

	}

	public class Script
	{
		public static string IncludeHeader = "";
		public static void IncludeDefault(string import_statement)
		{
			IncludeHeader += import_statement;
		}

		
		public static Script CurrentScript { get; private set; }// = new Lua();

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
			State.DoString(IncludeHeader);
			State["_G.script"] = this;
			CurrentScript = this;
			
			
		}

		public void ImportModule()
		{

		}
	}
}
