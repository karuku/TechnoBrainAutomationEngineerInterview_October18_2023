using System;
using Xunit;
using TechnoBrainQuestionOne;
using TechnoBrainQuestionOne.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Linq;

namespace TechnoBrainQuestionOneTests
{
	public class SampleApiClientTests
	{
		private static readonly string[] temperatureSummaries = new[]
		   {
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};
		[Fact]
		public async Task GetTemperaturesTest()
		{
			IAppSettingConfig config = new AppSettingConfig();
			config.APiBaseUrl = "";
			ISampleApiClient client = new SampleApiClient(config);

			
			var res = await client.GetTemperaturesAsync(new WeatherReq());

			var expectedCode = HttpStatusCode.OK;
			Assert.Equal(expectedCode,res.StatusCode);

			Assert.Equal(temperatureSummaries, res.SuccessResponse?.Select(c => c.Summary));
		}
	}
}
