using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace TechnoBrainQuestionTwo
{
	public interface IFileService
	{
		int Count(string text);
		int ProcessFile(string filename);
		string ReadCsvFile(string filename);
		string ReadJsonFile(string filename);
		string ReadTextFile(string filename);
		void ValidateFile(string filename);
	}

	public class FileService : IFileService
	{
		public const string _validTextRegex = "[^a-zA-Z0-9]";
		public FileService()
		{

		}

		public int ProcessFile(string filename)
		{
			ValidateFile(filename);
			filename = filename.ToLower();
			var text = "";
			var count = 0;
			if (filename.EndsWith(".txt"))
			{
				text = ReadTextFile(filename);
			}
			else if (filename.EndsWith(".csv"))
			{
				text = ReadCsvFile(filename);
			}
			else if (filename.EndsWith(".json"))
			{
				text = ReadJsonFile(filename);
			}
			else
			{
				throw new Exception("file type not supported");
			}

			if (string.IsNullOrWhiteSpace(text))
				throw new Exception("No text in file");

			count = Count(text);
			return count;
		}

		public void ValidateFile(string filename)
		{
			if (string.IsNullOrWhiteSpace(filename) || !File.Exists(filename))
				throw new FileNotFoundException("No File or file doesn't exist");


		}
		public int Count(string text)
		{
			var regx = new Regex("[^a-zA-Z0-9]");
			text = regx.Replace(text, " ");
			string[] textWordArr = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			
			var count = textWordArr.Length;

			return count;
		}
		public string ReadTextFile(string filename)
		{
			ValidateFile(filename);

			var text = File.ReadAllText(filename);
			return text;
		}
		public string ReadCsvFile(string filename)
		{
			ValidateFile(filename);

			var text = File.ReadAllText(filename);
			text.Replace(',', ' ');
			return text;
		}
		public string ReadJsonFile(string filename)
		{
			ValidateFile(filename);

			var text = File.ReadAllText(filename);
			text.Replace(',', ' ');
			return text;
		}
	}
}
