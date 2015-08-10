using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

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
            // Create the MATLAB instance 
            MLApp.MLApp matlab = new MLApp.MLApp();

            // Change to the directory where the function is located 
            matlab.Execute(@"cd c:\Windows\Temp");

            // Define the output 
            object result = null;

            // Call the MATLAB function myfunc
            matlab.Execute("syms a x(t)");
            result = matlab.Execute("dsolve(diff(x) == -a*x)");

            Console.WriteLine(result);
            MessageBox.Show(result.ToString());
        }
    }
}
