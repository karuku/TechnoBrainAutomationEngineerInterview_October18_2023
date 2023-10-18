using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBrainQuestionOne.Models
{
	public class WeatherReq
	{
		public int Page { get; set; } = 0;
		public int Count { get; set; } = 10;
	}
}
