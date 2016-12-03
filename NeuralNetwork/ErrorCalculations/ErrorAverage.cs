using System;

namespace NeuralNetwork.ErrorCalculations
{
	public class ErrorAverage : IErrorCalculation
	{
		public double ComputeError(double[] values, double[] target)
		{
			double sum = 0.0;
			for (int i = 0; i < target.Length; i++)
			{
				sum += Math.Abs(target[i] - values[i]);
			}
			return sum / target.Length;
		}
	}
}
