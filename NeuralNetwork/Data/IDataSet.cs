using System;

namespace NeuralNetwork.Data
{
	public interface IDataSet : ICloneable
	{
		void ImportData();

		int Count();
		int CountRemaining();

		IDataRecord GetNextRecord();
		IDataRecord GetCurrentRecord();
		void Reset();

		Tuple<IDataSet, IDataSet> Split(double percentage);

		void Shuffle();
	}
}
