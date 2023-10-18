using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBrainQuestionOne.Models
{
	public interface IAppSettingConfig
	{
		string APiBaseUrl { get; set; }
	}

	public class AppSettingConfig : IAppSettingConfig
	{
		public string APiBaseUrl { get; set; }
	}
}
