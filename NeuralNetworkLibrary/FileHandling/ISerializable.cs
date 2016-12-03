
namespace NeuralNetworkLibrary.FileHandling
{
	interface ISerializable
	{
		void SaveToFile(string filename);
		void ReadFromFile(string filename);
	}
}
