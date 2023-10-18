using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TechnoBrainQuestionOne.Extensions
{
	public static class IServiceCollectionExtensions
	{
		internal static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
		{
			services
				.AddScoped<ISampleApiClient, SampleApiClient>((prov) =>
				{
					var config = configuration.GetAppSettingConfiguration();
					return new SampleApiClient(config);
				});
			return services;
		}
		
	}
}
