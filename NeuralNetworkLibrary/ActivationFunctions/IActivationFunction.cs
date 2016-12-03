
namespace NeuralNetworkLibrary.ActivationFunctions
{
	public interface IActivationFunction
	{
		double Compute(double x);
		double ComputeDerivative(double x);
	}
}
