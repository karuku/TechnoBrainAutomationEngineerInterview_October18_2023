using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using TechnoBrainQuestionOne.Models;

namespace TechnoBrainQuestionOne.Extensions
{
	public static class IConfigurationExtensions
	{
		public static string _settingsSectionName = "AppSettings";

		public static IAppSettingConfig GetAppSettingConfiguration(this IConfiguration configuration)
		{
			var configModel = configuration.GetSection(_settingsSectionName)
				.Get<AppSettingConfig>();

			return configModel;
		}

	}
}
