using Assignment.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Assignment
{
	static class Program
	{
		//[DllImport("kernel32.dll")]
		//static extern IntPtr GetConsoleWindow();

		//[DllImport("user32.dll")]
		//static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		//const int SW_HIDE = 0;
		//const int SW_SHOW = 5;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			//var handle = GetConsoleWindow();
			//ShowWindow(handle, SW_SHOW);

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
}
}
