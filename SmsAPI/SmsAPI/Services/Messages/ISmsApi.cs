using SmsAPI.Entitites;
using SmsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsAPI.Services.Messages
{
    public interface ISmsApi
    {
        public bool SendSms(SmsMessageModel sms);
        public Task<SmsServiceMessageResultModel> SendSmsApiAsync(SmsServiceMessageModel sms);
        public Task<SmsServiceMessageResultModel> SendSmsApiAccountBalanceAsync();
        public Task<List<SmsLog>> SendSmsApiDataAsync();
        public Task<double> GetSmsApiTotalCostAsync();
    }
}
