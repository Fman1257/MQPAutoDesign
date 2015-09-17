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
    public partial class DesignWindow : Form
    {
        int x_coord =  50;
        int size = 150;
        public DesignWindow()
        {
            InitializeComponent();
        }

        public void DrawGear(int x_coord, int size)
        {
            System.Drawing.Graphics graphics = this.CreateGraphics();
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(
                x_coord, 100 - size/2, size, size);
            graphics.DrawEllipse(System.Drawing.Pens.Black, rectangle);
        }

        private void DrawButton_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < Form1.gears.Count; x++) {
                size = Form1.gears[x].num_teeth * 10;
                DrawGear(x_coord, size);
                x_coord = x_coord + size;

            }
           
            
        }

    }
}
