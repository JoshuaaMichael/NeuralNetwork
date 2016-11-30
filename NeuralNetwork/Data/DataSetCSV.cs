using System;
using System.Collections.Generic;
using NeuralNetwork.Exceptions;

namespace NeuralNetwork.Data
{
	public class DataSetCSV : DataSetTextFile
	{
		private DataSetCSV() { }

		public DataSetCSV(string filename)
			: base(filename) { }

		public DataSetCSV(List<IDataRecord> dataRecords)
			: base(dataRecords) { }

		protected override Tuple<double[], string> ParseInputs(string line, int numberOfInputs)
		{
			string[] values = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

			if (numberOfInputs > values.Length)
			{
				throw new InvalidDataFileException("Tried to get element which didn't exist on line");
			}

			double[] doubleValues = new double[numberOfInputs];
			for (int i = 0; i < numberOfInputs; i++)
			{
				doubleValues[i] = double.Parse(values[i].Trim());
			}

			int indexUsed = GetNthIndex(line, ',', numberOfInputs) + 1; //Get index of where rest of line starts

			string restOfLine = line.Substring(indexUsed);

			return new Tuple<double[], string>(doubleValues, restOfLine);
		}

		protected override double[] ParseOutput(string line, int numberOfOutputs)
		{
			string[] values = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

			if (numberOfOutputs > values.Length)
			{
				throw new InvalidDataFileException("Tried to get element which didn't exist on line");
			}

			double[] doubleValues = new double[numberOfOutputs];
			for (int i = 0; i < numberOfOutputs; i++)
			{
				doubleValues[i] = double.Parse(values[i].Trim());
			}

			return doubleValues;
		}

		private int GetNthIndex(string str, char value, int count)
		{
			int occurance = 0;
			for (int i = 0; i < str.Length; i++)
			{
				if (str[i] == value)
				{
					occurance++;
					if (occurance == count)
					{
						return i;
					}
				}
			}
			return -1;
		}

		public override object Clone()
		{
			DataSetCSV dscsv = new DataSetCSV();
			List<IDataRecord> idr = new List<IDataRecord>();

			foreach (IDataRecord dr in dataRecords)
			{
				dscsv.dataRecords.Add((IDataRecord)dr.Clone());
			}

			dscsv.filename = filename; //Need to implement because abstract base can't
			dscsv.currentIndex = currentIndex;
			return dscsv;
		}
	}
}
