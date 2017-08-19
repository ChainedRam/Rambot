using System;
using System.Collections;
using System.Windows.Forms;

namespace KLD.TeamSpeak.Console
{

    public partial class ConsoleUI : Form
    {
        private ArrayList previousLines;
        private int historyPoint;

        private string lastTyped; 

        public ConsoleUI()
        {
            InitializeComponent();
            previousLines = new ArrayList();
            historyPoint = 0;
            lastTyped = string.Empty;

        }

        public Action<string> LineEntered;
        public Action OnStart;

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            OnStart?.Invoke(); 
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                if (historyPoint == previousLines.Count)
                {
                    return;
                }

                if (historyPoint == 0)
                {
                    lastTyped = textBox.Text; 
                }

                SetLine((string)previousLines[(previousLines.Count - 1) - historyPoint]);

                historyPoint++;
            }
            else if (e.KeyCode == Keys.Down && historyPoint != 0)
            {
                e.Handled = true;
                if (historyPoint == 1)
                {
                    SetLine(lastTyped);
                }
                else
                {
                    SetLine((string)previousLines[(previousLines.Count+1) - historyPoint]);
                }

                historyPoint--;
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            string lText = textBox.Text;

            if (string.IsNullOrEmpty(lText))
            {
                return; 
            }

            this.AppendDisplay(lText);
            this.ExecuteLine(lText);

            previousLines.Add(lText);
            textBox.Clear();

            this.historyPoint = 0; 
        }

        public void AppendDisplay(string text)
        {
            richTextBox.AppendText(text + "\n");
        }
       
        private void SetLine(string line)
        {
            textBox.Clear();
            textBox.Text = line;
        }

        public void ExecuteLine(string line)
        {
            LineEntered?.Invoke(line);
        }

    }
}
