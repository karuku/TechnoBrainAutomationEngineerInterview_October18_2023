using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using TechnoBrainQuestionOne.Models;
using System.Net;

namespace TechnoBrainQuestionOne
{
	public interface ISampleApiClient
	{
		Task<IResBase<IEnumerable<WeatherModel>>> GetTemperaturesAsync(WeatherReq request);
	}

	public class SampleApiClient : ISampleApiClient
	{
		private readonly string _baseUrl;
		private readonly RestClient _restClient;
		public SampleApiClient(IAppSettingConfig config)
		{
			_baseUrl = config.APiBaseUrl;
			_restClient = new RestClient(_baseUrl);
		}

		public async Task<IResBase<IEnumerable<WeatherModel>>> GetTemperaturesAsync(WeatherReq request)
		{
			var url = UrisHelper.WeatherUrl;
			var response = await GetRequestAsync<List<WeatherModel>>(url);
			return new ResBase<IEnumerable<WeatherModel>>()
			{
				ErrorResponse = response.ErrorRes,
				SuccessResponse = response.SuccessResponse,
				IsSuccess = response.IsSuccessful,
				StatusCode=response.statusCode
			};
		}
		private async Task<(S SuccessResponse, ErrorRes ErrorRes, bool IsSuccessful,HttpStatusCode statusCode)> GetRequestAsync<S>(string resourceUrl) where S : new()
		{
			try
			{
				var client = _restClient;
				RestRequest restRequest = new RestRequest(resourceUrl, Method.GET);
				restRequest.AddHeader("Content-Type", "application/json");

				var response = await client.ExecuteAsync(restRequest);

				(S SuccessResponse, ErrorRes ErrorRes, bool IsSuccessful, HttpStatusCode statusCode) result = (new S(), null, false,response.StatusCode);
				if (!response.IsSuccessful)
				{
					var displayError = response.ErrorMessage;
					if (!string.IsNullOrWhiteSpace(response.Content))
						result.ErrorRes = JsonConvert.DeserializeObject<ErrorRes>(response.Content);
					else if (response.ErrorException != null)
					{
						var internetError = "kindly check your internet connection and try again.";
						var innerError = response.ErrorException.GetBaseException().Message;
						if (innerError.ToLower().Contains("connection attempt failed"))
						{
							displayError = $"No internet connection, {internetError}";
						}
						else if (innerError.ToLower().Contains("timed out"))
						{
							displayError = $"Timed out, Possibly due to no internet connection, {internetError}";
						}
						else if (innerError.ToLower().Contains("no such host"))
						{
							displayError = $"Connection error, {internetError}";
						}
						result.ErrorRes = new ErrorRes
						{
							ErrorMessage = displayError,
							ErrorCode = response.StatusCode.ToString(),
						};
					}
					else
					{
						result.ErrorRes = new ErrorRes
						{
							ErrorMessage = "Unknown Api Error occured in sample Api but wasn't communicated",
							ErrorCode = response.StatusCode.ToString()
						};
					}
					result.statusCode = HttpStatusCode.BadRequest;
					result.IsSuccessful = false;

				}
				else
				{
					result.statusCode = HttpStatusCode.OK;
					result.SuccessResponse = JsonConvert.DeserializeObject<S>(response.Content);
					result.IsSuccessful = true;

				}

				return result;
			}
			catch (Exception ex)
			{
				throw;
			}

		}

		private void HandleRestExceptions(IRestResponse response, string message)
		{
			message += ". " +
				(string.IsNullOrWhiteSpace(response.ErrorMessage) ? "" : response.ErrorMessage + ". ") +
				(response.ErrorException != null ? response.ErrorException.Message : "");
			throw new Exception(message);
		}

	}
}
