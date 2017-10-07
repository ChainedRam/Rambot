using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace KLD.Teamspeak.BotManagerApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Channel list
            string[] channelList = new string[] {"Overwatch", "FFXIV", "Dota" };
            channelComboBox.Items.AddRange(channelList);

            // ID list
            int currentid = 1;
            int maxid = 11;
            while (currentid < maxid)
            {
                idComboBox.Items.Add(currentid);
                currentid++;
            }
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            string name = botNameTextBox.Text;
            string chnl = (string)channelComboBox.SelectedItem;
            int id = (int)idComboBox.SelectedItem;
            //Debug.WriteLine(name + " " + chnl + " " + id);
            string output = string.Format("{0}{1}{2}", name,chnl,id);
            Debug.WriteLine(output);
        }
    }
}
