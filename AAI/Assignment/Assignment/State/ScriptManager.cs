using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLua;

namespace Assignment.State
{
	public static class ScriptManager
	{
		public const string SCRIPTPATH = "./scripts/";
		public const string SCRIPTEXTENSION = ".lua";

		private static Dictionary<string, string> loadedFiles = new Dictionary<string, string>();

		public static Lua LoadScript(string scriptName, Dictionary<string, object> objectsToParse)
		{
			try
			{
				Lua script = new Lua();
				script.LoadCLRPackage();
				script.DoString(OpenFile(scriptName));

				if (objectsToParse != null)
				{
					foreach (var objectParse in objectsToParse)
					{
						script[objectParse.Key] = objectParse.Value;
					}
				}

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

		public static void ExecuteScript(Lua script)
		{
			LuaFunction scriptFunction = script[(string) script["state"]] as LuaFunction;
			scriptFunction.Call();
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
