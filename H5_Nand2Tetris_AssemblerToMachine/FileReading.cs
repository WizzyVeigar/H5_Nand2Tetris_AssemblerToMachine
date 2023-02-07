using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace H5_Nand2Tetris_AssemblerToMachine
{
    internal class FileReading
    {
        PredefinedValues predefinedValues = new PredefinedValues();
        StringBuilder machineString = new StringBuilder();
        public void ReadFile(string filename)
        {
            if (machineString.Length > 0)
            {
                machineString.Clear();
            }

            //string[] testing = new string[]
            //{
            //    "(START)",
            //    "@21",
            //    "D=A",
            //    "@TEMP",
            //    "M=D",
            //    "@START",
            //    "0;JMP"
            //};

            if (File.Exists(filename))
            {
                string[] file = File.ReadAllLines(filename);
                ConvertFileToMachine(file);
            }


            File.WriteAllText(Environment.CurrentDirectory + "/output.hack", machineString.ToString());
        }

        /// <summary>
        /// Remove all comments from file, so it won't mess with convertion
        /// </summary>
        /// <param name="file"></param>
        private void RemoveCommentsFromFile(string[] file)
        {
            for (int i = 0; i < file.Length; i++)
            {
                if (file[i].Contains("//"))
                {
                    int index = file[i].IndexOf("//");
                    if (index >= 0)
                    {
                        file[i] = file[i].Substring(0, index);
                    }
                }
            }
        }

        /// <summary>
        /// Add labels to the list of predefined elements
        /// </summary>
        /// <param name="file"></param>
        private void AddLabelsToPredifinedValues(string[] file)
        {
            int lineCounter = 0;
            for (int i = 0; i < file.Length; i++)
            {
                if (!string.IsNullOrEmpty(file[i]))
                {

                    if (file[i].Contains('('))
                    {
                        string loopword = file[i].Split('(', ')')[1];
                        if (!predefinedValues.aValues.ContainsKey(loopword))
                        {
                            predefinedValues.aValues.Add(loopword, BitConverter.ConvertToBitString(lineCounter));
                            file[i] = "";
                            continue;
                        }
                    }
                    lineCounter++;
                }
            }
        }

        /// <summary>
        /// Add variables used to the predefined list
        /// </summary>
        /// <param name="file"></param>
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
                            predefinedValues.aValues.Add(variable, BitConverter.ConvertToBitString(varCounter));
                            varCounter++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Loop through all lines and convert each line into a bit string
        /// </summary>
        /// <param name="file"></param>
        private void ConvertFileToMachine(string[] file)
        {
            //They could be here or in the ReadFile()
            //but it makes sense here, as you want these to be ran, before ConvertFileToMachine()

            RemoveCommentsFromFile(file);
            AddLabelsToPredifinedValues(file);
            AddVariablesToPredifinedValues(file);



            for (int i = 0; i < file.Length; i++)
            {
                if (!string.IsNullOrEmpty(file[i]))
                {

                    //If A instruction
                    if (file[i].Contains('@'))
                    {
                        string variableOrNum = file[i].Split('@', ' ')[1];
                        if (predefinedValues.aValues.ContainsKey(variableOrNum))
                        {
                            machineString.AppendLine(string.Join("", predefinedValues.aValues[variableOrNum]));
                        }
                        else
                        {
                            machineString.AppendLine(string.Join("", BitConverter.ConvertToBitString(Convert.ToInt32(variableOrNum))));
                        }
                    }
                    //else C instruction
                    else
                    {
                        string dest, comp, jump;

                        machineString.Append(111);

                        if (file[i].Contains('='))
                        {
                            dest = file[i].Split(' ', '=')[0];
                        }
                        else
                            dest = "null";


                        if (file[i].Contains(';'))
                        {
                            jump = file[i].Split(';', ' ')[1];
                        }
                        else
                            jump = "null";

                        if (dest == "null" && jump != "null")
                            comp = file[i].Split(' ', ';')[0];

                        else if (dest != "null" && jump == "null")
                            comp = file[i].Split('=', ' ')[1];
                        else
                            comp = file[i].Split('=', ';')[1];


                        comp = string.Join("", predefinedValues.compInstructions[comp]);
                        dest = string.Join("", predefinedValues.destInstruction[dest]);
                        jump = string.Join("", predefinedValues.jumpInstructions[jump]);
                        machineString.Append(comp);
                        machineString.Append(dest);
                        machineString.AppendLine(jump);
                    }
                }
            }
        }
    }
}
