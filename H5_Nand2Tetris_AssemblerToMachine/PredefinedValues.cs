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
        public Dictionary<string, string> aValues = new Dictionary<string, string>();
        public Dictionary<string, string> destInstruction = new Dictionary<string, string>()
        {
            {"null", "000" },
            {"M",    "001"  },
            {"D",    "010"  },
            {"MD",   "011"  },
            {"A",    "100"  },
            {"AM",   "101"  },
            {"AD",   "110"  },
            {"AMD",  "111"  },

        };
        public Dictionary<string, string> jumpInstructions = new Dictionary<string, string>()
        {
            {"null", "000" },
            {"JGT", "001" },
            {"JEQ", "010" },
            {"JGE", "011" },
            {"JLT", "100" },
            {"JNE", "101" },
            {"JLE", "110" },
            {"JMP", "111" },
        };
        public Dictionary<string, string> compInstructions = new Dictionary<string, string>()
        {
            {"0", "0101010" },
            {"1", "0111111" },
            {"-1", "0111010" },
            {"D", "0001100" },
            {"A", "0110000" },
            {"M", "1110000" },
            {"!D", "0001101" },
            {"!A", "0110001" },
            {"!M", "1110001" },
            {"D+1", "0011111" },
            {"A+1", "0110111" },
            {"M+1", "1110111" },
            {"D-1", "0001110" },
            {"A-1", "0110010" },
            {"M-1", "1110010" },
            {"D+A", "0000010" },
            {"D+M", "1000010" },
            {"D-A", "0010011" },
            {"D-M", "1010011" },
            {"A-D", "0000111" },
            {"M-D", "1000111" },
            {"D&A", "0000000" },
            {"D&M", "1000000" },
            {"D|A", "0010101" },
            {"D|M", "1010101" }
        };

        public PredefinedValues()
        {
            for (int i = 0; i < 15; i++)
            {
                aValues.Add($"R{i}", BitConverter.ConvertToBitString(i));
            }


            aValues.Add("SCREEN", BitConverter.ConvertToBitString(16384));
            aValues.Add("KBD", BitConverter.ConvertToBitString(24576));
            aValues.Add("SP", BitConverter.ConvertToBitString(0));
            aValues.Add("LCL", BitConverter.ConvertToBitString(1));
            aValues.Add("ARG", BitConverter.ConvertToBitString(2));
            aValues.Add("THIS", BitConverter.ConvertToBitString(3));
            aValues.Add("THAT", BitConverter.ConvertToBitString(4));
        }
    }
}
