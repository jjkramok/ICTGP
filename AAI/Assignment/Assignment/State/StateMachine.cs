using Assignment.Entity;
using Assignment.World;
using NLua;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.State
{
	public static class StateMachine
	{
		private static Dictionary<string, Lua> scripts;

		/*
		public StateMachine(BaseEntity entity)
		{
			var objectsToParse = new Dictionary<string, object>();
			objectsToParse.Add("entity", entity);
			objectsToParse.Add("world", GameWorld.Instance);

			script = ScriptManager.LoadScript(ScriptManager.SCRIPTPATH + entity.Type.ToString() + ScriptManager.SCRIPTEXTENSION, objectsToParse);

			if(script == null)
			{
				Console.WriteLine("Failed to open script");
				Console.WriteLine(ScriptManager.SCRIPTPATH + entity.Type.ToString() + ScriptManager.SCRIPTEXTENSION);
			}
		}
		*/

		public static void Initialize()
		{
			scripts = new Dictionary<string, Lua>();

			foreach (string scriptPath in ScriptManager.FindAllScripts())
			{
				scripts.Add(scriptPath.ToLower(), ScriptManager.LoadScript(scriptPath));
			}
		}

		public static void Execute(BaseEntity entity)
		{
			if (GameWorld.Instance.Random.NextDouble() > 0.7)
			{
				string scriptPath = Path.Combine(ScriptManager.SCRIPTPATH, entity.Type.ToString(), entity.State + ScriptManager.SCRIPTEXTENSION).ToLower();

				if (string.IsNullOrEmpty(entity.State))
				{
					Console.WriteLine($"No state set for {entity.Type}");
					return;
				}

				if (entity.State != entity.PreviousState)
				{
					ScriptManager.RunFunctionScript(scripts[scriptPath], "enter", entity);
					entity.PreviousState = entity.State;
				}

				var newState = ScriptManager.RunFunctionScript(scripts[scriptPath], "execute", entity);

				if (newState != entity.State)
				{
					ScriptManager.RunFunctionScript(scripts[scriptPath], "exit", entity);
					entity.State = newState;
				}
			}
		}
	}
}
