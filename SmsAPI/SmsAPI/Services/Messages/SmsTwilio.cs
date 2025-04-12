using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmsAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Pricing.V2.Voice;
using Twilio.Rest.Pricing.V1.Messaging;
using System.Data;
using SmsAPI.Contexts;
using SmsAPI.Entitites;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using System.Data.Common;

namespace SmsAPI.Services.Messages
{
    public class SmsTwilio : ISmsTwilio
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<SmsTwilio> _logger;
        private readonly SmsContext _context;
        private static readonly HttpClient client = new HttpClient();
        public SmsTwilio(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<SmsTwilio> logger, SmsContext context)
        {
            _httpClientFactory = httpClientFactory;
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }
        public bool SendSms(SmsMessageModel sms)
        {
            return true;
        }

        public async Task<SmsServiceMessageResultModel> SendSmsTwilioAsync(SmsServiceMessageModel sms)
        {
            var smsTwilioMessageResultModel = new SmsServiceMessageResultModel();
            string accountSid = _configuration.GetSection("TWILIO").GetValue<string>("accountSid");
            string authToken = _configuration.GetSection("TWILIO").GetValue<string>("authToken");

            TwilioClient.Init(accountSid, authToken);

            var message = await MessageResource.CreateAsync(
                body: sms.msg,
                from: new Twilio.Types.PhoneNumber(_configuration.GetSection("TWILIO").GetValue<string>("from")),
                statusCallback: new Uri("http://6208-79-168-75-145.ngrok.io/api/sms/smstwilio/callback"),
                to: sms.tel
            );
            
            var sid = message.Sid;
            var message0 = MessageResource.Fetch(pathSid: sid);
            var status = message0.Status;

            //var number = NumberResource.Fetch(
            //pathDestinationNumber: new Twilio.Types.PhoneNumber(sms.tel)
            //);

            ////var price = number.OutboundCallPrices[0].BasePrice;
            //var isocountry = number.IsoCountry;
            //var country = Twilio.Rest.Pricing.V1.Messaging.CountryResource.Fetch(pathIsoCountry: isocountry);
            //var price = country.OutboundSmsPrices[0].Prices[0].CurrentPrice;
            
            var responseStringPrice = message0.Price;

            var responseStringSId = message0.Sid.ToString();

            var responseStringStatus = status;
            
            var responseString = responseStringStatus + ":" + responseStringSId + ":" + responseStringPrice;
            if (responseString.Substring(0, 7)== "sending" )
            {

                smsTwilioMessageResultModel.Price = responseStringPrice;
                smsTwilioMessageResultModel.MsgId = responseStringSId;
                smsTwilioMessageResultModel.isSucess = true;
                smsTwilioMessageResultModel.MessageResult = responseString;
                return smsTwilioMessageResultModel;
            }

            if (responseString.Substring(0, 4) == "sent" || responseString.Substring(0, 9) == "delivered")
            {

                smsTwilioMessageResultModel.Price = responseStringPrice;
                smsTwilioMessageResultModel.MsgId = responseStringSId;
                smsTwilioMessageResultModel.isSucess = true;
                smsTwilioMessageResultModel.MessageResult = responseString;
                return smsTwilioMessageResultModel;
            }

            _logger.LogError(responseString);
            smsTwilioMessageResultModel.isSucess = false;
            smsTwilioMessageResultModel.MessageResult = responseString;
            Console.WriteLine(message.Sid);
            return smsTwilioMessageResultModel;
        }

