
namespace NeuralNetworkLibrary.NetworkNodes
{
	public interface INeuralNetworkNodes
	{
		//Network
		int NumberOfNodes();
		int NumberOfLayers();
		int NumberOfNodesInLayer(int layer);
		double[] GetOutputs();
		int GetNodeOffset(int layer);

		//Biases
		double GetBias(int layer, int index);
		void SetBias(int layer, int index, double value);
		int NumberOfBiases();

		//Sums
		double GetSum(int layer, int index);
		void SetSum(int layer, int index, double value);
		int NumberOfSums();

		//Weights
		double GetWeight(int layerFrom, int indexFrom, int layerTo, int indexTo);
		void SetWeight(int layerFrom, int indexFrom, int layerTo, int indexTo, double value);
		int NumberOfWeights();
		int GetWeightOffset(int layer);
	}
}
