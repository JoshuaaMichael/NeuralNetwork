using System;

namespace NeuralNetworkLibrary.ErrorCalculations
{
	public class ErrorRootMeanSquared : IErrorCalculation
	{
		public double ComputeError(double[] values, double[] target)
		{
			double sum = 0.0;
			for (int i = 0; i < target.Length; i++)
			{
				sum += (target[i] - values[i]) * (target[i] - values[i]);
			}
			return Math.Sqrt(sum / target.Length);
		}
	}
}
