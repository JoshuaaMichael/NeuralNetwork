using System;

namespace NeuralNetwork.Data
{
	public class DataRecord : IDataRecord
	{
		protected double[] inputs;
		protected double[] outputs;

		private DataRecord() { }

		public DataRecord(double[] inputs, double[] outputs)
		{
			this.inputs = inputs;
			this.outputs = outputs;
		}

		public object Clone()
		{
			DataRecord dr = new DataRecord();
			dr.inputs = new double[inputs.Length];
			dr.outputs = new double[outputs.Length];
			Array.Copy(inputs, dr.inputs, inputs.Length);
			Array.Copy(outputs, dr.outputs, outputs.Length);
			return dr;
		}

		public double[] GetInputs()
		{
			return inputs;
		}

		public double[] GetOutputs()
		{
			return outputs;
		}

		public int NumberOfInputs()
		{
			return inputs.Length;
		}

		public int NumberOfOutputs()
		{
			return outputs.Length;
		}
	}
}
