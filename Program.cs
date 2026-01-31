using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryManagementSystem.core;

namespace LibraryManagementSystem
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Start the server on a background thread
            var core = new Core();
            _ = Task.Run(() => core.StartAsync());

            // Wait a moment for server to start before launching the UI
            System.Threading.Thread.Sleep(500);

            Application.Run(new Login());
        }
    }
}
