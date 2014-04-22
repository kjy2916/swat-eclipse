using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SWAT_SQLite_Result
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ArcSWAT.Project p = new ArcSWAT.Project(@"C:\Swat\ArcSWAT\Databases\Example1_model"); //University
            //ArcSWAT.Project p = new ArcSWAT.Project(@"C:\zhiqiang\Existing SWAT Model\Lasalle-new");   //AAFC
            richTextBox1.Text = p.ToString();
            System.Diagnostics.Debug.WriteLine(richTextBox1.Text);

            projectTree1.Project = p;
        }
    }
}
