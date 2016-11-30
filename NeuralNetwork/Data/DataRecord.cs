using System;

namespace NeuralNetwork.Data
{
	public class DataRecord : IDataRecord
	{
		protected double[] input;
		protected double[] output;

		private DataRecord() { }

		public DataRecord(double[] input, double[] output)
		{
			this.input = input;
			this.output = output;
		}

		public object Clone()
		{
			DataRecord dr = new DataRecord();
			Array.Copy(input, dr.input, input.Length);
			Array.Copy(output, dr.output, output.Length);
			return dr;
		}

		public double[] GetInput()
		{
			return input;
		}

		public double[] GetOutput()
		{
			return output;
		}

		public int NumberOfInput()
		{
			return input.Length;
		}

		public int NumberOfOutput()
		{
			return output.Length;
		}
	}
}
