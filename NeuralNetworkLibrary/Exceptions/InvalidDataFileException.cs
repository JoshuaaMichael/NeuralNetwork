using System;

namespace NeuralNetworkLibrary.Exceptions
{
	public class InvalidDataFileException : Exception
	{
		public InvalidDataFileException(string message)
			: base(message) { }

		public InvalidDataFileException(string message, Exception innerException)
			: base(message, innerException) { }
	}
}
