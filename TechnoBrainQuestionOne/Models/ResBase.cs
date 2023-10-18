using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBrainQuestionOne.Models
{
	public interface IResBase<T>
	{
		bool IsSuccess { get; set; }
		System.Net.HttpStatusCode StatusCode { get; set; }
		ErrorRes ErrorResponse { get; set; }
		T SuccessResponse { get; set; }
	}

	public class ResBase<T> : IResBase<T>
	{
		public bool IsSuccess { get; set; }
		public System.Net.HttpStatusCode StatusCode { get; set; }
		public T SuccessResponse { get; set; }
		public ErrorRes ErrorResponse { get; set; }
	}
}
