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

            int stageTrack = 0; // Keeps track of the number of stages

            // Convert values to floats
            float.TryParse(LengthText.Text, out length);
            float.TryParse(WidthText.Text, out width);
            float.TryParse(HeightText.Text, out height);
            float.TryParse(TorqueInText.Text, out torque_in);
            float.TryParse(TorqueOutText.Text, out torque_out);
            float.TryParse(DiametralText.Text, out diametral_pitch);

            // Get necessary overall gear train ratio
            double torque_ratio = torque_in / torque_out;

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
            /*if (factor_trigger == 0)
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
            */

            // FELICE'S PART

            // Creates new List to store all possible gear train ratios, beginning with the first gear train

            List<List<double>> stageList = new List<List<double>>();
            Console.WriteLine("{0} is the torque ratio", torque_ratio);
            List<double> firstTrain = FindFactors((int)torque_ratio);

            stageList.Add(firstTrain);

            // DEBUG TEST
            /*
            List<List<double>> stageList = new List<List<double>>();
            List<double> firstTrain = new List<double>();
            firstTrain.Add(2);
            firstTrain.Add(2);
            firstTrain.Add(3);
            firstTrain.Add(3);
            firstTrain.Add(5);

            stageList.Add(firstTrain);
            */

            // Counts the number of stages of the first stage. This will be used to only check for the current stage.
            stageTrack = stageList[0].Count;
            Console.WriteLine("Starting stageTrack: {0}", stageTrack);
            // Calculates all possible gear trains
            while (stageTrack > 0)
            {
                List<List<double>> tempStageList = new List<List<double>>();
                Console.WriteLine("Created new instance of tempStageList");
                stageList.ForEach(train => // For each geartrain in stage list,
                {
                    if (stageTrack == train.Count) // Find all geartrains that have the number of stages in stageTrack
                    {
                        Console.WriteLine("Found geartrain with specified number of stages, {0}", train.Count);
                        for (int trainIndex = 0; trainIndex < (train.Count - 1); trainIndex++) // Index tracker for the current train
                        {
                            Console.WriteLine("--Incremented trainIndex, {0}", trainIndex);
                            for (int i = 1; i < (train.Count - trainIndex); i++) // For each gear train with that number of stages, multiply two of the numbers, and add the new list to stageList
                            {
                                Console.WriteLine("---factor1 index: ({0}, {1}), factor2 index: ({2}, {3})", trainIndex, train.ElementAt(trainIndex), (trainIndex + i), train.ElementAt(trainIndex + i));
                                double numberToAdd = train[trainIndex] * train[trainIndex + i];
                                // 10:1 rule filter
                                if (numberToAdd > 10) // if the ratio is greater than 10
                                {
                                    // Do Nothing
                                }
                                else //if it is 10 or below, continue forming the gear train to add
                                {
                                    // Creates new train to add to stageList
                                    List<double> trainToAdd = new List<double>(train);

                                    // Removes the two numbers being multiplied and adds their product to the geartrain to be added
                                    Console.WriteLine("---Added {0} to train, removed {1} and {2}", numberToAdd, trainToAdd.ElementAt(trainIndex), trainToAdd.ElementAt(trainIndex + i));
                                    trainToAdd.RemoveAt(trainIndex + i);
                                    trainToAdd.RemoveAt(trainIndex);
                                    trainToAdd.Add(numberToAdd);
                                    trainToAdd.Sort();
                                    //Check to see if the train is already in the list
                                    if (checkIfInList(trainToAdd, tempStageList)) // If it is, then do nothing

                                    {
                                    }
                                    else // if it isn't, add it to tempStageList
                                    {
                                        tempStageList.Add(trainToAdd);
                                        Console.WriteLine("---Added train to tempStageList");
                                    }
                                }

                            }

                        }
                    }
                });

                // Add temporary list to stageList
                if (!tempStageList.Any()) // If tempStageList is empty
                {
                    // Do nothing
                }

                else //If it isn't, add tempStageList to StageList
                {
                    stageList.AddRange(tempStageList);
                    Console.WriteLine("TempStageList added to StageList");
                }

                // Filter duplicates
                stageList.ForEach(train => train.Sort()); // Sort every list in stageList
                stageList = stageList.Distinct().ToList(); // Remove Duplicates using .Distinct().ToList()

                Console.WriteLine("Current Stage List:");
                stageList.ForEach(train =>
                {
                    for (int i = 0; i < train.Count; i++)
                    {
                        Console.Write("{0}-", train[i]);
                    }
                    Console.WriteLine();
                });
                

                stageTrack--; // Decrements stageTrack
                Console.WriteLine("stageTrack = {0}", stageTrack);
            }

            // END OF FELICE'S PART

            //Console.WriteLine(Gear_Ratio_Master_List);

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


        // Return the number's prime factors. This method uses Lists as opposed to arrays
        private List<double> FindFactors(double num)
        {
            List<double> FactorList = new List<double>();

            // Take out the 2s.
            while (num % 2 == 0)
            {
                FactorList.Add(2);
                num /= 2;
            }

            // Take out other primes.
            double factor = 3;
            while (factor * factor <= num)
            {
                if (num % factor == 0)
                {
                    // This is a factor.
                    FactorList.Add(factor);
                    num /= factor;
                }
                else
                {
                    // Go to the next odd number.
                    factor += 2;
                }
            }

            // If num is not 1, then whatever is left is prime.
            if (num > 1) FactorList.Add(num);

            return FactorList;
        }

        private bool checkIfInList(List<double> listToCheck, List<List<double>> listOfLists)
        {

            for (int i = 0; i < listOfLists.Count; i++) {
                if (listOfLists[i].SequenceEqual(listToCheck))
                {
                    return true;
                }
            }
            return false;
        }
    }
}


