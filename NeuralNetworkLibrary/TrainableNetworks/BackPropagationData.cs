using NeuralNetworkLibrary.Data;
using NeuralNetworkLibrary.NetworkNodes;
using System;

namespace NeuralNetworkLibrary.TrainableNetworks
{
	public class BackPropagationData
	{
		public double Eta { get; protected set; }
		public double Alpha { get; protected set; }
		public double TargetError { get; protected set; }
		public int NumberOfEpochs { get; protected set; }
		public int NumberOfIterations { get; protected set; }

		protected double[] gradients;
		protected double[] previousWeightDelta;
		protected double[] previousBiasDelta;

		public IDataSet DataSetTraining { get; protected set; }
		public IDataSet DataSetTesting { get; protected set; }
		public INeuralNetworkNodes Nodes { get; protected set; }

		private BackPropagationData(INeuralNetworkNodes nodes, IDataSet dataSet, double trainingData, double eta, double alpha, double targetError, int numberOfEpochs, int numberOfIterations)
		{
			this.Nodes = nodes; //Required for navigating arrays

			Tuple<IDataSet, IDataSet> dataSetSplit = dataSet.Split(trainingData);
			DataSetTraining = dataSetSplit.Item1;
			DataSetTesting = dataSetSplit.Item2;

			this.Eta = eta;
			this.Alpha = alpha;
			this.TargetError = targetError;
			this.NumberOfEpochs = numberOfEpochs;
			this.NumberOfIterations = numberOfIterations;

			gradients = new double[nodes.NumberOfSums()];
			previousWeightDelta = new double[nodes.NumberOfWeights()];
			previousBiasDelta = new double[nodes.NumberOfBiases()];
		}

		public double GetGradient(int layer, int node)
		{
			return gradients[Nodes.GetNodeOffset(layer) + node];
		}

		public void SetGradient(int layer, int node, double value)
		{
			gradients[Nodes.GetNodeOffset(layer) + node] = value;
		}

		public double GetPreviousBiasDelta(int layer, int node)
		{
			return previousBiasDelta[Nodes.GetNodeOffset(layer) + node];
		}

		public void SetPreviousBiasDelta(int layer, int node, double value)
		{
			previousBiasDelta[Nodes.GetNodeOffset(layer) + node] = value;
		}

		public double GetPreviousWeightDelta(int layerFrom, int indexFrom, int layerTo, int indexTo)
		{
			return previousWeightDelta[Nodes.GetWeightOffset(layerTo) + (indexTo * Nodes.NumberOfNodesInLayer(layerFrom)) + indexFrom];
		}

		public void SetPreviousWeightDelta(int layerFrom, int indexFrom, int layerTo, int indexTo, double value)
		{
			previousWeightDelta[Nodes.GetWeightOffset(layerTo) + (indexTo * Nodes.NumberOfNodesInLayer(layerFrom)) + indexFrom] = value;
		}
	}
}
