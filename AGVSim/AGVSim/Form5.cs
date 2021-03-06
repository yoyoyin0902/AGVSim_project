using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRect = System.Drawing.Rectangle;
using CString = System.String;
using CPoint = System.Drawing.Point;
using CTime = System.DateTime;
using System.Threading;
using System.Data.OleDb;
using DWG_MAPVIEW;
using System.IO;


namespace AGVSim
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        string url = System.Environment.CurrentDirectory;

        private void Output_Click(object sender, EventArgs e)
        {
            StreamWriter str = new StreamWriter(url + @"/Station" + "/" + StationID.Text + ".txt");
            string WriteWord1 = StationID.Text;
            string WriteWord2 = "Stay" + StayTime.Text + "s";
            str.WriteLine(WriteWord1);
            str.WriteLine(WriteWord2);
            str.Close();
            MessageBox.Show("The file is save！");
        }   

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
