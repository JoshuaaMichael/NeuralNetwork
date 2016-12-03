using System;
using System.Collections.Generic;
using NUnit.Framework;
using NeuralNetworkLibrary.Data;
using NUnit.Framework.Constraints;

namespace NeuralNetworkLibraryUnitTests.Data
{
	[TestFixture]
	class DataSetTest
	{
		const int COUNT_TEST_RECORDS = 10;
		const int INPUT_RECORD_LENGTH = 5;
		const int OUTPUT_RECORD_LENGTH = 3;
		List<IDataRecord> testDataRecords;
		int testDataRecordsCountVal = 0;

		[OneTimeSetUp]
		protected void Init()
		{
			testDataRecords = new List<IDataRecord>();
			for (int i = 0; i < COUNT_TEST_RECORDS; i++)
			{
				double[] inputs = GenerateTestDoubleArray(INPUT_RECORD_LENGTH);
				double[] outputs = GenerateTestDoubleArray(OUTPUT_RECORD_LENGTH);
				testDataRecords.Add(new DataRecord(inputs, outputs));
			}
		}

		protected double[] GenerateTestDoubleArray(int numberOfItems)
		{
			double[] arr = new double[numberOfItems];
			for(int i = 0; i < numberOfItems; i++)
			{
				arr[i] = testDataRecordsCountVal++;
			}
			return arr;
		}

		[SetUp]
		protected void SetUp()
		{
		}

		[Test]
		public void TestDataSetCountEmpty()
		{
			//Arrange
			DataSet sut = new DataSet();

			//Act
			int actual = sut.Count();

			//Asset
			Assert.AreEqual(0, actual);
		}

		[Test]
		public void TestDataSetCountRemainingEmpty()
		{
			//Arrange
			DataSet sut = new DataSet();

			//Act
			int actual = sut.CountRemaining();

			//Asset
			Assert.AreEqual(0, actual);
		}

		[Test]
		public void TestDataSetGetNextRecordEmpty()
		{
			//Arrange
			DataSet sut = new DataSet();

			//Act
			IDataRecord actual = sut.GetNextRecord();

			//Asset
			Assert.AreEqual(null, actual);
		}

		[Test]
		public void TestDataSetGetCurrentRecordEmpty()
		{
			//Arrange
			DataSet sut = new DataSet();

			//Act
			IDataRecord actual = sut.GetCurrentRecord();

			//Asset
			Assert.AreEqual(null, actual);
		}

		[Test]
		public void TestDataSetShuffleEmpty()
		{
			//Arrange
			DataSet sut = new DataSet();

			//Act
			sut.Shuffle();

			//Asset
			//Doesn't throw exception
		}

		[Test]
		public void TestDataSetSplitEmpty()
		{
			//Arrange
			DataSet sut = new DataSet();

			//Act
			Tuple<IDataSet, IDataSet> actual = sut.Split(0.5);
			DataSet empty = new DataSet();

			//Asset
			Assert.AreEqual(empty.Count(), actual.Item1.Count());
			Assert.AreEqual(empty.Count(), actual.Item2.Count());
		}

		[Test]
		public void TestDataSetCloneEmpty()
		{
			//Arrange
			DataSet sut = new DataSet();

			//Act
			IDataSet clone = (IDataSet)sut.Clone();

			//Asset
			Assert.AreEqual(0, clone.Count());
		}

		[Test]
		public void TestDataSetCountAfterReset()
		{
			//Arrange
			DataSet sut = new DataSet();

			//Act
			sut.Reset();
			int actual = sut.Count();

			//Asset
			Assert.AreEqual(0, actual);
		}

		[Test]
		public void TestDataSetCountRemainingAfterReset()
		{
			//Arrange
			DataSet sut = new DataSet();

			//Act
			sut.Reset();
			int actual = sut.CountRemaining();

			//Asset
			Assert.AreEqual(0, actual);
		}

