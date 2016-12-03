using System;
using System.Collections.Generic;
using System.IO;
using LumenWorks.Framework.IO.Csv;
using NeuralNetworkLibrary.Exceptions;

namespace NeuralNetworkLibrary.Data
{
	public class DataSetCSV : DataSetFile
	{
		protected int csvColumnCount = -1;

		protected DataSetCSV() { }

		public DataSetCSV(string filename, int inputCount, int outputCount)
			: base(filename, inputCount, outputCount) { }

		public DataSetCSV(string filename, int inputCount, int outputCount, int csvColumnCount)
			: base(filename, inputCount, outputCount)
		{
			this.csvColumnCount = csvColumnCount;
		}

		public override void ImportData()
		{
			using (CsvReader csv = new CsvReader(new StreamReader(filename), false))
			{
				csv.SkipEmptyLines = true;
				if(csv.FieldCount != csvColumnCount && csv.FieldCount != inputCount + outputCount)
				{
					throw new InvalidDataFileException("Number of fields in data file does not match required");
				}

				while (csv.ReadNextRecord())
				{
					string[] values = new string[csv.FieldCount];
					csv.CopyCurrentRecordTo(values);
					HandleLine(values);
				}
			}
		}

		protected virtual Tuple<double[], double[]> HandleLine(string[] values)
		{
			double[] inputValues = ParseInputs(values);

			double[] outputValues = ParseOutput(values);

			return new Tuple<double[], double[]>(inputValues, outputValues);
		}

		protected virtual double[] ParseInputs(string[] values)
		{
			double[] inputValues = new double[inputCount];

			for (int i = 0; i < inputCount; i++)
			{
				int result;
				if(!int.TryParse(values[i], out result))
				{
					throw new InvalidDataFileException("Value found that does not parse to double");
				}
				inputValues[i] = result;
			}

			return inputValues;
		}

		protected virtual double[] ParseOutput(string[] values)
		{
			double[] outputValues = new double[outputCount];

			for (int i = inputCount; i < inputCount + outputCount; i++)
			{
				int result;
				if (!int.TryParse(values[i], out result))
				{
					throw new InvalidDataFileException("Value found that does not parse to double");
				}
				outputValues[i] = result;
			}

			return outputValues;
		}

		public override object Clone()
		{
			DataSetCSV dscsv = new DataSetCSV();
			IEnumerable<IDataRecord> idr = new List<IDataRecord>();

			foreach (IDataRecord dr in dataRecords)
			{
				dscsv.dataRecords.Add((IDataRecord)dr.Clone());
			}

			dscsv.filename = filename;
			dscsv.currentIndex = currentIndex;
			dscsv.csvColumnCount = csvColumnCount;
			return dscsv;
		}
	}
}
