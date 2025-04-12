using SmsAPI.Models;
using System.Threading.Tasks;

namespace SmsAPI.Services.Tracking
{
    public interface IMessageTracking
    {
        public Task<int> AddSmsBulkAsync(SmsServiceMessageModel sms, SmsServiceMessageResultModel smsResult );
        public Task<int> AddSmsApiAsync(SmsServiceMessageModel sms, SmsServiceMessageResultModel smsResult);
        public Task<int> AddSmsTwilioAsync(SmsServiceMessageModel sms, SmsServiceMessageResultModel smsResult);
        public Task<int> AddSmsTwilioWppAsync(SmsServiceMessageModel sms, SmsServiceMessageResultModel smsResult);
    }
}