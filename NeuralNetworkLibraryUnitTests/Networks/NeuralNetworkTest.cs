using NeuralNetworkLibrary.ActivationFunctions;
using NUnit.Framework;
using System.Linq;
using NeuralNetworkLibrary.NetworkNodes;
using NeuralNetworkLibrary.Data;
using NeuralNetworkLibrary.Networks;
using NeuralNetworkLibrary.TrainableNetworks;
using NeuralNetworkLibraryExamples.Data;
using NeuralNetworkLibrary.ErrorCalculations;

namespace NeuralNetworkLibraryUnitTests.Networks
{
	[TestFixture]
	class NeuralNetworkTest
	{
		int[] layers;
		IActivationFunction[] activationFunctions;
		double[] biases;
		double[] sums;
		double[] weights;

		[OneTimeSetUp]
		protected void Init()
		{
			layers = new int[] { 4, 5, 3 };
			activationFunctions = new IActivationFunction[2];
			activationFunctions[0] = new ActivationFunctionSigmoid();
			activationFunctions[1] = new ActivationFunctionHyperTan();
			biases = new double[NumberOfNodes() - layers[0]];
			sums = new double[NumberOfNodes() - layers[0]];
			weights = new double[NumberOfWeights()];
		}

		[SetUp]
		protected void SetUp()
		{
		}

		[Test]
		public void TestComputeOutputs()
		{
			//Arrange
			layers = new int[] { 2, 3, 1 };

			double[] input = new double[layers[0]];
			biases = new double[] { 0.73, 0.79, 0.69, 0.77 };
			weights = new double[] { 0.8, 0.2, 0.4, 0.9, 0.3, 0.5, 0.3, 0.5, 0.9 };
			input = new double[] { 1.0, 1.0 };

			IDataRecord dr = new DataRecord(input, null);

			INeuralNetworkNodes nodes = new NeuralNetworkNodes(layers, biases, sums, weights);
			INeuralNetwork sut = new NeuralNetwork(nodes, activationFunctions);

			double[] expected = new double[] { 1.0, 1.0, 1.0 };

			//Act
			double[] actual = sut.ComputeOutputs(input);

			//Assert
			Assert.AreEqual(expected, actual);
		}

		//[Test]
		//public void TestBackProp()
		//{
			

		//	//Arrange

		//	double trainingPercentage = 0.8;
		//	double eta = 0.01;
		//	double alpha = 0.05;
		//	double targetError = 0.20;
		//	double targetCorrect = 0.95;
		//	int numberOfEpochs = 1000;
		//	int numberOfIterations = 200;

		//	INeuralNetworkNodes randNodes = new NeuralNetworkNodesRandom(layers, -1.0, 1.0, -1.0, 1.0);
		//	IDataSetImportableData data = new DataSetCsvIris(@"C:\Users\joshj\Dropbox\Portfolio\NeuralNetworkLibrary\NeuralNetworkLibraryUnitTests\bin\Debug\iris-dataset.dat");
		//	data.ImportData();
		//	BackPropagationData backPropData = new BackPropagationData(randNodes, data, trainingPercentage, eta, alpha, targetError, targetCorrect, numberOfEpochs, numberOfIterations, new ErrorPercentage());

		//	BackPropagation sut = new BackPropagation(backPropData, activationFunctions);

		//	//Act
		//	sut.Train();

		//	//Assert
		//	//Assert.AreEqual(expected, actual);
		//}

		private int NumberOfNodes()
		{
			return layers.Sum();
		}

		private int NumberOfWeights()
		{
			int total = 0;
			for (int i = 1; i < layers.Length; i++)
			{
				total += layers[i] * layers[i - 1];
			}
			return total;
		}

		private double[] ZeroArray(double[] array)
		{
			for(int i = 1; i < array.Length + 1; i++)
			{
				array[i - 1] = 0.0;
			}
			return array;
		}
	}
}
