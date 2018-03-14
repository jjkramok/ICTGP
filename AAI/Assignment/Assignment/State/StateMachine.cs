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
			string scriptPath = Path.Combine(ScriptManager.SCRIPTPATH, entity.Type.ToString(), entity.State + ScriptManager.SCRIPTEXTENSION).ToLower();

			if (string.IsNullOrEmpty(entity.State))
			{
				Console.WriteLine($"No state set for {entity.Type}");
				return;
			}
			if(entity.State == "DEBUGSTATE")
			{
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
