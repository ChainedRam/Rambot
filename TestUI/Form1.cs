using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Task.Factory.StartNew(SetLabel).ContinueWith((t) => { label1.Text = t.Result;  }, TaskScheduler.FromCurrentSynchronizationContext());

            string root = "C:\\Ram\\SoundCollection"; 
            foreach (string n in Directory.GetFiles(root, "*.wav").Select(c => c.Substring(root.Length+1, c.Length - (root.Length + 5))).ToArray())
                Debug.WriteLine(n);
        }

        int i = 0; 

        private string SetLabel()
        {
            Thread.Sleep(10000); 
            return "From a damn thread " + i++;
        }
    }
   
}
