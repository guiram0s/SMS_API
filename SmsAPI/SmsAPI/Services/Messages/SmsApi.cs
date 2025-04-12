using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmsAPI.Contexts;
using SmsAPI.Entitites;
using SmsAPI.Models;
using SMSApi.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SmsAPI.Services.Messages
{
    public class SmsApi : ISmsApi
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private ILogger<SmsApi> _logger;
        private static readonly HttpClient client = new HttpClient();
		private readonly SmsContext _context;

		public SmsApi(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<SmsApi> logger, SmsContext context)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
			_context = context;
		}

        public bool SendSms(SmsMessageModel sms)
        {
            return true;
        }

		public async Task<SmsServiceMessageResultModel> SendSmsApiAsync(SmsServiceMessageModel sms)
		{
			
			var smsApiMessageResultModel = new SmsServiceMessageResultModel();
			var bodyPost = new Dictionary<string, string>();
			bodyPost.Add("access_token", _configuration.GetSection("SMSAPI").GetValue<string>("access_token"));
			bodyPost.Add("from", _configuration.GetSection("SMSAPI").GetValue<string>("from"));
			bodyPost.Add("to", sms.tel);
			bodyPost.Add("message", sms.msg);

			var httpRequestMessage = new HttpRequestMessage();
			httpRequestMessage.Method = HttpMethod.Post;
			var content = new FormUrlEncodedContent(bodyPost);
			var httpClient = _httpClientFactory.CreateClient();

			var contentJson = new StringContent(JsonConvert.SerializeObject(content, Formatting.Indented));
			Console.WriteLine(contentJson.ReadAsStringAsync());
			try
			{
				var urlSend = Path.Combine(_configuration.GetSection("SMSAPI").GetValue<string>("SmsApiUrl"), _configuration.GetSection("SMSAPI").GetValue<string>("SmsSendApiUrl"));
				var httpResponseMessage = httpClient.PostAsync(urlSend, content).Result;
				//var response = await client.PostAsync("/sms_send.php",content);
				var responseString = await httpResponseMessage.Content.ReadAsStringAsync();


				try
				{
					httpResponseMessage.EnsureSuccessStatusCode();
				}
				catch (HttpRequestException ex)
				{
					_logger.LogError($"Problemas de Conexão ao Serviço: {ex.Message} ");
					smsApiMessageResultModel.isSucess = false;
					smsApiMessageResultModel.MessageResult = ex.Message;
					return smsApiMessageResultModel;
				}

				if (responseString.Substring(0, 2).ToUpper() == "OK")
				{
					//-------
					
					smsApiMessageResultModel.Price = responseString.Substring(responseString.Length - 5);
					smsApiMessageResultModel.MsgId = responseString.Substring(3, responseString.Length- responseString.Substring(responseString.Length - 9).Length); 
					smsApiMessageResultModel.isSucess = true;
					smsApiMessageResultModel.MessageResult = responseString;
					return smsApiMessageResultModel;
				}

				_logger.LogError(responseString);
				smsApiMessageResultModel.isSucess = false;
				smsApiMessageResultModel.MessageResult = responseString;
				return smsApiMessageResultModel;
			}
			catch (System.Exception ex)
			{
				_logger.LogError($"Exception: {ex.Message}");
				smsApiMessageResultModel.isSucess = false;
				smsApiMessageResultModel.MessageResult = ex.Message;
				return smsApiMessageResultModel;
			}
			
		}

		public async Task<SmsServiceMessageResultModel> SendSmsApiAccountBalanceAsync()
		{

            var smsApiMessageResultModel = new SmsServiceMessageResultModel();

			var httpRequestMessage = new HttpRequestMessage();
			httpRequestMessage.Method = HttpMethod.Post;
			var httpClient = _httpClientFactory.CreateClient();
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configuration.GetSection("SMSAPI").GetValue<string>("access_token"));
			var contentJson = new StringContent(JsonConvert.SerializeObject(Formatting.Indented));
			Console.WriteLine(contentJson.ReadAsStringAsync());
			try
			{
				var urlSend = Path.Combine(_configuration.GetSection("SMSAPI").GetValue<string>("SmsApiUrl"), _configuration.GetSection("SMSAPI").GetValue<string>("SmsApiAccountBalance"));
				var httpResponseMessage = await httpClient.GetAsync(urlSend);
				var responseString = await httpResponseMessage.Content.ReadAsStringAsync();

				try
				{
					httpResponseMessage.EnsureSuccessStatusCode();
				}
				catch (HttpRequestException ex)
				{
					_logger.LogError($"Problemas de Conexão ao Serviço: {ex.Message} ");
					smsApiMessageResultModel.isSucess = false;
					smsApiMessageResultModel.MessageResult = ex.Message;
					return smsApiMessageResultModel;
				}

				if (responseString.Substring(0, 2).ToUpper() == "OK")
				{
					smsApiMessageResultModel.isSucess = true;
					smsApiMessageResultModel.MessageResult = responseString;
					return smsApiMessageResultModel;
				}

				_logger.LogError(responseString);
				smsApiMessageResultModel.isSucess = false;
				smsApiMessageResultModel.MessageResult = responseString;
				return smsApiMessageResultModel;
			}
			catch (System.Exception ex)
			{
				_logger.LogError($"Exception: {ex.Message}");
				smsApiMessageResultModel.isSucess = false;
				smsApiMessageResultModel.MessageResult = ex.Message;
				return smsApiMessageResultModel;
			}
        }
		public async Task<List<SmsLog>> SendSmsApiDataAsync()
		{
			using (_context)
			{
				var data = _context.SmsLogs
					.Where(b => b.Provider == "SMSAPI")
					.ToList();
				return data;
			}
		}

		public async Task<double> GetSmsApiTotalCostAsync()
        {
			using (_context)
			{
				double total = _context.SmsLogs
				  .Where(p => p.Provider == "SMSAPI")
				  .Select(i => Convert.ToDouble(i.Price)).Sum();

				return total;
			}
		}
	}
}
