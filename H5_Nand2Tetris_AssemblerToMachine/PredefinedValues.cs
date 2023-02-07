using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H5_Nand2Tetris_AssemblerToMachine
{
    internal class PredefinedValues
    {
        /// <summary>
        /// Holds predefined A instruction values
        /// </summary>
        public Dictionary<string, int[]> aValues = new Dictionary<string, int[]>();
        public Dictionary<string, int[]> destInstruction = new Dictionary<string, int[]>()
        {
            {"null", new int[]{000} },
            {"M", new int[]{001} },
            {"D", new int[]{010} },
            {"MD", new int[]{011} },
            {"A", new int[]{100} },
            {"AM", new int[]{101} },
            {"AD", new int[]{110} },
            {"AMD", new int[]{111} },

        };
        public Dictionary<string, int[]> jumpInstructions = new Dictionary<string, int[]>()
        {
            {"null", new int[]{000} },
            {"JGT", new int[]{001} },
            {"JEQ", new int[]{010} },
            {"JGE", new int[]{011} },
            {"JLT", new int[]{100} },
            {"JNE", new int[]{101} },
            {"JLE", new int[]{110} },
            {"JMP", new int[]{111} },
        };
        public Dictionary<string, int[]> compInstructions = new Dictionary<string, int[]>()
        {
            {"0", new int[]{0101010 } },
            {"1", new int[]{0111111} },
            {"-1", new int[]{0111010 } },
            {"D", new int[]{0001100 } },
            {"A", new int[]{0110000 } },
            {"M", new int[]{1110000 } },
            {"!D", new int[]{0001101 } },
            {"!A", new int[]{0110001 } },
            {"!M", new int[]{1110001 } },
            {"D+1", new int[]{0011111} },
            {"A+1", new int[]{0110111 } },
            {"M+1", new int[]{1110111 } },
            {"D-1", new int[]{0001110 } },
            {"A-1", new int[]{0110010} },
            {"M-1", new int[]{1110010} },
            {"D+A", new int[]{0000010} },
            {"D+M", new int[]{1000010} },
            {"D-A", new int[]{0010011} },
            {"D-M", new int[]{1010011} },
            {"A-D", new int[]{0000111} },
            {"M-D", new int[]{1000111} },
            {"D&A", new int[]{0000000} },
            {"D&M", new int[]{1000000} },
            {"D|A", new int[]{0010101} },
            {"D|M", new int[]{1010101} }
        };

        public PredefinedValues()
        {
            for (int i = 0; i <= 15; i++)
            {
                aValues.Add($"R{i}", BitConverter.ConvertToBitArray(i));
            }


            aValues.Add("SCREEN", BitConverter.ConvertToBitArray(16384));
            aValues.Add("KBD", BitConverter.ConvertToBitArray(24576));
            aValues.Add("SP", BitConverter.ConvertToBitArray(0));
            aValues.Add("LCL", BitConverter.ConvertToBitArray(1));
            aValues.Add("ARG", BitConverter.ConvertToBitArray(2));
            aValues.Add("THIS", BitConverter.ConvertToBitArray(3));
            aValues.Add("THAT", BitConverter.ConvertToBitArray(4));
        }
    }
}
