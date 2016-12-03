using System;
using NeuralNetworkLibrary.FileHandling;
using Newtonsoft.Json;
using System.IO;

namespace NeuralNetworkLibrary.NetworkNodes
{
	public abstract class NeuralNetworkNodes : INeuralNetworkNodes, ISerializable
	{
		[JsonProperty]
		protected int[] numberOfNodesByLayer; //Number of nodes indexed by layer starting at zero
		[JsonProperty]
		protected int numberOfNodes;

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

		protected NeuralNetworkNodes() { }

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

		public int NumberOfBiases()
		{
			return biases.Length;
		}

		public int NumberOfSums()
		{
			return sums.Length;
		}

		public int NumberOfWeights()
		{
			return weights.Length;
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

		public virtual void SaveToFile(string filename)
		{
			if (filename.Length == 0)
			{
				throw new ArgumentException("The filename for saving the file was empty");
			}

			string json = JsonConvert.SerializeObject(this, Formatting.Indented);

			File.WriteAllText(filename, json);
		}

		public virtual void ReadFromFile(string filename)
		{
			if (!File.Exists(filename))
			{
				throw new ArgumentException("File not found");
			}

			string json = File.ReadAllText(filename);
			NeuralNetworkNodes loadedNN = JsonConvert.DeserializeObject<NeuralNetworkNodes>(json);

			numberOfNodesByLayer =	loadedNN.numberOfNodesByLayer;
			numberOfNodes =			loadedNN.numberOfNodes;
			nodeOffset =			loadedNN.nodeOffset;
			weightOffset =			loadedNN.weightOffset;
			biases =				loadedNN.biases;
			sums =					loadedNN.sums;
			weights =				loadedNN.weights;
		}
	}
}
