using System;

namespace NeuralNetwork.NetworkNodes
{
	public class NeuralNetworkNodesRandom : NeuralNetworkNodes
	{
		private static Random rand = new Random();

		public NeuralNetworkNodesRandom(int[] numberOfNodesByLayer, double biasMin, double biasMax, double weightMin, double weightMax)
			: base(numberOfNodesByLayer)
		{
			GenerateBiasesAndWeights(biasMin, biasMax, weightMin, weightMax);
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
