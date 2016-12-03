using System;
using NeuralNetworkLibrary.ActivationFunctions;
using NeuralNetworkLibrary.NetworkNodes;
using NeuralNetworkLibrary.ErrorCalculations;
using NeuralNetworkLibrary.Networks;
using NeuralNetworkLibrary.Data;

namespace NeuralNetworkLibrary.TrainableNetworks
{
	public class BackPropagation : NeuralNetwork, ITrainableNetwork
	{
		BackPropagationData backPropagationData;

		protected IErrorCalculation errorFunction;

		public BackPropagation(BackPropagationData backPropagationData, IActivationFunction[] activationFunctions)
			: base(backPropagationData.Nodes, activationFunctions)
		{
			this.backPropagationData = backPropagationData;
		}

		protected double[] UpdateWeights(double[] input, double[] target)
		{
			//Update the gradients - Each node has it's own gradient
			//Must be done from output to input
			for (int i = nodes.NumberOfLayers() - 1; i > 0; i--) //What layer we're on
			{
				if (i == nodes.NumberOfLayers() - 1) //The last layer
				{
					for (int j = 0; j < nodes.NumberOfNodesInLayer(i); j++) //go through the nodes in this layer
					{
						double derivative = activationFunctions[i].ComputeDerivative(nodes.GetSum(i, j));
						double value = derivative * (target[j] - nodes.GetSum(i, j));
						backPropagationData.SetGradient(i, j, value);
					}
				}
				else
				{
					for (int j = 0; j < nodes.NumberOfNodesInLayer(i); j++) //Go through this layers node
					{
						double derivative = activationFunctions[i].ComputeDerivative(nodes.GetSum(i, j));
						double sum = 0.0;
						for (int k = 0; k < nodes.NumberOfNodesInLayer(i - 1); k++) //Add in sums of weights between this node and each of last layers node
							sum += backPropagationData.GetGradient(i, j) * nodes.GetWeight(i - 1, k, i, j);
						double value = derivative * sum;
						backPropagationData.SetGradient(i, j, value);
					}
				}
			}

			//Update the weights
			//Can be done either direction, lets go forwards
			for (int i = 1; i < nodes.NumberOfLayers(); i++)
			{
				for (int j = 0; j < nodes.NumberOfNodesInLayer(i); j++) //current layers node node
				{
					for (int k = 0; k < nodes.NumberOfNodesInLayer(i - 1); k++) //node on last layer
					{
						double delta = backPropagationData.Eta * backPropagationData.GetGradient(i, j) * ((i != 1) ? nodes.GetSum(i - 1, k) : input[k]);
						double newWeight = nodes.GetWeight(i - 1, k, i, j) + delta + (backPropagationData.Alpha * backPropagationData.GetPreviousWeightDelta(i - 1, k, i, j));
						nodes.SetWeight(i - 1, k, i, j, newWeight);
						backPropagationData.SetPreviousWeightDelta(i - 1, k, i, j, delta);
					}
				}
			}

			//Update the biases
			for (int i = 1; i < nodes.NumberOfLayers(); i++)
			{
				for (int j = 0; j < nodes.NumberOfNodesInLayer(i); j++)
				{
					double delta = backPropagationData.Eta * backPropagationData.GetGradient(i, j);
					double newBias = nodes.GetBias(i, j) + delta + (backPropagationData.Alpha * backPropagationData.GetPreviousBiasDelta(i, j));
					nodes.SetBias(i, j, newBias);
					backPropagationData.SetPreviousBiasDelta(i, j, delta);
				}
			}

			return ComputeOutputs(input);
		}

		public void Train()
		{
			backPropagationData.DataSetTraining.Reset();
			IDataRecord dataRecord = backPropagationData.DataSetTraining.GetNextRecord();
			double[] outputs = ComputeOutputs(dataRecord.GetInputs()); // prime the back-prop loop
			double error = errorFunction.ComputeError(outputs, dataRecord.GetOutputs());

			//Console.WriteLine("Initial error: " + error);

			for (int i = 0; i < backPropagationData.NumberOfEpochs && error > backPropagationData.TargetError; i++)
			{
				//Console.WriteLine("Currently up to epoch: " + (i + 1) + "/" + backPropagationData.NumberOfEpochs + " - Percentage correct: " + GetPercentageCorrect() * 100.0);
				backPropagationData.DataSetTraining.Reset();
				while ((dataRecord = backPropagationData.DataSetTraining.GetNextRecord()) != null)
				{
					for (int j = 0; j < backPropagationData.NumberOfIterations; j++)
					{
						double[] result = UpdateWeights(dataRecord.GetInputs(), dataRecord.GetOutputs());
					}
					error = errorFunction.ComputeError(outputs, dataRecord.GetOutputs());
					//Console.WriteLine("Error: " + error);
				}
			}
		}

		public double GetPercentageCorrect()
		{
			backPropagationData.DataSetTesting.Reset();
			double correct = 0;
			IDataRecord dataRecord;
			while ((dataRecord = backPropagationData.DataSetTesting.GetNextRecord()) != null)
				if (errorFunction.ComputeError(ComputeOutputs(dataRecord.GetInputs()), dataRecord.GetOutputs()) <= backPropagationData.TargetError)
					correct += 1;
			return correct / backPropagationData.DataSetTesting.Count();
		}
	}
}
