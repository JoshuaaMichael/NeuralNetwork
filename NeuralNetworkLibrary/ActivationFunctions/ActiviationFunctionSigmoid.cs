using System;

namespace NeuralNetworkLibrary.ActivationFunctions
{
	public class ActivationFunctionSigmoid : IActivationFunction
	{
		public double Compute(double x)
		{
			return 1 / (1 + Math.Exp(-x));
		}

		public double ComputeDerivative(double x)
		{
			double s = 1 / (1 + Math.Exp(-x)); //Sigmoid above
			return s * (1 - s);
		}
	}
}
