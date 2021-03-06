﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Research.Oslo;

namespace WindowsFormsApplication1
{
    public  partial class Form1 : Form
    {
        //int x = 0;
        public static List<Program.Gear> gears = new List<Program.Gear>();

        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileMain.ShowDialog() == DialogResult.OK)
            {
                // Change this to do something else later
                Close();
            }
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            Process proc = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    FileName = @"calcApps\testfunc\testfunc.exe",
                    Arguments = $"{num1.Value} {num2.Value}"
                }
            };
            proc.Start();
            string output = proc.StandardOutput.ReadToEnd();
            //int y; // This is the result of subtraction and y coordinate of graph
            //int.TryParse(output, out y); // Convert string to int
            //chart1.Series["Series1"].Points.AddXY(x + 1, y); // plot
            //x = x + 1; // increment x coordinate of graph by 1
            proc.WaitForExit();
            Console.Write(output);
            MessageBox.Show(output);
        }

        private void ODEbutton_Click_1(object sender, EventArgs e)
        {
            int time0;
            int.TryParse(time0box.Text, out time0);
            int timef;
            int.TryParse(timefbox.Text, out timef);
            int initial;
            int.TryParse(initialbox.Text, out initial);

            Process proc = new Process
            { 
                StartInfo =
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    FileName = @"calcApps\dsolvefunc\dsolvefunc2.exe",
                    Arguments = $"{equationbox.Text} {time0} {timef} {initial}"
                }
            };
            proc.Start();
            string output = proc.StandardOutput.ReadToEnd();
            //int y; // This is the result of subtraction and y coordinate of graph
            //int.TryParse(output, out y); // Convert string to int
            //chart1.Series["Series1"].Points.AddXY(x + 1, y); // plot
            //x = x + 1; // increment x coordinate of graph by 1
            proc.WaitForExit();
            Console.Write(output);
            MessageBox.Show(output);
        }
        
        // Microsoft ODE Solver
        private void OSLOButton_Click_1(object sender, EventArgs e)
        {
            var sol = Ode.RK547M(
                0,
                new Vector(5.0, 1.0), // initial conditions
                (t, x) => new Vector(
                     x[0] - x[0] * x[1], // Create equations to solve
                       -x[1] + x[0] * x[1]));
            var points = sol.SolveFromToStep(0, 20, 1).ToArray();
            foreach (var sp in points)
            {
                Console.WriteLine("{0}\t{1}", sp.T, sp.X); // output answers to console
                chart1.Series["Series1"].Points.AddXY(sp.T, sp.X[0]); // plot
                chart1.Series["Series2"].Points.AddXY(sp.T, sp.X[1]); // plot

            }


        }

        private void AddGearButton_Click_1(object sender, EventArgs e)
        {
            int teeth;
            int.TryParse(NumberOfTeethTxtBox.Text, out teeth);
            Program.Gear gear = new Program.Gear(teeth);
            gears.Add(gear);
            Console.WriteLine("{0}", gears.Count);
        }

        private void CalculateOutputButton_Click(object sender, EventArgs e)
        {
            float ratio = gears[0].num_teeth / gears[gears.Count - 1].num_teeth;
            if (gears.Count % 2 == 0)
            {
                ratio = ratio * -1;
            }
            RatioBox.Text = Convert.ToString(ratio);
            float velocityfloat;
            float.TryParse(VelocityIn.Text, out velocityfloat);
            OutputVelBox.Text = Convert.ToString(velocityfloat * ratio);

            // Open image
            DesignWindow frm = new DesignWindow();
            frm.Show();

        }

        private void AutoGearButton_Click(object sender, EventArgs e)
        {
            AutoGearForm frm = new AutoGearForm();
            frm.Show();
        }
    }
}
