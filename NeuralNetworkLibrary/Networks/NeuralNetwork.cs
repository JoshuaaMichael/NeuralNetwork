using System;
using NeuralNetworkLibrary.NetworkNodes;
using NeuralNetworkLibrary.ActivationFunctions;

namespace NeuralNetworkLibrary.Networks
{
	public class NeuralNetwork : INeuralNetwork
	{
		protected INeuralNetworkNodes nodes;
		protected IActivationFunction[] activationFunctions;

		public NeuralNetwork(INeuralNetworkNodes nodes, IActivationFunction[] activationFunctions)
		{
			this.nodes = nodes;
			if(activationFunctions.Length != nodes.NumberOfLayers() - 1)
			{
				throw new ArgumentException("Incorrect number of activation functions");
			}
			this.activationFunctions = activationFunctions;
		}

		public double[] ComputeOutputs(double[] input)
		{
			if (input.Length != nodes.NumberOfNodesInLayer(0))
			{
				throw new ArgumentException("Number of input values not equal to number of input nodes");
			}
			for (int i = 1; i < nodes.NumberOfLayers(); i++)
			{
				for (int j = 0; j < nodes.NumberOfNodesInLayer(i); j++)
				{
					double sum = 0;
					for (int k = 0; k < nodes.NumberOfNodesInLayer(i - 1); k++)
					{
						sum = ((i == 1) ? input[k] : nodes.GetSum(i - 1, k)) * nodes.GetWeight(i - 1, k, i, j);
					}
					sum += nodes.GetBias(i, j);
					sum = activationFunctions[i].Compute(sum);
					nodes.SetSum(i, j, sum);
				}
			}
			return nodes.GetOutputs();
		}

		public INeuralNetworkNodes GetNeuralNetworkNodes()
		{
			return nodes;
		}
	}
}
