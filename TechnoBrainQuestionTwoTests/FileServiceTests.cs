using System;
using Xunit;
using TechnoBrainQuestionTwo;
using System.IO;

namespace TechnoBrainQuestionTwoTests
{
	public class FileServiceTests
	{
		private IFileService _fileService;
		[Fact]
		public void ProcessFileWithEmptyPathTest()
		{
			_fileService = new FileService();
			var path = "";
			Action processFileWithEmptyPathAction = () => _fileService.ProcessFile(path);

			Assert.Throws<FileNotFoundException>(processFileWithEmptyPathAction);
		}
		[Fact]
		public void ProcessFileWithInvalidFilePathTest()
		{
			_fileService = new FileService();
			var path = "sampleFile.xml";
			Action processFileAction = () => _fileService.ProcessFile(path);

			Assert.Throws<Exception>(processFileAction);
		}
		[Fact]
		public void ProcessFileWithValidFilePathTest()
		{
			_fileService = new FileService();
			var path = "sampleFile.txt";
			Func<int> processFileAction = () => _fileService.ProcessFile(path);

			var count = processFileAction.Invoke();
			var expected = 3;
			Assert.Equal(expected,count);
		}
	}
}
