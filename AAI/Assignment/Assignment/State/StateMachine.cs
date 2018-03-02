using Assignment.Entity;
using Assignment.World;
using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.State
{
	public class StateMachine
	{
		private Lua script;

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

		public void Execute()
		{
			try
			{
				ScriptManager.ExecuteScript(script);
			}
			catch
			{
				Console.WriteLine("Script could not be executed");
			}
		}
	}
}
