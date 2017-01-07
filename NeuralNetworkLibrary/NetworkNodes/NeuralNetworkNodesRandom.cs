using Newtonsoft.Json;
using System;
using System.IO;

namespace NeuralNetworkLibrary.NetworkNodes
{
	public class NeuralNetworkNodesRandom : NeuralNetworkNodesBase
	{
		private static Random rand = new Random();

		private NeuralNetworkNodesRandom() { }

		public NeuralNetworkNodesRandom(string filename)
		{
			ReadFromFile(filename);
		}

		public NeuralNetworkNodesRandom(int[] numberOfNodesByLayer, double biasMin, double biasMax, double weightMin, double weightMax)
			: base(numberOfNodesByLayer)
		{
			GenerateBiasesAndWeights(biasMin, biasMax, weightMin, weightMax);
		}

		public override void SaveToFile(string filename)
		{
			if (filename.Length == 0)
			{
				throw new ArgumentException("The filename for saving the file was empty");
			}

			string json = JsonConvert.SerializeObject(this, Formatting.Indented);

			File.WriteAllText(filename, json);
		}

		public override void ReadFromFile(string filename)
		{
			if (!File.Exists(filename))
			{
				throw new ArgumentException("File not found");
			}

			string json = File.ReadAllText(filename);
			NeuralNetworkNodesRandom loadedNN = JsonConvert.DeserializeObject<NeuralNetworkNodesRandom>(json);

			numberOfNodesByLayer = loadedNN.numberOfNodesByLayer;
			numberOfNodes = loadedNN.numberOfNodes;
			numberOfWeights = loadedNN.numberOfWeights;
			nodeOffset = loadedNN.nodeOffset;
			weightOffset = loadedNN.weightOffset;
			biases = loadedNN.biases;
			sums = loadedNN.sums;
			weights = loadedNN.weights;
	}

	private void GenerateBiasesAndWeights(double biasMin, double biasMax, double weightMin, double weightMax)
		{
			for (int i = 0; i < weights.Length; i++)
			{
				weights[i] = weightMin + (rand.NextDouble() * (weightMax - weightMin));
			}
			for (int i = 0; i < biases.Length; i++)
			{
				biases[i] = biasMin + (rand.NextDouble() * (biasMax - biasMin));
			}
		}
	}
}
