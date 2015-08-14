using System;
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

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int x = 0;
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
            int y; // This is the result of subtraction and y coordinate of graph
            int.TryParse(output, out y); // Convert string to int
            chart1.Series["Series1"].Points.AddXY(x + 1, y); // plot
            x = x + 1; // increment x coordinate of graph by 1
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
                    FileName = @"calcApps\dsolvefunc\dsolvefunc.exe",
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
    }
}