		[Test]
		public void TestDataSetGetNextRecordAfterReset()
		{
			//Arrange
			DataSet sut = new DataSet();

			//Act
			sut.Reset();
			IDataRecord actual = sut.GetNextRecord();

			//Asset
			Assert.AreEqual(null, actual);
		}

		[Test]
		public void TestDataSetGetCurrentRecordAfterReset()
		{
			//Arrange
			DataSet sut = new DataSet();

			//Act
			sut.Reset();
			IDataRecord actual = sut.GetCurrentRecord();

			//Asset
			Assert.AreEqual(null, actual);
		}

		[Test]
		public void TestDataSetCountWithData()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);

			//Act
			int actual = sut.Count();

			//Asset
			Assert.AreEqual(COUNT_TEST_RECORDS, actual);
		}

		[Test]
		public void TestDataSetCountRemainingWithData()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);

			//Act
			int actual = sut.CountRemaining();

			//Asset
			Assert.AreEqual(COUNT_TEST_RECORDS, actual);
		}

		[Test]
		public void TestDataSetCountRemainingWithDataAdvanceCursor()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);
			int countToRemove = 5;

			//Act
			for (int i = 0; i < countToRemove; i++)
			{
				sut.GetNextRecord();
			}
			int actual = sut.CountRemaining();

			//Asset
			Assert.AreEqual(COUNT_TEST_RECORDS - countToRemove, actual);
		}

		[Test]
		public void TestDataSetCountRemainingWithDataUsedAll()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);

			//Act
			for(int i = 0; i < COUNT_TEST_RECORDS; i++)
			{
				sut.GetNextRecord();
			}
			int actual = sut.CountRemaining();

			//Asset
			Assert.AreEqual(0, actual);
		}

		[Test]
		public void TestDataSetCountRemainingWithDataAfterUsedAll()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);

			//Act
			for (int i = 0; i < COUNT_TEST_RECORDS + 1; i++)
			{
				sut.GetNextRecord();
			}
			int actual = sut.CountRemaining();

			//Asset
			Assert.AreEqual(0, actual);
		}

		[Test]
		public void TestDataSetGetNextRecordWithData()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);

			//Act
			IDataRecord actual = sut.GetNextRecord();

			//Asset
			Assert.AreEqual(testDataRecords[0], actual);
		}

		[Test]
		public void TestDataSetGetNextRecordWithDataHalf()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);

			//Act
			for (int i = 0; i < (COUNT_TEST_RECORDS / 2) - 1; i++) //Half, without the one we're interested in
			{
				sut.GetNextRecord();
			}
			IDataRecord actual = sut.GetNextRecord();

			//Asset
			Assert.AreEqual(testDataRecords[(COUNT_TEST_RECORDS / 2) - 1], actual); //-1 is because index of array
		}

		[Test]
		public void TestDataSetGetNextRecordWithDataLastRecord()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);

			//Act
			for (int i = 0; i < COUNT_TEST_RECORDS - 1; i++) //Skip to the second last one
			{
				sut.GetNextRecord();
			}
			IDataRecord actual = sut.GetNextRecord();

			//Asset
			Assert.AreEqual(testDataRecords[testDataRecords.Count - 1], actual); //-1 is because index of array
		}

		[Test]
		public void TestDataSetGetNextRecordWithDataAfterLastRecord()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);

			//Act
			for (int i = 0; i < COUNT_TEST_RECORDS; i++) //Skip to the last one
			{
				sut.GetNextRecord();
			}
			IDataRecord actual = sut.GetNextRecord(); //Get that extra

			//Asset
			Assert.AreEqual(null, actual);
		}

		[Test]
		public void TestDataSetGetCurrentRecordWithData()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);

			//Act
			IDataRecord actual = sut.GetCurrentRecord();

			//Asset
			Assert.AreEqual(null, actual);
		}

		[Test]
		public void TestDataSetGetCurrentRecordWithDataHalf()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);

			//Act
			for (int i = 0; i < (COUNT_TEST_RECORDS / 2) - 1; i++) //Half, without the one we're interested in
			{
				sut.GetNextRecord();
			}
			IDataRecord actual = sut.GetCurrentRecord();

			//Asset
			Assert.AreEqual(testDataRecords[(COUNT_TEST_RECORDS / 2) - 1 - 1], actual); //-1 is because index of array, another for currentRecord
		}

		[Test]
		public void TestDataSetGetCurrentRecordWithDataLastRecord()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);

			//Act
			for (int i = 0; i < COUNT_TEST_RECORDS; i++) //Skip to the last one
			{
				sut.GetNextRecord();
			}
			IDataRecord actual = sut.GetCurrentRecord();

			//Asset
			Assert.AreEqual(testDataRecords[testDataRecords.Count - 1], actual); //-1 is because index of array
		}

		[Test]
		public void TestDataSetGetCurrentRecordWithDataAfterLastRecord()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);

			//Act
			for (int i = 0; i < COUNT_TEST_RECORDS + 5; i++) //Skip to the last one
			{
				sut.GetNextRecord();
			}
			IDataRecord actual = sut.GetCurrentRecord(); //Get that extra

			//Asset
			Assert.AreEqual(testDataRecords[testDataRecords.Count - 1], actual);
		}
		
		[Test]
		public void TestDataSetGetCurrentRecordWithDataAfterResetStart()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);

			//Act
			sut.Reset();
			IDataRecord actual = sut.GetCurrentRecord();

			//Asset
			Assert.AreEqual(null, actual);
		}

		[Test]
		public void TestDataSetGetCurrentRecordWithDataAfterResetHalf()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);

			//Act
			for (int i = 0; i < COUNT_TEST_RECORDS / 2; i++) //Go half way
			{
				sut.GetNextRecord();
			}
			sut.Reset();
			IDataRecord actual = sut.GetCurrentRecord(); //Get that extra

			//Asset
			Assert.AreEqual(null, actual);
		}

		[Test]
		public void TestDataSetGetCurrentRecordWithDataAfterResetFull()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);

			//Act
			for (int i = 0; i < COUNT_TEST_RECORDS; i++) //Skip to the last one
			{
				sut.GetNextRecord();
			}
			sut.Reset();
			IDataRecord actual = sut.GetCurrentRecord(); //Get that extra

			//Asset
			Assert.AreEqual(null, actual);
		}

		[Test]
		public void TestDataSetGetCurrentRecordWithDataAfterResetAfterLastRecordFull()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);

			//Act
			for (int i = 0; i < COUNT_TEST_RECORDS + 5; i++) //Skip to the last one, plus more
			{
				sut.GetNextRecord();
			}
			sut.Reset();
			IDataRecord actual = sut.GetCurrentRecord(); //Get that extra

			//Asset
			Assert.AreEqual(null, actual);
		}

		[Test]
		public void TestDataSetGetNextRecordWithDataAfterReset()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);

			//Act
			sut.Reset();
			IDataRecord actual = sut.GetNextRecord();

			//Asset
			Assert.AreEqual(testDataRecords[0], actual);
		}

		[Test]
		public void TestDataSetGetNextRecordWithDataAfterResetHalf()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);

			//Act
			for (int i = 0; i < COUNT_TEST_RECORDS / 2; i++) //Half
			{
				sut.GetNextRecord();
			}
			sut.Reset();
			IDataRecord actual = sut.GetNextRecord();

			//Asset
			Assert.AreEqual(testDataRecords[0], actual);
		}

		[Test]
		public void TestDataSetGetNextRecordWithDataAfterResetFull()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);

			//Act
			for (int i = 0; i < COUNT_TEST_RECORDS; i++) //Skip to the last one
			{
				sut.GetNextRecord();
			}
			sut.Reset();
			IDataRecord actual = sut.GetNextRecord();

			//Asset
			Assert.AreEqual(testDataRecords[0], actual);
		}

		[Test]
		public void TestDataSetGetNextRecordWithDataAfterResetAfterLastRecord()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);

			//Act
			for (int i = 0; i < COUNT_TEST_RECORDS + 5; i++) //Skip to the last one, plus some more
			{
				sut.GetNextRecord();
			}
			sut.Reset();
			IDataRecord actual = sut.GetNextRecord();

			//Asset
			Assert.AreEqual(testDataRecords[0], actual);
		}

		[Test]
		public void TestDataSetCountWithDataAfterReset()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);

			//Act
			sut.Reset();
			int actual = sut.Count();

			//Asset
			Assert.AreEqual(COUNT_TEST_RECORDS, actual);
		}

		[Test]
		public void TestDataSetCountRemainingWithDataAfterReset()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);

			//Act
			sut.Reset();
			int actual = sut.CountRemaining();

			//Asset
			Assert.AreEqual(COUNT_TEST_RECORDS, actual);
		}

		[Test]
		public void TestDataSetSplitWithDataHalf()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);
			double splitPercentage = 0.5;

			//Act
			Tuple<IDataSet, IDataSet> pair = sut.Split(splitPercentage);

			//Asset
			Assert.AreEqual(COUNT_TEST_RECORDS, pair.Item1.Count() + pair.Item2.Count());
			Assert.AreEqual(COUNT_TEST_RECORDS * splitPercentage, pair.Item1.Count());
			Assert.AreEqual(COUNT_TEST_RECORDS - (COUNT_TEST_RECORDS * splitPercentage), pair.Item2.Count());
			for (int i = 0; i < COUNT_TEST_RECORDS; i++)
			{
				IDataRecord idr = sut.GetNextRecord();
				if (i < pair.Item1.Count())
				{
					IDataRecord idrp1 = pair.Item1.GetNextRecord();
					Assert.AreEqual(idr, idrp1);
				}
				else
				{
					IDataRecord idrp2 = pair.Item2.GetNextRecord();
					Assert.AreEqual(idr, idrp2);
				}
			}
		}

		[Test]
		public void TestDataSetSplitWithDataEighty()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);
			double splitPercentage = 0.8;

			//Act
			Tuple<IDataSet, IDataSet> pair = sut.Split(splitPercentage);

			//Asset
			Assert.AreEqual(COUNT_TEST_RECORDS, pair.Item1.Count() + pair.Item2.Count());
			Assert.AreEqual(COUNT_TEST_RECORDS * splitPercentage, pair.Item1.Count());
			Assert.AreEqual(COUNT_TEST_RECORDS - (COUNT_TEST_RECORDS * splitPercentage), pair.Item2.Count());
			for (int i = 0; i < COUNT_TEST_RECORDS; i++)
			{
				IDataRecord idr = sut.GetNextRecord();
				if (i < pair.Item1.Count())
				{
					IDataRecord idrp1 = pair.Item1.GetNextRecord();
					Assert.AreEqual(idr, idrp1);
				}
				else
				{
					IDataRecord idrp2 = pair.Item2.GetNextRecord();
					Assert.AreEqual(idr, idrp2);
				}
			}
		}

		[Test]
		public void TestDataSetSplitWithDataOneHundred()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);
			double splitPercentage = 1.0;

			//Act
			Tuple<IDataSet, IDataSet> pair = sut.Split(splitPercentage);

			//Asset
			Assert.AreEqual(COUNT_TEST_RECORDS, pair.Item1.Count() + pair.Item2.Count());
			Assert.AreEqual(COUNT_TEST_RECORDS * splitPercentage, pair.Item1.Count());
			Assert.AreEqual(COUNT_TEST_RECORDS - (COUNT_TEST_RECORDS * splitPercentage), pair.Item2.Count());
			for (int i = 0; i < COUNT_TEST_RECORDS; i++)
			{
				IDataRecord idr = sut.GetNextRecord();
				if (i < pair.Item1.Count())
				{
					IDataRecord idrp1 = pair.Item1.GetNextRecord();
					Assert.AreEqual(idr, idrp1);
				}
				else
				{
					IDataRecord idrp2 = pair.Item2.GetNextRecord();
					Assert.AreEqual(idr, idrp2);
				}
			}
		}

		[Test]
		public void TestDataSetSplitWithDataOverOneHundred()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);
			double splitPercentage = 1.5;

			//Act
			ActualValueDelegate<object> testDelegate = () => sut.Split(splitPercentage);

			//Assert
			Assert.That(testDelegate, Throws.TypeOf<ArgumentException>());
		}

		[Test]
		public void TestDataSetSplitWithDataZero()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);
			double splitPercentage = 0.0;

			//Act
			Tuple<IDataSet, IDataSet> pair = sut.Split(splitPercentage);

			//Asset
			Assert.AreEqual(COUNT_TEST_RECORDS, pair.Item1.Count() + pair.Item2.Count());
			Assert.AreEqual(COUNT_TEST_RECORDS * splitPercentage, pair.Item1.Count());
			Assert.AreEqual(COUNT_TEST_RECORDS - (COUNT_TEST_RECORDS * splitPercentage), pair.Item2.Count());
			for (int i = 0; i < COUNT_TEST_RECORDS; i++)
			{
				IDataRecord idr = sut.GetNextRecord();
				if (i < pair.Item1.Count())
				{
					IDataRecord idrp1 = pair.Item1.GetNextRecord();
					Assert.AreEqual(idr, idrp1);
				}
				else
				{
					IDataRecord idrp2 = pair.Item2.GetNextRecord();
					Assert.AreEqual(idr, idrp2);
				}
			}
		}

		[Test]
		public void TestDataSetSplitWithDataNegative()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);
			double splitPercentage = -0.5;

			//Act
			ActualValueDelegate<object> testDelegate = () => sut.Split(splitPercentage);

			//Assert
			Assert.That(testDelegate, Throws.TypeOf<ArgumentException>());
		}

		[Test]
		public void TestDataSetCloneWithData()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);

			//Act
			IDataSet clone = (IDataSet)sut.Clone();

			//Asset
			Assert.AreEqual(COUNT_TEST_RECORDS, clone.Count());
			Assert.AreEqual(sut.CountRemaining(), clone.CountRemaining());
			for (int i = 0; i < COUNT_TEST_RECORDS; i++)
			{
				IDataRecord idrSut = sut.GetNextRecord();
				IDataRecord idrClone = clone.GetNextRecord();
				Assert.AreEqual(idrSut.GetInputs(), idrClone.GetInputs());
				Assert.AreEqual(idrSut.GetOutputs(), idrClone.GetOutputs());
			}
		}

		[Test]
		public void TestDataSetCloneWithDataAdvanceCursor()
		{
			//Arrange
			DataSet sut = new DataSet(testDataRecords);

			//Act
			for (int i = 0; i < COUNT_TEST_RECORDS / 2; i++) //Advance through half
			{
				sut.GetNextRecord();
			}
			IDataSet clone = (IDataSet)sut.Clone();

			//Asset
			Assert.AreEqual(COUNT_TEST_RECORDS, clone.Count());
			Assert.AreEqual(sut.CountRemaining(), clone.CountRemaining());
			sut.Reset();
			clone.Reset();
			for (int i = 0; i < COUNT_TEST_RECORDS; i++)
			{
				IDataRecord idrSut = sut.GetNextRecord();
				IDataRecord idrClone = clone.GetNextRecord();
				Assert.AreEqual(idrSut.GetInputs(), idrClone.GetInputs());
				Assert.AreEqual(idrSut.GetOutputs(), idrClone.GetOutputs());
			}
		}
	}
}
