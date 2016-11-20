using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Books
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private string _Path;
        private Working _Working;
        private void OpenBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog Folder = new FolderBrowserDialog();
            if (Folder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _Path = Folder.SelectedPath;
                textPath.Text = _Path + "\\Books\\";
                _Working = new Working(_Path + "\\Books\\");
            }
        }
        public void AddList(string s)
        {
            SC.Invoke( delegate {listBox1.Items.Add(s); });
            
        }
        private void StartBtn_Click(object sender, EventArgs e)
        {
            _Working.StartWirking();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_Working != null)
            {
                label1.Text = "Всего: " + _Working._Count.ToString();
                label2.Text = "Пройдено: " + _Working._Remaints.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _Working.StopWirking();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _Working.PauseWirking();
        }
    }
}
