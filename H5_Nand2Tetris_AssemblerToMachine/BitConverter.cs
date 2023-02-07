using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace H5_Nand2Tetris_AssemblerToMachine
{
    internal class BitConverter
    {
        public static int[] ConvertToBitArray(int value)
        {
            string s = Convert.ToString(value, 2); //Convert to binary in a string

            int[] bits = s.PadLeft(16, '0') // Add 0's from left
                         .Select(c => int.Parse(c.ToString())) // convert each char to int
                         .ToArray(); // Convert IEnumerable from select to Array
            return bits;
        }
    }
}
