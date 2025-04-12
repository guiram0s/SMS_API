using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmsAPI.Contexts;
using SmsAPI.Entitites;
using SmsAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SmsAPI.Services.Messages
{
    public class SmsBulk : ISmsBulk
    {


        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private ILogger<SmsBulk> _logger;
        private readonly SmsContext _context;
        private static readonly HttpClient client = new HttpClient();
        public SmsBulk(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<SmsBulk> logger, SmsContext context)
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


        public async Task<SmsServiceMessageResultModel> SendSmsBulkAsync(SmsServiceMessageModel sms)
        {
            var smsBulkMessageResultModel = new SmsServiceMessageResultModel();
            var bodyPost = new Dictionary<string, string>();
            bodyPost.Add("uid", _configuration.GetSection("SMSBULKPT").GetValue<string>("uid"));
            bodyPost.Add("pwd", _configuration.GetSection("SMSBULKPT").GetValue<string>("pwd"));
            bodyPost.Add("tel", sms.tel);
            bodyPost.Add("msg", sms.msg);
            bodyPost.Add("rem", _configuration.GetSection("SMSBULKPT").GetValue<string>("rem"));
            bodyPost.Add("dth", string.Empty);
            bodyPost.Add("uni", "1");
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Post;
            var content = new FormUrlEncodedContent(bodyPost);
            var httpClient = _httpClientFactory.CreateClient();

            var contentJson = new StringContent(JsonConvert.SerializeObject(content, Formatting.Indented));
            Console.WriteLine(contentJson.ReadAsStringAsync());
            try
            {
                var urlSend = Path.Combine(_configuration.GetSection("SMSBULKPT").GetValue<string>("SmsBulkUrl"), _configuration.GetSection("SMSBULKPT").GetValue<string>("SmsSend"));
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
                    smsBulkMessageResultModel.isSucess = false;
                    smsBulkMessageResultModel.MessageResult = ex.Message;
                    return smsBulkMessageResultModel;
                }

                if (responseString.Substring(0, 2).ToUpper() == "OK")
                {
                    
                    smsBulkMessageResultModel.Price = responseString.Substring(responseString.Length - 6);
                    smsBulkMessageResultModel.MsgId = responseString.Substring(3,11);
                    smsBulkMessageResultModel.isSucess = true;
                    smsBulkMessageResultModel.MessageResult = responseString;
                    return smsBulkMessageResultModel;
                }

                _logger.LogError(responseString);
                smsBulkMessageResultModel.isSucess = false;
                smsBulkMessageResultModel.MessageResult = responseString;
                return smsBulkMessageResultModel;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}");
                smsBulkMessageResultModel.isSucess = false;
                smsBulkMessageResultModel.MessageResult = ex.Message;
                return smsBulkMessageResultModel;
            }
  
        }

        public async Task<SmsServiceMessageResultModel> SendSmsBulkAccountBalanceAsync()
        {
            var smsBulkMessageResultModel = new SmsServiceMessageResultModel();
        
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Get;
            var httpClient = _httpClientFactory.CreateClient();

            var contentJson = new StringContent(JsonConvert.SerializeObject(Formatting.Indented));
            Console.WriteLine(contentJson.ReadAsStringAsync());
            try
            {
                var urlAccountBalance = Path.Combine(_configuration.GetSection("SMSBULKPT").GetValue<string>("SmsBulkUrl"), _configuration.GetSection("SMSBULKPT").GetValue<string>("SmsAccountBalance"));
                urlAccountBalance = urlAccountBalance + "?uid=" + _configuration.GetSection("SMSBULKPT").GetValue<string>("uid") + "&pwd=" + _configuration.GetSection("SMSBULKPT").GetValue<string>("pwd");
                var httpResponseMessage = await httpClient.GetAsync(urlAccountBalance);
                //var response = await client.PostAsync("/sms_send.php",content);
                var responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                try
                {
                    httpResponseMessage.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException ex)
                {
                    _logger.LogError($"Problemas de Conexão ao Serviço: {ex.Message} ");
                    smsBulkMessageResultModel.isSucess = false;
                    smsBulkMessageResultModel.MessageResult = ex.Message;
                    return smsBulkMessageResultModel;

                }
                

                if (responseString.Substring(0, 2).ToUpper() == "OK")
                {
                    smsBulkMessageResultModel.isSucess = true;
                    smsBulkMessageResultModel.MessageResult = responseString;
                    return smsBulkMessageResultModel;
                }

                _logger.LogError(responseString);
                smsBulkMessageResultModel.isSucess = false;
                smsBulkMessageResultModel.MessageResult = responseString;
                return smsBulkMessageResultModel;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}");
                smsBulkMessageResultModel.isSucess = false;
                smsBulkMessageResultModel.MessageResult = ex.Message;
                return smsBulkMessageResultModel;
            }
        }
        public async Task<List<SmsLog>> SendSmsBulkDataAsync()
        {
            using (_context)
            {
                var data = _context.SmsLogs
                    .Where(b => b.Provider == "SMSBULKPT")
                    .ToList();
                return data;
            }
        }

        public async Task<double> SendSmsBulkTotalCostAsync()
        {
            using (_context)
            {
                //var result = _context.SmsLogs.GroupBy(o => o.Id)
                //   .Select(g => new { membername = g.Key, total = g.Sum(i => Int32.Parse(i.Price)) });
                double total = _context.SmsLogs
                  .Where(p => p.Provider == "SMSBULKPT")
                  .Select(i => Convert.ToDouble(i.Price)).Sum();

                return total;
            }
        }
    }
}
