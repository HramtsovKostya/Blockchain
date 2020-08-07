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
            _chain.Add(textBox.Text, "Admin");
            textBox.Clear();
            AddDataToListBox();
        }        

        private void MainForm_Load(object sender, EventArgs e)
        {
            AddDataToListBox();
        }

        private void AddDataToListBox()
        {
            foreach (var block in _chain.Blocks)
                listBox.Items.Add(block);
        }
    }
}