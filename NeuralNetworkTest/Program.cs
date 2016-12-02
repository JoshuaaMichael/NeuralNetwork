using NeuralNetwork.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//I think a CSV reading library would be better

namespace NeuralNetworkTest
{
	class Program
	{
		static void Main(string[] args)
		{
			int numberInputs = 5, numberOutputs = 4;
			int numberOfRecords = 100;
			double count = 1.0125;

			List<string> fileGen = new List<string>();

			for(int i = 0; i < numberOfRecords; i++)
			{
				string line = "";
				for (int j = 0; j < numberInputs; j++)
				{
					line += count++ + ", ";
				}
				for (int j = 0; j < numberOutputs; j++)
				{
					line += (j != numberInputs - 1) ? count++ + ", " : count++ + "";
				}
				fileGen.Add(line);
			}

			File.WriteAllLines("generated.txt", fileGen.ToArray());
		}
	}
}
