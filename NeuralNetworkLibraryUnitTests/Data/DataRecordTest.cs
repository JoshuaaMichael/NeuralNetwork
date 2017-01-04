using NUnit.Framework;
using NeuralNetworkLibrary.Data;

namespace NeuralNetworkLibraryUnitTests.Data
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
		public void TestDataRecordGetInputs()
		{
			//Arrange
			DataRecord sut  = new DataRecord(inputs, outputs);

			//Execute
			double[] actual = sut.GetInputs();

			//Asset
			Assert.AreEqual(inputs, actual);
		}

		[Test]
		public void TestDataRecordGetOutputs()
		{
			//Arrange
			DataRecord sut = new DataRecord(inputs, outputs);

			//Execute
			double[] actual = sut.GetOutputs();

			//Asset
			Assert.AreEqual(outputs, actual);
		}

		[Test]
		public void TestDataRecordNumberOfInputs()
		{
			//Arrange
			DataRecord sut = new DataRecord(inputs, outputs);

			//Execute
			int actual = sut.NumberOfInputs();

			//Asset
			Assert.AreEqual(inputs.Length, actual);
		}

		[Test]
		public void TestDataRecordNumberOfOutputs()
		{
			//Arrange
			DataRecord sut = new DataRecord(inputs, outputs);

			//Execute
			int actual = sut.NumberOfOutputs();

			//Asset
			Assert.AreEqual(outputs.Length, actual);
		}

		[Test]
		public void TestDataRecordCloneObjectReference()
		{
			//Arrange
			DataRecord sut = new DataRecord(inputs, outputs);

			//Execute
			DataRecord actual = (DataRecord)sut.Clone();

			//Asset
			Assert.AreNotEqual(sut, actual);
		}

		[Test]
		public void TestDataRecordCloneMembersEqual()
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
