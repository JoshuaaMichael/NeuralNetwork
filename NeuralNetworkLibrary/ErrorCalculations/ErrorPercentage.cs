using System;

namespace NeuralNetworkLibrary.ErrorCalculations
{
	public class ErrorPercentage : IErrorCalculation
	{
		public double ComputeErrorOld(double[] values, double[] target)
		{
			double absoluteSum = 0.0;
			double trueSum = 0.0;
			for (int i = 0; i < target.Length; i++)
			{
				absoluteSum += Math.Abs(target[i] - values[i]);
				trueSum += target[i];
			}
			return absoluteSum / trueSum;
		}

		public double ComputeError(double[] values, double[] target)
		{
			double sum = 0;
			for(int i = 0; i < values.Length; i++)
			{
				sum += Math.Abs(values[i] - target[i]) / Math.Abs(values[i]);
			}
			return sum / values.Length;
		}
	}
}
