using KLD.TeamSpeak.Console;
using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace KLD.TeamSpeak.Core
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string fileName = ConfigurationManager.AppSettings["ConsoleStartUp"];

            string filePath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()) + "\\" + fileName; 

            string[] consoleInitLines = File.ReadAllLines(filePath);

            string currentDirectory = Directory.GetParent(""+Directory.GetParent(Directory.GetCurrentDirectory())).ToString(); 

            //setup loader directories TODO add events 
           

            //insure play command exist TODO remove 
           

            //create controller 
            BotConsoleController controller = new BotConsoleController();

            //Set up application 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Create console 
            ConsoleUI console = new ConsoleUI();

            //set up in and outs 
            console.LineEntered = controller.Read;
            controller.Print = console.AppendDisplay;

            //start up 
            foreach (string line in consoleInitLines)
                controller.Read(line);

            //Start console 
            Application.Run(console);
        }

    }
}
