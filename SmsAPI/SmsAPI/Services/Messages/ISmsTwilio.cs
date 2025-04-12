using SmsAPI.Entitites;
using SmsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsAPI.Services.Messages
{
    public interface ISmsTwilio
    {
        public bool SendSms(SmsMessageModel sms);
        public Task<SmsServiceMessageResultModel> SendSmsTwilioAsync(SmsServiceMessageModel sms);
        public Task<SmsServiceMessageResultModel> SendSmsTwilioWppAsync(SmsServiceMessageModel sms);
        public Task<SmsServiceMessageResultModel> SendSmsTwilioAccountBalanceAsync();
        public Task<SmsServiceMessageResultModel> SendSmsTwilioCostAsync();
        public Task<List<SmsLog>> SendSmsTwilioDataAsync();
        public Task<double> SendSmsTwilioTotalCostAsync();

    }
}
