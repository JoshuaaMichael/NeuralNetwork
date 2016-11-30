using System;

namespace NeuralNetwork.Data
{
	public interface IDataRecord : ICloneable
	{
		double[] GetInput();
		double[] GetOutput();
		int NumberOfInput();
		int NumberOfOutput();
	}
}
