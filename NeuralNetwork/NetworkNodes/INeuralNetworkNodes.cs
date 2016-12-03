
namespace NeuralNetwork.NetworkNodes
{
	public interface INeuralNetworkNodes
	{
		//Network
		int NumberOfNodes();
		int NumberOfLayers();
		int NumberOfNodesInLayer(int layer);
		double[] GetOutputs();

		//Biases
		double GetBias(int layer, int index);
		void SetBias(int layer, int index, double value);

		//Sums
		double GetSum(int layer, int index);
		void SetSum(int layer, int index, double value);

		//Weights
		double GetWeight(int layerFrom, int indexFrom, int layerTo, int indexTo);
		void SetWeight(int layerFrom, int indexFrom, int layerTo, int indexTo, double value);
	}
}
