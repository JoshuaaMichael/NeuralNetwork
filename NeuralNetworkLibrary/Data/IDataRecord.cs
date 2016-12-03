using System;

namespace NeuralNetworkLibrary.Data
{
	public interface IDataRecord : ICloneable
	{
		double[] GetInputs();
		double[] GetOutputs();
		int NumberOfInputs();
		int NumberOfOutputs();
	}
}
