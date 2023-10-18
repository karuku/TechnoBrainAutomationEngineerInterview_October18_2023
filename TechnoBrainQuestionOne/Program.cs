using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TechnoBrainQuestionOne.Extensions;

namespace TechnoBrainQuestionOne
{
	static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();
			AppServiceProvider = host.Services;

			Console.WriteLine("Began TechnoBrain Question One");

			ITemperatureService temperatureService = new TemperatureService();

			Action temperaturesAction = async () => { await temperatureService.ShowTemperatures(); };
			temperaturesAction.Invoke();

			Console.ReadLine();
		}
		
		static void FileProcessor(string filename)
		{
			try 
			{
				var sr = File.OpenText(filename);

			}
			catch(Exception ex)
			{

			}
		}

		static IHostBuilder CreateHostBuilder(string[] args)
		{
			IHostBuilder hostbuilder = new HostBuilder();
			return hostbuilder
				.ConfigureAppConfiguration((hostingContext, config) =>
				{
					config.AddJsonFile("appsettings.json", false, true);
				})
				.ConfigureServices(async (context, services) =>
				{
					var appSettings = context.Configuration.GetAppSettingConfiguration();
					services
					.AddAppServices(context.Configuration);

				});
		}

		public static IServiceProvider AppServiceProvider { get; private set; }

	}
}
