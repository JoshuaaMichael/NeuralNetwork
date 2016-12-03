
namespace NeuralNetwork.Data
{
	public abstract class DataSetFile : DataSet, IDataSetImportableData
	{
		protected string filename;
		protected int inputCount;
		protected int outputCount;

		protected DataSetFile() { }

		public DataSetFile(string filename, int inputCount, int outputCount)
		{
			this.filename = filename;
			this.inputCount = inputCount;
			this.outputCount = outputCount;
		}

		public abstract override void ImportData();
	}
}
