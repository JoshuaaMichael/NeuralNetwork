using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetworkLibrary.Networks;
using NeuralNetworkLibrary.NetworkNodes;
using NeuralNetworkLibrary.ActivationFunctions;
using NeuralNetworkLibraryExamples.Data;
using NeuralNetworkLibrary.TrainableNetworks;
using NeuralNetworkLibrary.ErrorCalculations;
using System.IO;

namespace NeuralNetworkLibraryExamples
{
	class Example1 : Example
	{
		NeuralNetworkNodesRandom randNodes;
		int[] layers;
		IActivationFunction[] activationFunctions;
		DataSetCsvIris data;

		public void Setup()
		{
			layers = new int[] { 4, 5, 3 };
			activationFunctions = new IActivationFunction[2];
			activationFunctions[0] = new ActivationFunctionSigmoid();
			activationFunctions[1] = new ActivationFunctionHyperTan();

			string nodeFilename = "random-nodes.dat";
			if (File.Exists(nodeFilename))
			{
				randNodes = new NeuralNetworkNodesRandom(nodeFilename);
			}
			else
			{
				randNodes = new NeuralNetworkNodesRandom(layers, -1.0, 1.0, -1.0, 1.0);
				randNodes.SaveToFile(nodeFilename);
			}
			data = new DataSetCsvIris("iris-dataset.dat");
			data.ImportData();
			data.Shuffle();
		}

		public void RunExample()
		{
			double trainingPercentage = 0.8;
			double learnRate = 0.04;
			double momentum = 0.02;
			double targetError = 0.20;
			double targetCorrect = 0.95;
			int numberOfEpochs = 300;
			int numberOfIterations = 300;

			BackPropagationData backPropData = new BackPropagationData(randNodes, data, trainingPercentage, learnRate, momentum, targetError, targetCorrect, numberOfEpochs, numberOfIterations, new ErrorPercentage());
			BackPropagation backProp = new BackPropagation(backPropData, activationFunctions);


			backProp.Train();
		}
	}
}
