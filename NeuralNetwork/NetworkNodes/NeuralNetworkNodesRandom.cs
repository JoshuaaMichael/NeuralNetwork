using System;

namespace NeuralNetworkLibrary.NetworkNodes
{
	public class NeuralNetworkNodesRandom : NeuralNetworkNodes
	{
		private static Random rand = new Random();

		public NeuralNetworkNodesRandom(string filename)
		{
			ReadFromFile(filename);
		}

		public NeuralNetworkNodesRandom(int[] numberOfNodesByLayer, double biasMin, double biasMax, double weightMin, double weightMax)
			: base(numberOfNodesByLayer)
		{
			GenerateBiasesAndWeights(biasMin, biasMax, weightMin, weightMax);
		}

		public override void ReadFromFile(string filename)
		{
			base.ReadFromFile(filename); //Didn't add any extra members in class, can get away with this
		}

		public override void SaveToFile(string filename)
		{
			base.SaveToFile(filename); //Don't need to save any other members in class, can get away with this
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
