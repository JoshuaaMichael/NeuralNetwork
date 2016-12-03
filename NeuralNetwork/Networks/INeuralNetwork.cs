using NeuralNetwork.NetworkNodes;

namespace NeuralNetwork.Networks
{
	public interface INeuralNetwork
	{
		double[] ComputeOutputs(double[] input);
		INeuralNetworkNodes GetNeuralNetworkNodes();
	}
}
