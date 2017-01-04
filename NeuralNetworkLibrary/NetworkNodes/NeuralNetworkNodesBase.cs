using System;
using NeuralNetworkLibrary.FileHandling;
using Newtonsoft.Json;
using System.IO;

namespace NeuralNetworkLibrary.NetworkNodes
{
	public abstract class NeuralNetworkNodesBase : INeuralNetworkNodes, ISerializable
	{
		[JsonProperty]
		protected int[] numberOfNodesByLayer; //Number of nodes indexed by layer starting at zero
		[JsonProperty]
		protected int numberOfNodes;
		[JsonProperty]
		protected int numberOfWeights;

		[JsonProperty]
		protected int[] nodeOffset; //Start index of biases/sums indexed by layer in bias/sum array
		[JsonProperty]
		protected int[] weightOffset; //Start index of weight indexed by layer in weight array

		[JsonProperty]
		protected double[] biases; //Array of all biases, layer major, not including input layer
		[JsonProperty]
		protected double[] sums;
		[JsonProperty]
		protected double[] weights; //Array of all weights, layer major, not including output layer

		protected NeuralNetworkNodesBase() { }

		public NeuralNetworkNodesBase(int[] numberOfNodesByLayer)
		{
			this.numberOfNodesByLayer = numberOfNodesByLayer;
			CalculateOffsets();
			GenerateArrays();
		}

		public NeuralNetworkNodesBase(int[] numberOfNodesByLayer, double[] biases, double[] sums, double[] weights)
			: this(numberOfNodesByLayer)
		{
			CalculateOffsets();
			if (biases.Length != numberOfNodes ||
				sums.Length != numberOfNodes ||
				weights.Length != numberOfWeights)
			{
				throw new ArgumentException("One of the input arrays are the wrong size");
			}
			this.biases = biases;
			this.sums = sums;
			this.weights = weights;
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

		public int NumberOfBiases()
		{
			return numberOfNodes;
		}

		public int NumberOfSums()
		{
			return numberOfNodes;
		}

		public int NumberOfWeights()
		{
			return numberOfWeights;
		}

		private void CalculateOffsets()
		{
			nodeOffset = new int[NumberOfLayers()];
			weightOffset = new int[NumberOfLayers()];

			//Layer 0 doesn't have sums/biases or weights
			nodeOffset[0] = int.MinValue;
			weightOffset[0] = int.MinValue;

			numberOfNodes = 0;
			numberOfWeights = 0;

			for (int i = 1; i < NumberOfLayers(); i++)
			{
				nodeOffset[i] = numberOfNodes;
				numberOfNodes += NumberOfNodesInLayer(i);

				weightOffset[i] = numberOfWeights;
				numberOfWeights += NumberOfNodesInLayer(i) * NumberOfNodesInLayer(i - 1);
			}
		}

		private void GenerateArrays()
		{
			biases = new double[numberOfNodes];
			sums = new double[numberOfNodes];
			weights = new double[numberOfWeights];
		}

		public double GetBias(int layer, int index)
		{
			return biases[nodeOffset[layer] + index];
		}

		public void SetBias(int layer, int index, double value)
		{
			biases[nodeOffset[layer] + index] = value;
		}

		public double GetSum(int layer, int index)
		{
			return sums[nodeOffset[layer] + index];
		}

		public void SetSum(int layer, int index, double value)
		{
			sums[nodeOffset[layer] + index] = value;
		}

		public double GetWeight(int layerFrom, int indexFrom, int layerTo, int indexTo)
		{
			return weights[weightOffset[layerTo] + (indexTo * NumberOfNodesInLayer(layerFrom)) + indexFrom];
		}

		public void SetWeight(int layerFrom, int indexFrom, int layerTo, int indexTo, double value)
		{
			weights[weightOffset[layerTo] + (indexTo * NumberOfNodesInLayer(layerFrom)) + indexFrom] = value;
		}

		public double[] GetOutputs()
		{
			int lastLayer = NumberOfLayers() - 1;
			double[] layerSums = new double[numberOfNodesByLayer[lastLayer]];
			Array.Copy(sums, nodeOffset[lastLayer], layerSums, 0, layerSums.Length);
			return layerSums;
		}

		public int GetNodeOffset(int layer)
		{
			return nodeOffset[layer];
		}

		public int GetWeightOffset(int layer)
		{
			return weightOffset[layer];
		}

		public abstract void SaveToFile(string filename);

		public abstract void ReadFromFile(string filename);
	}
}
