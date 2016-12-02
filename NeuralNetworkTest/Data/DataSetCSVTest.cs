using NUnit.Framework;
using NeuralNetwork.Data;

namespace NeuralNetworkTest.Data
{
	[TestFixture]
	class DataSetCSVTest
	{
		const string NON_EXISTANT_FILE = "non-existant.txt";
		const string BINARY_FILE = "binary-fail.dat";
		const string NO_CONFIG_LINE = "no-config-line.txt";
		const string INCOMPLETE_CONFIG_LINE = "incomplete-config-line.txt";
		const string OVERZEALOUS_CONFIG_LINE = "overzealous-config-line.txt";
		const string EMPTY_AFTER_CONFIG_LINE = "empty-after-config-line.txt";
		const string EMPTY_LINE_START = "empty-line-start.txt";
		const string EMPTY_LINE_MID = "empty-line-mid.txt";
		const string EMPTY_LINE_END = "empty-line-end.txt";
		const string COMMENT_AT_START = "comment-at-start.txt";
		const string COMMENTS_AT_START = "comments-at-start.txt";
		const string COMMENTS_DURING_FILE = "comments-during-file.txt";
		const string INCORRECT_COUNT_INPUTS = "incorrect-count-inputs.txt";
		const string INCORRECT_COUNT_OUTPUTS = "incorrect-count-outputs.txt";
		const string CORRECT_FILE = "correct-file.txt";

		[OneTimeSetUp]
		protected void Init()
		{
		}

		[SetUp]
		protected void SetUp()
		{
		}

		[Test]
		public void DataSetCSVFileDoesNotExist()
		{

		}

		//File doesn't exists
		//Binary file
		//No config line
		//Incomplete config line
		//Overzealous config line
		//Empty after config line
		//Empty line start
		//Empty line mid
		//Comment at start
		//Comments at start
		//Comments during file
		//Line with wrong number of inputs
		//Line with wrong number of outputs
		//Working correctly on 100 file
		//Clone working
	}
}
