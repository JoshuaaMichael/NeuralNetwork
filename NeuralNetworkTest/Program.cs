using NeuralNetwork.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkTest
{
	class Program
	{
		const int COUNT_TEST_RECORDS = 10;
		const int INPUT_RECORD_LENGTH = 5;
		const int OUTPUT_RECORD_LENGTH = 3;
		static List<IDataRecord> testDataRecords;
		static int testDataRecordsCountVal = 0;

		protected static void Init()
		{
			testDataRecords = new List<IDataRecord>();
			for (int i = 0; i < COUNT_TEST_RECORDS; i++)
			{
				double[] inputs = GenerateTestDoubleArray(INPUT_RECORD_LENGTH);
				double[] outputs = GenerateTestDoubleArray(OUTPUT_RECORD_LENGTH);
				testDataRecords.Add(new DataRecord(inputs, outputs));
			}
		}

		protected static double[] GenerateTestDoubleArray(int numberOfItems)
		{
			double[] arr = new double[numberOfItems];
			for (int i = 0; i < numberOfItems; i++)
			{
				arr[i] = testDataRecordsCountVal++;
			}
			return arr;
		}


		static void Main(string[] args)
		{
			Init();

			//Arrange
			DataSet sut = new DataSet(testDataRecords);

			//Execute
			IDataSet clone = (IDataSet)sut.Clone();

			//Asset
			//Assert.AreEqual(COUNT_TEST_RECORDS, clone.Count());
			for (int i = 0; i < COUNT_TEST_RECORDS; i++)
			{
				IDataRecord idrSut = sut.GetNextRecord();
				IDataRecord idrClone = clone.GetNextRecord();
				//Assert.AreEqual(idrSut, idrClone);
			}
		}
	}
}
