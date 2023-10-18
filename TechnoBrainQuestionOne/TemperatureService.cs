using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoBrainQuestionOne.Models;

namespace TechnoBrainQuestionOne
{
	public interface ITemperatureService
	{
		Task ShowTemperatures();
		Task<IResBase<IEnumerable<WeatherModel>>> GetTemperatures();
	}

	public class TemperatureService : ITemperatureService
	{
		public TemperatureService()
		{

		}

		public async Task ShowTemperatures()
		{
			var res = await GetTemperatures();
			if (res.IsSuccess)
			{
				var datas = res.SuccessResponse;
			
				foreach (var data in datas)
					Console.WriteLine($"{data.TemperatureC}");
			}
			else
			{
				var data = res.ErrorResponse;
				
				Console.WriteLine($"{data.ErrorMessage}");
			}
		}
		public async Task<IResBase<IEnumerable<WeatherModel>>> GetTemperatures()
		{
			var client = Program.AppServiceProvider.GetRequiredService<ISampleApiClient>();

			var res = await client.GetTemperaturesAsync(new Models.WeatherReq());
			if (res.IsSuccess)
			{
				var datas = res.SuccessResponse;
				return res;
			}
			else
			{
				var data = res.ErrorResponse;
				throw new Exception(data.ErrorMessage);
			}
		}

	}
}
