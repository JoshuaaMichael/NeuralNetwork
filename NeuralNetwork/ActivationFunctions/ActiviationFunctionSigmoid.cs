using System;

namespace NeuralNetwork.ActivationFunctions
{
	public class ActiviationFunctionSigmoid : IActivationFunction
	{
		public double Compute(double x)
		{
			if (x < -45.0)
			{
				return 0.0;
			}
			else if (x > 45.0)
			{
				return 1.0;
			}
			else
			{
				return 1.0 / (1.0 + Math.Exp(-x));
			}
		}

		public double ComputeDerivative(double x)
		{
			return (1 - x) * x;
		}
	}
}
