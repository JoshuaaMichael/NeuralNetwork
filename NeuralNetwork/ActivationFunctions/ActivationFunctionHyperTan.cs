using System;

namespace NeuralNetworkLibrary.ActivationFunctions
{
	public class ActivationFunctionHyperTan : IActivationFunction
	{
		public double Compute(double x)
		{
			if (x < -10.0)
			{
				return -1.0;
			}
			else if (x > 10.0)
			{
				return 1.0;
			}
			else
			{
				return Math.Tanh(x);
			}
		}

		public double ComputeDerivative(double x)
		{
			return (1 - x) * (1 + x);
		}
	}
}
