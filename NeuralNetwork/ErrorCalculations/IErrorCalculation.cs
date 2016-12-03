
namespace NeuralNetworkLibrary.ErrorCalculations
{
	public interface IErrorCalculation
	{
		double ComputeError(double[] values, double[] target);
	}
}
