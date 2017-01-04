using NeuralNetworkLibrary.Data;
using NeuralNetworkLibrary.ErrorCalculations;
using NeuralNetworkLibrary.NetworkNodes;
using System;

namespace NeuralNetworkLibrary.TrainableNetworks
{
	public class BackPropagationData
	{
		public double LearnRate { get; protected set; }
		public double Momentum { get; protected set; }
		public double TargetError { get; protected set; }
		public double TargetCorrect { get; protected set; }
		public int NumberOfIterations { get; protected set; }
		public int MaxEpochs { get; protected set; }

		protected double[] gradients;
		protected double[] previousWeightDelta;
		protected double[] previousBiasDelta;

		public IDataSet DataSetTraining { get; protected set; }
		public IDataSet DataSetTesting { get; protected set; }
		public INeuralNetworkNodes Nodes { get; protected set; }

		public IErrorCalculation ErrorCalculation { get; protected set; }

		public BackPropagationData(INeuralNetworkNodes nodes, IDataSet dataSet, double trainingData, double learnRate, double momentum, double targetError, double targetCorrect, int numberOfIterations, int maxEpochs, IErrorCalculation errorCalculation)
		{
			Nodes = nodes; //Required for navigating arrays

			Tuple<IDataSet, IDataSet> dataSetSplit = dataSet.Split(trainingData);
			DataSetTraining = dataSetSplit.Item1;
			DataSetTesting = dataSetSplit.Item2;

			LearnRate = learnRate;
			Momentum = momentum;
			TargetError = targetError;
			TargetCorrect = targetCorrect;
			NumberOfIterations = numberOfIterations;
			MaxEpochs = maxEpochs;

			gradients = new double[nodes.NumberOfNodes()];
			previousWeightDelta = new double[nodes.NumberOfWeights()];
			previousBiasDelta = new double[nodes.NumberOfBiases()];

			ErrorCalculation = errorCalculation;
		}

		public double GetPreviousWeightDelta(int layerFrom, int indexFrom, int layerTo, int indexTo)
		{
			return GetWeightFromArray(previousWeightDelta, layerFrom, indexFrom, layerTo, indexTo);
		}

		public void SetPreviousWeightDelta(int layerFrom, int indexFrom, int layerTo, int indexTo, double value)
		{
			SetWeightInArray(previousWeightDelta, layerFrom, indexFrom, layerTo, indexTo, value);
		}

		public double GetGradient(int layer, int node)
		{
			return GetBiasFromArray(gradients, layer, node);
		}

		public void SetGradient(int layer, int node, double value)
		{
			SetBiasInArray(gradients, layer, node, value);
		}

		public double GetPreviousBiasDelta(int layer, int node)
		{
			return GetBiasFromArray(previousBiasDelta, layer, node);
		}

		public void SetPreviousBiasDelta(int layer, int node, double value)
		{
			SetBiasInArray(previousBiasDelta, layer, node, value);
		}

		private double GetWeightFromArray(double[] weights, int layerFrom, int indexFrom, int layerTo, int indexTo)
		{
			int index = Nodes.GetWeightOffset(layerTo) + (indexTo * Nodes.NumberOfNodesInLayer(layerFrom)) + indexFrom;
			return weights[index];
		}
		private void SetWeightInArray(double[] weights, int layerFrom, int indexFrom, int layerTo, int indexTo, double value)
		{
			int index = Nodes.GetWeightOffset(layerTo) + (indexTo * Nodes.NumberOfNodesInLayer(layerFrom)) + indexFrom;
			weights[index] = value;
		}
		private double GetBiasFromArray(double[] biases, int layer, int node)
		{
			int index = Nodes.GetNodeOffset(layer) + node;
			return biases[index];
		}
		private void SetBiasInArray(double[] biases, int layer, int node, double value)
		{
			int index = Nodes.GetNodeOffset(layer) + node;
			biases[index] = value;
		}
	}
}
