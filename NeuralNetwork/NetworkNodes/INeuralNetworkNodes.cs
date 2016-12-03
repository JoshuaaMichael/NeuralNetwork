
namespace NeuralNetwork.NetworkNodes
{
	public interface INeuralNetworkNodes
	{
		int NumberOfNodes();
		int NumberOfLayers();
		int NumberOfNodesInLayer(int layer);
	}
}
