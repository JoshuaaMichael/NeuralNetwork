using NeuralNetworkLibrary.Data;
using NeuralNetworkLibrary.Exceptions;

namespace NeuralNetworkLibraryExamples.Data
{
	class DataSetCsvIris : DataSetCSV
	{
		public DataSetCsvIris(string filename)
			: base(filename, 4, 3, 5) { }

		protected override double[] ParseOutput(string[] values)
		{
			string value = values[values.Length - 1].Trim();

			if (value == "Iris-setosa")
				return new double[] { 1.0, 0.0, 0.0 };
			else if (value == "Iris-versicolor")
				return new double[] { 0.0, 1.0, 0.0 };
			else if (value == "Iris-virginica")
				return new double[] { 0.0, 0.0, 1.0 };
			else
				throw new InvalidDataFileException("Unhandled type of data in file");
		}
	}
}
