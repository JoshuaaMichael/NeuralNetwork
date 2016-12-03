using System;
using NeuralNetworkLibrary.ActivationFunctions;
using NeuralNetworkLibrary.NetworkNodes;
using NeuralNetworkLibrary.ErrorCalculations;
using NeuralNetworkLibrary.Networks;

namespace NeuralNetworkLibrary.TrainableNetworks
{
	public class BackPropagation : NeuralNetwork, ITrainableNetwork
	{


		protected IErrorCalculation errorFunction;

		public BackPropagation(INeuralNetworkNodes nodes, IActivationFunction[] activationFunctions)
			: base(nodes, activationFunctions) { }

		

		public double GetPercentageCorrect()
		{
			throw new NotImplementedException();
		}

		public void Train()
		{
			throw new NotImplementedException();
		}
	}
}
