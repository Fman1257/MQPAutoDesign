using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Research.Oslo;

namespace WindowsFormsApplication1
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 


        // Create a class to contain properties for a gear
        public partial class Gear
        {
            // Properties
            public int num_teeth;
            public float pitch_diameter;
            public float key_hole_size;
            public float diametral_pitch;
            public float torque;
            public List<Gear> Input_Gears;
            public List<Gear> Output_Gears;

            // Constructor for new gear
            public Gear(int teeth)
            {
                num_teeth = teeth;
                diametral_pitch = num_teeth / pitch_diameter;
            }

            void Add_Gear(Gear gear)
            {
                gear.Input_Gears.Add(this);
                this.Output_Gears.Add(gear);
            }

        }

        // Create a class to contain propereties for an axle
        public class Axle
        {
            // Properties
            public float length;
            public float diameter;
            public float mass;
            public float torque;

            // Constructor for new axle
            public Axle(float length_in, float diameter_in, float mass_in)
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
