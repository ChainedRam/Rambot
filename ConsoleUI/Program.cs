using System;
using System.Windows.Forms;

namespace KLD.TeamSpeak.Console
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ConsoleUI console;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            console = new ConsoleUI();
            Application.Run(console);
        }
    }
}
