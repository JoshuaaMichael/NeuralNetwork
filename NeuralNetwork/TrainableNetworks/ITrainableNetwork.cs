namespace NeuralNetworkLibrary.TrainableNetworks
{
	public interface ITrainableNetwork
	{
		void Train();
		double GetPercentageCorrect();
	}
}
