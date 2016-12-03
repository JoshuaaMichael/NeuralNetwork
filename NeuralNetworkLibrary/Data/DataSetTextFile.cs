using System;
using System.IO;

namespace NeuralNetworkLibrary.Data
{
	public abstract class DataSetTextFile : DataSetFile
	{
		protected DataSetTextFile() { }

		public DataSetTextFile(string filename, int inputCount, int outputCount)
			: base(filename, inputCount, outputCount) { }

		public override void ImportData()
		{
			using (StreamReader reader = new StreamReader(File.OpenRead(filename)))
			{
				while (!reader.EndOfStream)
				{
					string line = reader.ReadLine().Trim();
					if (line.Length == 0 || line.StartsWith("#"))
					{
						continue;
					}
					Tuple<double[], double[]> record = HandleLine(line);
					dataRecords.Add(new DataRecord(record.Item1, record.Item2));
				}
			}
		}

		protected virtual Tuple<double[], double[]> HandleLine(string line)
		{
			Tuple<double[], string> inputResults = ParseInputs(line); //Values and reset of line

			double[] outputResults = ParseOutput(inputResults.Item2);

			return new Tuple<double[], double[]>(inputResults.Item1, outputResults);
		}

		protected abstract Tuple<double[], string> ParseInputs(string line);

		protected abstract double[] ParseOutput(string line);
	}
}
