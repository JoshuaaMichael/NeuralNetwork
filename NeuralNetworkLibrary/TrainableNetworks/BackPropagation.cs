using System;
using NeuralNetworkLibrary.ActivationFunctions;
using NeuralNetworkLibrary.Networks;
using NeuralNetworkLibrary.Data;
using System.Collections.Generic;
using System.Text;
using System.IO;

//TODO: Update weights -> http://quaetrix.com/Build2014.html

namespace NeuralNetworkLibrary.TrainableNetworks
{
	public class BackPropagation : NeuralNetwork, ITrainableNetwork
	{
		BackPropagationData backPropagationData;
		List<double[]> monitor;

		public BackPropagation(BackPropagationData backPropagationData, IActivationFunction[] activationFunctions)
			: base(backPropagationData.Nodes, activationFunctions)
		{
			this.backPropagationData = backPropagationData;
			monitor = new List<double[]>();
		}

		public double[] UpdateWeights(double[] input, double[] target)
		{
			#region Calculate gradients - Must be done backwards
			for (int i = nodes.NumberOfLayers() - 1; i > 0; i--) //What layer we're on
			{
				if (i == nodes.NumberOfLayers() - 1) //The last layer
				{
					for (int j = 0; j < nodes.NumberOfNodesInLayer(i); j++) //Go through the nodes in this layer
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

						for(int k = 0; k < nodes.NumberOfNodesInLayer(i + 1); k++)
						{
							sum += backPropagationData.GetGradient(i + 1, k) * nodes.GetWeight(i + 1, k, i, j);
						}
						double value = derivative * sum;
						backPropagationData.SetGradient(i, j, value);
					}
				}
			}
			#endregion

			#region Update weights - Can be done either direction, lets go forwards
			for (int i = 1; i < nodes.NumberOfLayers(); i++)
			{
				for (int j = 0; j < nodes.NumberOfNodesInLayer(i); j++) //current layer's node
				{
					for (int k = 0; k < nodes.NumberOfNodesInLayer(i - 1); k++) //last layer's node
					{
						//Not sure on GetGradient values
						double delta = backPropagationData.LearnRate * backPropagationData.GetGradient(i, j) * ((i != 1) ? nodes.GetSum(i - 1, k) : input[k]);
						double newWeight = nodes.GetWeight(i - 1, k, i, j) + delta + (backPropagationData.Momentum * backPropagationData.GetPreviousWeightDelta(i - 1, k, i, j));
						nodes.SetWeight(i - 1, k, i, j, newWeight);
						backPropagationData.SetPreviousWeightDelta(i - 1, k, i, j, delta);
					}
				}
			}
			#endregion

			#region Update biases
			for (int i = 1; i < nodes.NumberOfLayers(); i++)
			{
				for (int j = 0; j < nodes.NumberOfNodesInLayer(i); j++)
				{
					double delta = backPropagationData.GetGradient(i, j) * backPropagationData.LearnRate;
					double newBias = nodes.GetBias(i, j) + delta + (backPropagationData.GetPreviousBiasDelta(i, j) * backPropagationData.Momentum);
					nodes.SetBias(i, j, newBias);
					backPropagationData.SetPreviousBiasDelta(i, j, delta);
				}
			}
			#endregion

			return ComputeOutputs(input);
		}

		public void Train()
		{
			backPropagationData.DataSetTraining.Reset();
			IDataRecord dataRecord = backPropagationData.DataSetTraining.GetNextRecord();
			double[] outputs = ComputeOutputs(dataRecord.GetInputs()); // prime the back-prop loop
			double percentCorrect = GetPercentageCorrect();

			Console.WriteLine("Initial percentage correct: " + percentCorrect * 100.0);

			for (int i = 0; i < backPropagationData.MaxEpochs && percentCorrect < backPropagationData.TargetCorrect; i++)
			{
				Console.WriteLine("Currently up to epoch: " + (i + 1) + "/" + backPropagationData.MaxEpochs + " - Percentage correct: " + percentCorrect * 100.0);
				backPropagationData.DataSetTraining.Reset();
				backPropagationData.DataSetTraining.Shuffle(); //Visit all training data in a random order
				while ((dataRecord = backPropagationData.DataSetTraining.GetNextRecord()) != null)
				{
					for (int j = 0; j < backPropagationData.NumberOfIterations; j++)
					{
						UpdateWeights(dataRecord.GetInputs(), dataRecord.GetOutputs());
					}
				}
				percentCorrect = GetPercentageCorrect();
				AddOutputToMonitor();
			}
			WriteArray();
		}

		private void AddOutputToMonitor()
		{
			backPropagationData.DataSetTesting.Reset();
			IDataRecord rec = backPropagationData.DataSetTesting.GetNextRecord();
			double[] output = ComputeOutputs(rec.GetInputs());
			monitor.Add(output);			
		}

		private void WriteArray()
		{
			backPropagationData.DataSetTesting.Reset();
			IDataRecord rec = backPropagationData.DataSetTesting.GetNextRecord();
			double[] target = rec.GetOutputs();

			long epochTicks = new DateTime(1970, 1, 1).Ticks;
			long unixTime = ((DateTime.UtcNow.Ticks - epochTicks) / TimeSpan.TicksPerSecond);

			Console.WriteLine("Well well well...");
			Console.WriteLine(string.Format("{0}, {1}, {2}", target[0], target[1], target[2]));
			string fileName = "Data/" + unixTime + ".csv";

			StringBuilder strbld = new StringBuilder();
			foreach(double[] lineItem in monitor)
			{
				strbld.Append(string.Format("{0}, {1}, {2}\n", lineItem[0], lineItem[1], lineItem[2]));
			}
			File.WriteAllText(fileName, strbld.ToString());
		}

		public double GetPercentageCorrect()
		{
			backPropagationData.DataSetTesting.Reset();
			double correct = 0;
			IDataRecord dataRecord;
			while ((dataRecord = backPropagationData.DataSetTesting.GetNextRecord()) != null)
				if (backPropagationData.ErrorCalculation.ComputeError(ComputeOutputs(dataRecord.GetInputs()), dataRecord.GetOutputs()) <= backPropagationData.TargetError)
					correct += 1;
			return correct / backPropagationData.DataSetTesting.Count();
		}
	}
}
