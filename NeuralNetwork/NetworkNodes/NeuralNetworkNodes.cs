
namespace NeuralNetwork.NetworkNodes
{
	public abstract class NeuralNetworkNodes : INeuralNetworkNodes
	{
		protected int[] numberOfNodesByLayer; //Number of nodes indexed by layer starting at zero
		protected int numberOfNodes;

		protected int[] nodeOffset; //Start index of biases/sums indexed by layer in bias/sum array
		protected int[] weightOffset; //Start index of weight indexed by layer in weight array

		protected double[] biases; //Array of all biases, layer major, not including input layer
		protected double[] sums;
		protected double[] weights; //Array of all weights, layer major, not including output layer

		public NeuralNetworkNodes(int[] numberOfNodesByLayer)
		{
			this.numberOfNodesByLayer = numberOfNodesByLayer;
			GenerateArrays();
		}

		public int NumberOfNodes()
		{
			return numberOfNodes;
		}

		public int NumberOfLayers()
		{
			return numberOfNodesByLayer.Length;
		}

		public int NumberOfNodesInLayer(int layer)
		{
			return numberOfNodesByLayer[layer];
		}

		private void GenerateArrays()
		{
			nodeOffset = new int[NumberOfLayers()];
			weightOffset = new int[NumberOfLayers()];

			//Layer 0 doesn't have sums/biases or weights
			nodeOffset[0] = int.MinValue;
			weightOffset[0] = int.MinValue;

			int numberOfNodes = 0;
			int numberOfWeights = 0;

			for (int i = 1; i < NumberOfLayers(); i++)
			{
				nodeOffset[i] = numberOfNodes;
				numberOfNodes += NumberOfNodesInLayer(i);

				weightOffset[i] = numberOfWeights;
				numberOfWeights += NumberOfNodesInLayer(i) * NumberOfNodesInLayer(i - 1);
			}

			biases = new double[numberOfNodes];
			sums = new double[numberOfNodes];
			weights = new double[numberOfWeights];
		}

		protected double GetBias(int layer, int index)
		{
			return biases[nodeOffset[layer] + index];
		}

		protected void SetBias(int layer, int index, double value)
		{
			biases[nodeOffset[layer] + index] = value;
		}

		protected double GetSum(int layer, int index)
		{
			return sums[nodeOffset[layer] + index];
		}

		protected void SetSum(int layer, int index, double value)
		{
			sums[nodeOffset[layer] + index] = value;
		}

		protected double GetWeight(int layerFrom, int indexFrom, int layerTo, int indexTo)
		{
			return weights[weightOffset[layerTo] + (indexTo * NumberOfNodesInLayer(layerFrom)) + indexFrom];
		}

		protected void SetWeight(int layerFrom, int indexFrom, int layerTo, int indexTo, double value)
		{
			weights[weightOffset[layerTo] + (indexTo * NumberOfNodesInLayer(layerFrom)) + indexFrom] = value;
		}

		protected int GetNodeOffset(int layer)
		{
			return nodeOffset[layer];
		}

		protected int GetWeightOffset(int layer)
		{
			return weightOffset[layer];
		}
	}
}
