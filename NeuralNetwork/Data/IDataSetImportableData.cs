using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Data
{
	public interface IDataSetImportableData : IDataSet
	{
		void ImportData();
	}
}
