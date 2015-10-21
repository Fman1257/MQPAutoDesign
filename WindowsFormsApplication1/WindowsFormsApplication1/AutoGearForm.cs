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
    public partial class AutoGearForm : Form
    {
        public AutoGearForm()
        {
            InitializeComponent();
        }

        private void DesignButton_Click(object sender, EventArgs e)
        {
            // initiate values from user
            float length;
            float width;
            float height;
            float torque_in;
            float torque_out;
            float diametral_pitch;
            double[,,] master_factor_array = new double[2,20,2];
            int torque_ratio_converted = 0;
            List<List<double>> Gear_Ratio_Master_List = new List<List<double>>();

            // Convert values to floats
            float.TryParse(LengthText.Text, out length);
            float.TryParse(WidthText.Text, out width);
            float.TryParse(HeightText.Text, out height);
            float.TryParse(TorqueInText.Text, out torque_in);
            float.TryParse(TorqueOutText.Text, out torque_out);
            float.TryParse(DiametralText.Text, out diametral_pitch);

            // Get necessary overall gear train ratio
            double torque_ratio = torque_in / torque_out;

            // Calculate stage ratios

            // First take roots
            int root_counter = 0;
            double[] root_ratio_array = new double[12];
            while (root_counter <= 10) {
                root_ratio_array[root_counter] = Math.Pow(torque_ratio, root_counter + 1);
                root_counter = root_counter + 1;
            }

            // Convert non-integer values to fractions composed of integers
            // http://stackoverflow.com/questions/2751593/how-to-determine-if-a-decimal-double-is-an-integer
            int factor_trigger = 0;
            int integer_convert = 0;
            if (torque_ratio % 1 > 0.00000001)
            {
                factor_trigger = 1;
                double torque_ratio_double = 0;
                while (torque_ratio_double % 1 > 0.00000001)
                {
                    torque_ratio_double = torque_ratio * integer_convert;
                    integer_convert = integer_convert + 1;
                }
                torque_ratio_converted = Convert.ToInt32(torque_ratio_double); // Check?
            }
            // Now get prime factors
            if (factor_trigger == 0)
            {
                factoring(Convert.ToInt32(torque_ratio), master_factor_array, 0);
            }
            else if (factor_trigger == 1)
            {
                factoring(torque_ratio_converted, master_factor_array, 0);
                factoring(integer_convert, master_factor_array, 1);
            }

            // Analyze the factors
            factor_analysis(master_factor_array, Gear_Ratio_Master_List);
            factor_cleaner(Gear_Ratio_Master_List);
            Console.WriteLine(Gear_Ratio_Master_List);

        }
        // Get stage ratios from factoring
        //http://www.geeksforgeeks.org/print-all-prime-factors-of-a-given-number/
        private void factoring(int num, double[,,] master_factor_array, int master_index)
        {
            // setup
            int[] factor_array = new int[20];
            int counter = 0;
            int array_copy = 0;
            // get all 2 factors while even
            while (num%2 == 0) {
                factor_array[counter] = 2;
                num = num / 2;
                counter = counter + 1;
                Console.WriteLine(2);
            }
            // now that it's odd, get other factors
            for (int inc = 3; inc <= Math.Sqrt(num); inc = inc + 2)
            {
                while (num%inc == 0)
                {
                    factor_array[counter] = inc;
                    num = num / inc;
                    counter = counter + 1;
                    Console.WriteLine(inc);
                }
            }
            // include any remaining factors
            if (num > 2)
            {
                factor_array[counter] = num;
                counter = counter + 1;
                Console.WriteLine(num);
            } 
            // copy array to master array
            if (master_index == 0)
            {
                while (array_copy <= counter - 1)
                {
                    master_factor_array[master_index, array_copy,0] = factor_array[array_copy];
                    array_copy = array_copy + 1;
                }
            }
            if (master_index == 1)
            {
                while (array_copy <= counter - 1)
                {
                    master_factor_array[master_index, array_copy,0] = Math.Pow(factor_array[array_copy],-1);
                    array_copy = array_copy + 1;
                }
            }

        }

        // Analyze factors for stage combinations ADD TO GET BASE PRIMES
        private void factor_analysis(double[,,] master_factor_array, List<List<double>> Gear_Ratio_Master_List)
        {
            int head = 0;
            int tail = head + 1;
            //List<double> Gear_Ratio_List = new List<double>();
            // Count non-zero values in array
            int counter = 0;
            while (master_factor_array[0,counter,0] != 0)
            {
                //Gear_Ratio_List.Add(master_factor_array[0, counter, 0]);
                counter = counter + 1; // Counter is number of non-zero values, counter - 1 is array position
            }
           //Gear_Ratio_List.Sort();
           //Gear_Ratio_Master_List.Add(Gear_Ratio_List);
            int add_remaining = 0;
            while (head < counter - 1)
            {
                while (tail < counter)
                {
                    //Gear_Ratio_List.Clear();
                    List<double> Gear_Ratio_List = new List<double>();
                    Gear_Ratio_List.Add(master_factor_array[0, head, 0] * master_factor_array[0, tail, 0]);
                    master_factor_array[0, head, 1] = 1;
                    master_factor_array[0, tail, 1] = 1;
                    add_remaining = 0;
                    while (add_remaining < counter)
                    {
                        if (master_factor_array[0, add_remaining, 1] != 1)
                        {
                            Gear_Ratio_List.Add(master_factor_array[0, add_remaining, 0]);
                            master_factor_array[0, add_remaining, 1] = 1;
                            add_remaining = add_remaining + 1;
                        }
                        else
                        {
                            add_remaining = add_remaining + 1;
                        }
                    }
                    int remove_markers = 0;
                    while (remove_markers < counter)
                    {
                        master_factor_array[0, remove_markers, 1] = 0;
                        remove_markers = remove_markers + 1;
                    }
                    tail = tail + 1;
                    Gear_Ratio_List.Sort();
                    int ten_test = 0;
                    int invalid = 0;
                    while (ten_test < Gear_Ratio_List.Count())
                    {
                        if (Gear_Ratio_List[ten_test] > 10)
                        {
                            invalid = 1;
                        }
                        ten_test = ten_test + 1;
                    }
                    if (invalid != 1)
                    {
                        Gear_Ratio_Master_List.Add(Gear_Ratio_List);
                    }
                }
                head = head + 1;
                tail = head + 1;
            }

        }

        private void factor_cleaner (List<List<double>> Gear_Ratio_Master_List)
        {
            int current = 0;
            while (current < Gear_Ratio_Master_List.Count())
            {
                int y = 0;
                int x = 0;
                int stop = 0;
                int match = 0;
               
                while (y < Gear_Ratio_Master_List.Count)
                {
                    if (y != current && stop != 1)
                    {
                        x = 0;
                        match = 0;
                        while (x < Gear_Ratio_Master_List[y].Count)
                        {
                            if (Gear_Ratio_Master_List[current][x] == Gear_Ratio_Master_List[y][x])
                            {
                                match = match + 1;
                            }
                            x = x + 1;
                        }
                        if (match == Gear_Ratio_Master_List[current].Count)
                        {
                            Gear_Ratio_Master_List[current].Clear();
                            //Gear_Ratio_Master_List.RemoveAt(current);
                            stop = 1;
                        }
                    }
                    y = y + 1;
                }
                stop = 0;
                current = current + 1;
            }
            int clear = 0;
            while (clear < Gear_Ratio_Master_List.Count)
            {
                if (Gear_Ratio_Master_List[clear].Any() != true)
                {
                    Gear_Ratio_Master_List.RemoveAt(clear);
                    clear = clear - 1;
                }
                clear = clear + 1;
            }
        }
    }
}
