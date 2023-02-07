using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace H5_Nand2Tetris_AssemblerToMachine
{
    //Hello Nanna :)
    internal class FileReading
    {
        PredefinedValues predefinedValues = new PredefinedValues();
        public void ReadFile(string filename)
        {
            string[] testing = new string[]
            {
                "@21",
                "D=A",
                "@TEMP",
                "M=D",
                "@0",
                "0;JMP"
            };

            AddLabelsToPredifinedValues(testing);
            AddVariablesToPredifinedValues(testing);

            StringBuilder machineString = new StringBuilder();

            for (int i = 0; i < testing.Length; i++)
            {
                //If A instruction
                if (testing[i].Contains('@'))
                {
                    string variableOrNum = testing[i].Split('@', ' ')[1];
                    if (predefinedValues.aValues.ContainsKey(variableOrNum))
                    {
                        machineString.Append(predefinedValues.aValues[variableOrNum]);
                    }
                    else
                    {
                        machineString.Append(BitConverter.ConvertToBitArray(Convert.ToInt32(variableOrNum)));
                    }
                }
                //else C instruction
                else
                {
                    string dest, comp, jump;

                    machineString.Append(111);

                    if (testing[i].Contains('='))
                    {
                        dest = testing[i].Split(' ', '=')[1];
                    }
                    else
                        dest = "000";


                    if (testing[i].Contains(';'))
                    {
                        jump = testing[i].Split(';', ' ')[1];
                    }
                    else
                        jump = "000";

                    if (dest == "000" && jump != "000")
                        comp = testing[i].Split(' ', ';')[1];

                    else if (dest != "000" && jump == "000")
                        comp = testing[i].Split('=', ' ')[1];
                    else
                        comp = testing[i].Split('=', ';')[1];

                    machineString.Append(predefinedValues.aValues[comp]);
                    machineString.Append(predefinedValues.destInstruction[dest]);
                    machineString.Append(predefinedValues.jumpInstructions[jump]);
                }
            }

            //if (File.Exists(filename))
            //{
            //    string[] file = File.ReadAllLines(filename);
            //    AddLabelsToPredifinedValues(file);
            //    AddVariablesToPredifinedValues(file)
            //}
        }

        private void AddLabelsToPredifinedValues(string[] file)
        {
            for (int i = 0; i < file.Length; i++)
            {
                if (file[i].Contains('('))
                {
                    string loopword = file[i].Split('(', ')')[1];
                    if (!predefinedValues.aValues.ContainsKey(loopword))
                    {
                        predefinedValues.aValues.Add(loopword, BitConverter.ConvertToBitArray(i + 1));
                    }
                }
            }
        }

        private void AddVariablesToPredifinedValues(string[] file)
        {
            int varCounter = 16;
            for (int i = 0; i < file.Length; i++)
            {
                //check if what is after @ symbol is not a digit
                if (file[i].Contains('@'))
                {
                    if (!char.IsDigit(file[i][file[i].IndexOf('@') + 1]))
                    {
                        string variable = file[i].Split('@', ' ')[1];
                        if (!predefinedValues.aValues.ContainsKey(variable))
                        {
                            predefinedValues.aValues.Add(variable, BitConverter.ConvertToBitArray(varCounter));
                            varCounter++;
                        }
                    }
                }
            }
        }
    }
}
