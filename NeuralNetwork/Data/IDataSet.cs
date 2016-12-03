using System;

namespace NeuralNetworkLibrary.Data
{
	public interface IDataSet : ICloneable
	{
		int Count();
		int CountRemaining();

		IDataRecord GetNextRecord();
		IDataRecord GetCurrentRecord();
		void Reset();

		Tuple<IDataSet, IDataSet> Split(double percentage);

		void Shuffle();
	}
}
