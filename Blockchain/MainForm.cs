using Blockchain.Model;
using System;
using System.Windows.Forms;

namespace Blockchain
{
    public partial class MainForm : Form
    {
        public MainForm() => InitializeComponent();

        private readonly Chain _chain = new Chain();

        private void AddButton_Click(object sender, EventArgs e)
        {
            listBox.Items.Clear();
            _chain.Add(textBox.Text, "User");
            textBox.Clear();

            listBox.Items.AddRange(_chain.Blocks.ToArray());
        }        

        private void MainForm_Load(object sender, EventArgs e)
        {
            listBox.Items.AddRange(_chain.Blocks.ToArray());
        }
    }
}