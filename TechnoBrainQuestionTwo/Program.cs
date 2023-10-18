using System;

namespace TechnoBrainQuestionTwo
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Question two!");

			IFileService fileService = new FileService();
			var filename = "";
			var count = fileService.ProcessFile(filename);

			Console.WriteLine($"File: {filename} has word count of {count}");

			Console.ReadLine();
		}
	}
}
