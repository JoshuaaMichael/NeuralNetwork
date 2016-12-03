using System;
using System.Collections.Generic;

namespace NeuralNetworkLibrary.Data
{
	public class DataSet : IDataSet
	{
		protected static Random rand = new Random();

		protected List<IDataRecord> dataRecords;
		protected int currentIndex;
		const int EMPTY_INDEX = -1;

		public DataSet()
		{
			dataRecords = new List<IDataRecord>();
			currentIndex = EMPTY_INDEX;
		}

		public DataSet(List<IDataRecord> dataRecords)
		{
			this.dataRecords = dataRecords;
			currentIndex = -1;
		}

		public virtual void ImportData() { }

		public int Count()
		{
			return dataRecords.Count;
		}

		public int CountRemaining()
		{
			return dataRecords.Count - currentIndex - 1;
		}

		public IDataRecord GetNextRecord()
		{
			if (currentIndex + 1 > dataRecords.Count - 1)
			{
				return null;
			}
			else
			{
				currentIndex += 1;
				return dataRecords[currentIndex];
			}
		}

		public IDataRecord GetCurrentRecord()
		{
			if (currentIndex > dataRecords.Count - 1 || currentIndex == EMPTY_INDEX)
			{
				return null;
			}
			else
			{
				return dataRecords[currentIndex];
			}
		}

		public void Reset()
		{
			currentIndex = EMPTY_INDEX;
		}

		public void Shuffle()
		{
			int n = dataRecords.Count;
			while (n > 1)
			{
				n--;
				int k = rand.Next(n + 1);
				IDataRecord value = dataRecords[k];
				dataRecords[k] = dataRecords[n];
				dataRecords[n] = value;
			}
		}

		public Tuple<IDataSet, IDataSet> Split(double percentage)
		{
			if(percentage < 0 || percentage > 1)
			{
				throw new ArgumentException("Invalid percentage");
			}

			int countFirst = (int)(dataRecords.Count * percentage);
			List<IDataRecord> first = new List<IDataRecord>();
			List<IDataRecord> second = new List<IDataRecord>();
			for (int i = 0; i < dataRecords.Count; i++)
			{
				if (i < countFirst)
				{
					first.Add(dataRecords[i]);
				}
				else
				{
					second.Add(dataRecords[i]);
				}
			}
			return new Tuple<IDataSet, IDataSet>(new DataSet(first), new DataSet(second));
		}

		public virtual object Clone()
		{
			DataSet ds = new DataSet();
			List<IDataRecord> idr = new List<IDataRecord>();

			foreach (IDataRecord dr in dataRecords)
			{
				ds.dataRecords.Add((IDataRecord)dr.Clone());
			}

			ds.currentIndex = currentIndex;
			return ds;
		}
	}
}
