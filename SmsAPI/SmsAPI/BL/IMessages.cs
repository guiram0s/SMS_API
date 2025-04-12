using SmsAPI.Entitites;
using SmsAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmsAPI.BL
{
    public interface IMessages
    {
        //----------------------------------------------Bulk
        public Task<SmsServiceMessageResultModel> SendSmsBulkAsync(SmsServiceMessageModel sms);
        public Task<SmsServiceMessageResultModel> GetSmsBulkAccountBalanceAsync();
        public Task<List<SmsLog>> GetSmsBulkDataAsync();
        public Task<double> GetSmsBulkTotalPriceAsync();
        //----------------------------------------------Api
        public Task<SmsServiceMessageResultModel> SendSmsApiAsync(SmsServiceMessageModel sms);
        public Task<SmsServiceMessageResultModel> GetSmsApiAccountBalanceAsync();
        public Task<List<SmsLog>> GetSmsApiDataAsync();
        public Task<double> GetSmsApiTotalPriceAsync();
        //----------------------------------------------Twilio
        public Task<SmsServiceMessageResultModel> SendSmsTwilioAsync(SmsServiceMessageModel sms);
        public Task<SmsServiceMessageResultModel> SendSmsTwilioWppAsync(SmsServiceMessageModel sms);
        public Task<SmsServiceMessageResultModel> GetSmsTwilioAccountBalanceAsync();
        public Task<SmsServiceMessageResultModel> GetSmsTwilioCostAsync();
        public Task<List<SmsLog>> GetSmsTwilioDataAsync();
        public Task<double> GetSmsTwilioTotalPriceAsync();
    }
}