using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLua;
using Assignment.Entity;
using Assignment.World;

namespace Assignment.State
{
	public static class ScriptManager
	{
		public static readonly string SCRIPTPATH = Path.Combine(".", "scripts");
		public const string SCRIPTEXTENSION = ".lua";

		private static Dictionary<string, string> loadedFiles = new Dictionary<string, string>();

		public static Lua LoadScript(string scriptName)
		{
			try
			{
				Lua script = new Lua();
				script.LoadCLRPackage();
				script.DoString(OpenFile(scriptName));

				return script;
			}
			catch
			{
				return null;
			}
		}

		public static string[] FindAllScripts()
		{
			return Directory.GetFiles(SCRIPTPATH, "*" + SCRIPTEXTENSION, SearchOption.AllDirectories);
		}

		public static string RunFunctionScript(Lua script, string functionName, BaseEntity entity)
		{
			try
			{
				LuaFunction scriptFunction = script[functionName] as LuaFunction;
				var returnValue = scriptFunction.Call(entity, GameWorld.Instance);
				if (returnValue != null && returnValue.Length == 1)
				{
					return returnValue.First().ToString();
				}
				return null;
			}
			catch (Exception e)
			{
				Console.WriteLine($"Script could not be executed.");
				Console.WriteLine(e.Message);
				Console.WriteLine($"In file: {entity.Type}/{entity.State}.lua");
				Console.WriteLine($"In function: {functionName}");
			}
			return null;
		}

		private static string OpenFile(string scriptName)
		{
			if (!loadedFiles.ContainsKey(scriptName))
			{
				try
				{
					string text = File.ReadAllText(scriptName);
					loadedFiles.Add(scriptName, text);
				}
				catch
				{
					return "";
				}
			}
			return loadedFiles[scriptName];
		}
	}
}
