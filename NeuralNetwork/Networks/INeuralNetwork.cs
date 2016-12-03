using NeuralNetworkLibrary.NetworkNodes;

namespace NeuralNetworkLibrary.Networks
{
	public interface INeuralNetwork
	{
		double[] ComputeOutputs(double[] input);
		INeuralNetworkNodes GetNeuralNetworkNodes();
	}
}
