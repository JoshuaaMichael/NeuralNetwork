using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NeuralNetwork.Data;

namespace NeuralNetworkTest.Data
{
	[TestFixture]
	class DataRecordTest
	{
		double[] inputs;
		double[] outputs;

		[SetUp]
		protected void SetUp()
		{
			inputs = new double[] { 1.0, 2.0, 3.0 };
			outputs = new double[] { 2.0, 3.0, 4.0, 5.0 };
		}

		[Test]
		public void TestGetInputs()
		{
			//Arrange
			DataRecord sut  = new DataRecord(inputs, outputs);

			//Execute
			double[] actual = sut.GetInputs();

			//Asset
			Assert.AreEqual(inputs, actual);
		}

		[Test]
		public void TestGetOutputs()
		{
			//Arrange
			DataRecord sut = new DataRecord(inputs, outputs);

			//Execute
			double[] actual = sut.GetOutputs();

			//Asset
			Assert.AreEqual(outputs, actual);
		}

		[Test]
		public void TestNumberOfInputs()
		{
			//Arrange
			DataRecord sut = new DataRecord(inputs, outputs);

			//Execute
			int actual = sut.NumberOfInputs();

			//Asset
			Assert.AreEqual(inputs.Length, actual);
		}

		[Test]
		public void TestNumberOfOutputs()
		{
			//Arrange
			DataRecord sut = new DataRecord(inputs, outputs);

			//Execute
			int actual = sut.NumberOfOutputs();

			//Asset
			Assert.AreEqual(outputs.Length, actual);
		}

		[Test]
		public void TestCloneObjectReference()
		{
			//Arrange
			DataRecord sut = new DataRecord(inputs, outputs);

			//Execute
			DataRecord actual = (DataRecord)sut.Clone();

			//Asset
			Assert.AreNotEqual(sut, actual);
		}

		[Test]
		public void TestCloneMembersEqual()
		{
			//Arrange
			DataRecord sut = new DataRecord(inputs, outputs);

			//Execute
			DataRecord actual = (DataRecord)sut.Clone();

			//Asset
			Assert.AreEqual(inputs, actual.GetInputs());
			Assert.AreEqual(outputs, actual.GetOutputs());
		}

	}
}
