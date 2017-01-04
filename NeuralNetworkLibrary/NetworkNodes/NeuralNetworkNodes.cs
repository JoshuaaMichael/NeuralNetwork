using Newtonsoft.Json;
using System;
using System.IO;

namespace NeuralNetworkLibrary.NetworkNodes
{
	public class NeuralNetworkNodes : NeuralNetworkNodesBase
	{
		private NeuralNetworkNodes() { }

		public NeuralNetworkNodes(string filename)
		{
			ReadFromFile(filename);
		}

		public NeuralNetworkNodes(int[] numberOfNodesByLayer, double[] biases, double[] sums, double[] weights)
			: base(numberOfNodesByLayer, biases, sums, weights) { }

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
			NeuralNetworkNodes loadedNN = JsonConvert.DeserializeObject<NeuralNetworkNodes>(json);

			numberOfNodesByLayer = loadedNN.numberOfNodesByLayer;
			numberOfNodes = loadedNN.numberOfNodes;
			nodeOffset = loadedNN.nodeOffset;
			weightOffset = loadedNN.weightOffset;
			biases = loadedNN.biases;
			sums = loadedNN.sums;
			weights = loadedNN.weights;
		}
	}
}
