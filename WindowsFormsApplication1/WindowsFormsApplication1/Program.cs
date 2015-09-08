using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Research.Oslo;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 


        // Create a class to contain properties for a gear
        public class Gear
        {
            // Properties
            public int num_teeth;
            public int pitch_diameter;
            public int key_hole_size;
            public float diametral_pitch;

            // Constructor for new gear
            public Gear(int teeth, int diameter, int hole)
            {
                num_teeth = teeth;
                pitch_diameter = diameter;
                key_hole_size = hole;
                diametral_pitch = num_teeth / pitch_diameter;
            }
        }

        // Create a class to contain propereties for an axle
        public class Axle
        {
            // Properties
            public int length;
            public int diameter;
            public int mass;

            // Constructor for new axle
            public Axle(int length_in, int diameter_in, int mass_in)
            {
                length = length_in;
                diameter = diameter_in;
                mass = mass_in;
            }
        }
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