        public async Task<SmsServiceMessageResultModel> SendSmsTwilioWppAsync(SmsServiceMessageModel sms)
        {
            var smsTwilioMessageResultModel = new SmsServiceMessageResultModel();
            string accountSid = _configuration.GetSection("TWILIO").GetValue<string>("accountSid");
            string authToken = _configuration.GetSection("TWILIO").GetValue<string>("authToken");

            TwilioClient.Init(accountSid, authToken);

            var message = await MessageResource.CreateAsync(
                body: sms.msg,
                from: new Twilio.Types.PhoneNumber("whatsapp:+19035029234" /*"whatsapp:"+_configuration.GetSection("TWILIO").GetValue<string>("from")*/),
                to: sms.tel
            );

            var sid = message.Sid;
            var message0 = MessageResource.Fetch(pathSid: sid);
            var status = message0.Status;

            var responseStringPrice = message0.Price;

            var responseStringSId = message0.Sid.ToString();

            var responseStringStatus = status;

            var responseString = responseStringStatus + ":" + responseStringSId + ":" + responseStringPrice;
            if (responseString.Substring(0, 7) == "sending")
            {

                smsTwilioMessageResultModel.Price = responseStringPrice;
                smsTwilioMessageResultModel.MsgId = responseStringSId;
                smsTwilioMessageResultModel.isSucess = true;
                smsTwilioMessageResultModel.MessageResult = responseString;
                return smsTwilioMessageResultModel;
            }

            if (responseString.Substring(0, 4) == "sent" || responseString.Substring(0, 9) == "delivered")
            {

                smsTwilioMessageResultModel.Price = responseStringPrice;
                smsTwilioMessageResultModel.MsgId = responseStringSId;
                smsTwilioMessageResultModel.isSucess = true;
                smsTwilioMessageResultModel.MessageResult = responseString;
                return smsTwilioMessageResultModel;
            }

            _logger.LogError(responseString);
            smsTwilioMessageResultModel.isSucess = false;
            smsTwilioMessageResultModel.MessageResult = responseString;
            Console.WriteLine(message.Sid);
            return smsTwilioMessageResultModel;
        }

        public async Task<SmsServiceMessageResultModel> SendSmsTwilioAccountBalanceAsync()
        {
            var smsTwilioMessageResultModel = new SmsServiceMessageResultModel();
            string accountSid = _configuration.GetSection("TWILIO").GetValue<string>("accountSid");
            string authToken = _configuration.GetSection("TWILIO").GetValue<string>("authToken");
            string url = _configuration.GetSection("TWILIO").GetValue<string>("SmsTwilioUrl")+_configuration.GetSection("TWILIO").GetValue<string>("SmsTwilioAccount") + accountSid + "/Balance.json";
            var client = new WebClient();
            client.Credentials = new NetworkCredential(accountSid, authToken);
            string accountBalance = client.DownloadString(url);

            dynamic responseObject = JsonConvert.DeserializeObject<object>(accountBalance);
            var responseString = "Balance: "+responseObject["balance"].Value+ "| Currency: "+responseObject["currency"].Value;

            if (responseObject["balance"].Value!=null)
            {
                smsTwilioMessageResultModel.isSucess = true;
                smsTwilioMessageResultModel.MessageResult = responseString;
                return smsTwilioMessageResultModel;
            }

            
            smsTwilioMessageResultModel.isSucess = false;
            smsTwilioMessageResultModel.MessageResult = responseString;
            return smsTwilioMessageResultModel;
        }

        public async Task<SmsServiceMessageResultModel> SendSmsTwilioCostAsync()
        {
            var smsTwilioMessageResultModel = new SmsServiceMessageResultModel();
            string accountSid = _configuration.GetSection("TWILIO").GetValue<string>("accountSid");
            string authToken = _configuration.GetSection("TWILIO").GetValue<string>("authToken");
            TwilioClient.Init(accountSid, authToken);

            List<string> MsgIds = _context
              .SmsLogs
              .Where(u => u.Price == null)
              .Select(u => u.MessageId)
              .ToList();

            foreach (var smsCost in MsgIds)
            {
                var message0 = MessageResource.Fetch(pathSid: smsCost);
                var price = message0.Price;
                if (price != null)
                {
                   price=price.Replace("-", "");
                }
                var result =_context.SmsLogs.First(u => u.MessageId == smsCost);
                result.Price = price;
                
                _context.SmsLogs.Update(result);

            }
            await _context.SaveChangesAsync();

            var responseString = "sucesso";
            smsTwilioMessageResultModel.isSucess = true;
            smsTwilioMessageResultModel.MessageResult = responseString;
            return smsTwilioMessageResultModel;
        }

        public async Task<List<SmsLog>> SendSmsTwilioDataAsync()
        {
            using (_context)
            {
                var data = _context.SmsLogs
                    .Where(b => b.Provider=="Twilio")
                    .ToList();
                return data;
            }
        }

        public async Task<double> SendSmsTwilioTotalCostAsync()
        {
            double curAmount;
            string url = "http://www.floatrates.com/daily/usd.json";
            string json = new WebClient().DownloadString(url);
            var currency = JsonConvert.DeserializeObject<dynamic>(json);

            using (_context)
            {
                double total = _context.SmsLogs
                  .Where(p => p.Provider == "Twilio")
                  .Select(i => Convert.ToDouble(i.Price)).Sum();

                curAmount = total * Convert.ToSingle(currency.eur.rate);

                return curAmount;
            }
        }
    }
}
