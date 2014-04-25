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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //ArcSWAT.Project p = new ArcSWAT.Project(@"C:\Swat\ArcSWAT\Databases\Example1_model"); //University
            ArcSWAT.Project p = new ArcSWAT.Project(@"C:\Users\zyu\Downloads\LaSalleArcswat2013-05-01\Lasalle-new"); //University La Salle
            //ArcSWAT.Project p = new ArcSWAT.Project(@"C:\Users\yuz\Downloads\Example1_model");   //AAFC

            subbasinView1.setProjectScenario(p, p.Scenarios["Default"].ResultNormal, 
                ArcSWAT.SWATUnitType.RCH);
        }
    }
}
