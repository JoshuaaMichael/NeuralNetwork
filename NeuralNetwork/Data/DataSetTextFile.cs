using System;
using System.Collections.Generic;
using System.IO;
using NeuralNetwork.Exceptions;

namespace NeuralNetwork.Data
{
	public abstract class DataSetTextFile : DataSet
	{
		protected string filename;

		protected DataSetTextFile() { }

		public DataSetTextFile(string filename)
		{
			this.filename = filename;
		}

		public DataSetTextFile(List<IDataRecord> dataRecords)
			: base(dataRecords) { }

		public override void ImportData()
		{
			int lineNumber = 0;
			int inputCount = 0;
			int outputCount = 0;

			StreamReader reader = new StreamReader(File.OpenRead(filename));

			while (!reader.EndOfStream)
			{
				string settingsLine = reader.ReadLine().Trim();
				lineNumber += 1;
				if (settingsLine.Length == 0 || settingsLine.StartsWith("#"))
				{
					continue;
				}
				else
				{
					string[] settings = settingsLine.Split(',');
					inputCount = int.Parse(settings[0].Trim());
					outputCount = int.Parse(settings[1].Trim());
					break;
				}
			}

			if (inputCount < 1 || outputCount < 1)
			{
				throw new InvalidDataFileException("The data file " + filename + " does not have valid input count or output counts");
			}

			while (!reader.EndOfStream)
			{
				string line = reader.ReadLine().Trim();
				lineNumber += 1;
				if (line.Trim().Length == 0 || line.StartsWith("#"))
				{
					continue;
				}
				if (line.Split(',').Length != inputCount + outputCount)
				{
					dataRecords.Clear();
					throw new InvalidDataFileException("The data file " + filename + " has an invalid line on line " + lineNumber.ToString());
				}
				Tuple<double[], double[]> record = HandleLine(line, inputCount, outputCount);
				DataRecord dataRow = new DataRecord(record.Item1, record.Item2);
				dataRecords.Add(dataRow);
			}
			if (dataRecords.Count == 0)
			{
				throw new InvalidDataFileException("The data file " + filename + " contains no records");
			}
		}

		protected virtual Tuple<double[], double[]> HandleLine(string line, int numberOfInputs, int numberOfOutputs)
		{
			Tuple<double[], string> inputResults = ParseInputs(line, numberOfInputs); //Values and reset of line

			double[] outputResults = ParseOutput(inputResults.Item2, numberOfOutputs);

			return new Tuple<double[], double[]>(inputResults.Item1, outputResults);
		}

		protected abstract Tuple<double[], string> ParseInputs(string line, int numberOfInputs);

		protected abstract double[] ParseOutput(string line, int numberOfOutputs);
	}
}
