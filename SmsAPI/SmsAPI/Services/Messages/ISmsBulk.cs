using SmsAPI.Entitites;
using SmsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsAPI.Services.Messages
{
    public interface ISmsBulk
    {
        public bool SendSms(SmsMessageModel sms);
        public Task<SmsServiceMessageResultModel> SendSmsBulkAsync(SmsServiceMessageModel sms);
        public Task<SmsServiceMessageResultModel> SendSmsBulkAccountBalanceAsync();
        public Task<List<SmsLog>> SendSmsBulkDataAsync();
        public Task<double> SendSmsBulkTotalCostAsync();
    }
}
