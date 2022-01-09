using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DWG_MAPVIEW
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }      

        private void btn_Turn_To_Form1_Click(object sender, EventArgs e)
        {
            Form f4 = new Form4();
            this.Visible = false;
        }
        
        private void ReadDWG_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\Users\Serverler\Desktop\",
                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "dwg",
                Filter = "dwg files (*.dwg)|*.dwg",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            addDwgAsBmpImage(textBox1.Text);
        }

        private void addDwgAsBmpImage(string name)
        {

            try
            {
                axAcCtrl1.PutSourcePath(name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void axAcCtrl1_Enter(object sender, EventArgs e)
        {

        }
    }
}
