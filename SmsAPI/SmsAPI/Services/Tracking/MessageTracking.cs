using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SmsAPI.Contexts;
using SmsAPI.Entitites;
using SmsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsAPI.Services.Tracking
{
    public class MessageTracking : IMessageTracking
    {
        private readonly ILogger<MessageTracking> _logger;
        private readonly SmsContext _context;
        private readonly IConfiguration _configuration;

        public MessageTracking(ILogger<MessageTracking> logger, SmsContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }

        public async Task<int> AddSmsBulkAsync(SmsServiceMessageModel sms, SmsServiceMessageResultModel smsResult)
        {
            try
            {
                _logger.LogInformation("MessageTracking - AddSmsBulk() started.");

                SmsLog smsLog = new SmsLog();

                smsLog.Datetime = DateTime.UtcNow.ToString();
                smsLog.Phone = sms.tel;
                smsLog.Rem = _configuration.GetSection("SMSBULKPT").GetValue<string>("rem");
                smsLog.Result = smsResult.MessageResult;
                smsLog.Status = smsResult.isSucess == true ? "OK" : "ERROR";
                smsLog.Provider = _configuration.GetSection("SMSBULKPT").GetValue<string>("provider");
                smsLog.Price = smsResult.Price;
                smsLog.MessageId = smsResult.MsgId;
                await _context.SmsLogs.AddAsync(smsLog);

                var result = await _context.SaveChangesAsync();
                _logger.LogInformation("MessageTracking - AddSmsBulk() ended.");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                return 0;
            }
            
        }

        public async Task<int> AddSmsApiAsync(SmsServiceMessageModel sms, SmsServiceMessageResultModel smsResult)
        {
            try
            {
                _logger.LogInformation("MessageTracking - AddSmsApi() started.");

                SmsLog smsLog = new SmsLog();

                smsLog.Datetime = DateTime.UtcNow.ToString();
                smsLog.Phone = sms.tel;
                smsLog.Rem = _configuration.GetSection("SMSAPI").GetValue<string>("from");
                smsLog.Result = smsResult.MessageResult;
                smsLog.Status = smsResult.isSucess == true ? "OK" : "ERROR";
                smsLog.Provider = _configuration.GetSection("SMSAPI").GetValue<string>("provider");
                smsLog.Price = smsResult.Price;
                smsLog.MessageId = smsResult.MsgId;
                await _context.SmsLogs.AddAsync(smsLog);

                var result = await _context.SaveChangesAsync();
                _logger.LogInformation("MessageTracking - AddSmsApi() ended.");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                return 0;
            }

        }

        public async Task<int> AddSmsTwilioAsync(SmsServiceMessageModel sms, SmsServiceMessageResultModel smsResult)
        {
            _logger.LogInformation("MessageTracking - AddSmsTwilio() started.");

            SmsLog smsLog = new SmsLog();

            smsLog.Datetime = DateTime.UtcNow.ToString();
            smsLog.Phone = sms.tel;
            smsLog.Rem = _configuration.GetSection("TWILIO").GetValue<string>("from");
            smsLog.Result = smsResult.MessageResult;
            smsLog.Status = smsResult.isSucess == true ? "OK" : "ERROR";
            smsLog.Provider = _configuration.GetSection("TWILIO").GetValue<string>("provider");
            smsLog.Price = smsResult.Price;
            smsLog.MessageId = smsResult.MsgId;
            await _context.SmsLogs.AddAsync(smsLog);

            var result = await _context.SaveChangesAsync();
            _logger.LogInformation("MessageTracking - AddSmsTwilio() ended.");
            return result;
        }
        public async Task<int> AddSmsTwilioWppAsync(SmsServiceMessageModel sms, SmsServiceMessageResultModel smsResult)
        {
            _logger.LogInformation("MessageTracking - AddSmsTwilioWppAsync() started.");

            SmsLog smsLog = new SmsLog();

            smsLog.Datetime = DateTime.UtcNow.ToString();
            smsLog.Phone = sms.tel;
            smsLog.Rem = _configuration.GetSection("TWILIO").GetValue<string>("from");
            smsLog.Result = smsResult.MessageResult;
            smsLog.Status = smsResult.isSucess == true ? "OK" : "ERROR";
            smsLog.Provider = _configuration.GetSection("TWILIO").GetValue<string>("provider");
            smsLog.Price = smsResult.Price;
            smsLog.MessageId = smsResult.MsgId;
            await _context.SmsLogs.AddAsync(smsLog);

            var result = await _context.SaveChangesAsync();
            _logger.LogInformation("MessageTracking - AddSmsTwilioWppAsync() ended.");
            return result;
        }
    }
}
