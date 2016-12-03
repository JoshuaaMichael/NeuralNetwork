using NeuralNetworkLibrary.Data;
using NeuralNetworkLibrary.NetworkNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.TrainableNetworks
{
	public class BackPropagationData
	{
		protected double eta, alpha, targetError;
		protected int numberOfEpochs, numberOfIterations;

		protected double[] gradients;
		protected double[] previousWeightDelta;
		protected double[] previousBiasDelta;

		protected IDataSet dataSetTraining, dataSetTesting;
		protected INeuralNetworkNodes nodes;

		private BackPropagationData(INeuralNetworkNodes nodes, IDataSet dataSet, double trainingData, double eta, double alpha, double targetError, int numberOfEpochs, int numberOfIterations)
		{
			this.nodes = nodes; //Required for navigating arrays

			Tuple<IDataSet, IDataSet> dataSetSplit = dataSet.Split(trainingData);
			dataSetTraining = dataSetSplit.Item1;
			dataSetTesting = dataSetSplit.Item2;

			this.eta = eta;
			this.alpha = alpha;
			this.targetError = targetError;
			this.numberOfEpochs = numberOfEpochs;
			this.numberOfIterations = numberOfIterations;

			gradients = new double[nodes.NumberOfSums()];
			previousWeightDelta = new double[nodes.NumberOfWeights()];
			previousBiasDelta = new double[nodes.NumberOfBiases()];
		}

		public double GetGradient(int layer, int node)
		{
			int value = nodes.GetNodeOffset(layer) + node;
			return gradients[value];
		}

		public void SetGradient(int layer, int node, double value)
		{
			gradients[nodes.GetNodeOffset(layer) + node] = value;
		}

		public double GetPreviousBiasDelta(int layer, int node)
		{
			return previousBiasDelta[nodes.GetNodeOffset(layer) + node];
		}

		public void SetPreviousBiasDelta(int layer, int node, double value)
		{
			previousBiasDelta[nodes.GetNodeOffset(layer) + node] = value;
		}

		public double GetPreviousWeightDelta(int layerFrom, int indexFrom, int layerTo, int indexTo)
		{
			return previousWeightDelta[nodes.GetWeightOffset(layerTo) + (indexTo * nodes.NumberOfNodesInLayer(layerFrom)) + indexFrom];
		}

		public void SetPreviousWeightDelta(int layerFrom, int indexFrom, int layerTo, int indexTo, double value)
		{
			previousWeightDelta[nodes.GetWeightOffset(layerTo) + (indexTo * nodes.NumberOfNodesInLayer(layerFrom)) + indexFrom] = value;
		}
	}
}
