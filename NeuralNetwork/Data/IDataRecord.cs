using System;

namespace NeuralNetwork.Data
{
	public interface IDataRecord : ICloneable
	{
		double[] GetInputs();
		double[] GetOutputs();
		int NumberOfInputs();
		int NumberOfOutputs();
	}
}
