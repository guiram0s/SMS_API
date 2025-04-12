using Microsoft.Extensions.Logging;
using SmsAPI.Entitites;
using SmsAPI.Models;
using SmsAPI.Services.Messages;
using SmsAPI.Services.Tracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsAPI.BL
{
    public class Messages : IMessages
    {
        private readonly ISmsBulk _smsBulk;
        private readonly ISmsApi _smsApi;
        private readonly ISmsTwilio _smsTwilio;
        private readonly ILogger<Messages> _logger;
        private readonly IMessageTracking _messageTracking;
        public Messages(ISmsBulk smsBulk, ISmsApi smsApi, ISmsTwilio smsTwilio, IMessageTracking messageTracking, ILogger<Messages> logger)
        {
            _logger = logger;
            _smsApi = smsApi;
            _smsBulk = smsBulk;
            _smsTwilio = smsTwilio;
            _messageTracking = messageTracking;
        }

        #region SMSBULK
        public async Task<SmsServiceMessageResultModel> GetSmsBulkAccountBalanceAsync()
        {
            _logger.LogInformation("Business Logic: SendSmsAccountBalanceBulkAsync Begin");
            var smsBulkMessageResultModel = await _smsBulk.SendSmsBulkAccountBalanceAsync();
            _logger.LogInformation("Business Logic: SendSmsBulkAccountBalanceAsync End");

            return smsBulkMessageResultModel;
        }

        public async Task<SmsServiceMessageResultModel> SendSmsBulkAsync(SmsServiceMessageModel sms)
        {
            _logger.LogInformation("Business Logic: SendSmsBulkAsync Begin");
            var smsBulkMessageResultModel = await _smsBulk.SendSmsBulkAsync(sms);
            var result = await _messageTracking.AddSmsBulkAsync(sms, smsBulkMessageResultModel);
            _logger.LogInformation("Business Logic: SendSmsBulkAsync End");

            return smsBulkMessageResultModel;

        }

        public async Task<List<SmsLog>> GetSmsBulkDataAsync()
        {
            _logger.LogInformation("Business Logic - Message: SendSmsBulkDataAsync() Begin");

            var smsBulkMessageResultModel = await _smsBulk.SendSmsBulkDataAsync();

            _logger.LogInformation("Business Logic - Message: SendSmsSmsBulkDataAsync() End");
            return smsBulkMessageResultModel;
        }

        public async Task<double> GetSmsBulkTotalPriceAsync()
        {
            _logger.LogInformation("Business Logic - Message: SendSmsBulkTotalPriceAsync() Begin");

            var smsBulkMessageResultModel = await _smsBulk.SendSmsBulkTotalCostAsync();

            _logger.LogInformation("Business Logic - Message: SendSmsBulkTotalPriceAsync() End");
            return smsBulkMessageResultModel;
        }

        #endregion

        #region SMSAPI
        public async Task<SmsServiceMessageResultModel> GetSmsApiAccountBalanceAsync()
        {
            _logger.LogInformation("Business Logic: SendSmsAccountBalanceApiAsync Begin");
            var smsApiMessageResultModel = await _smsApi.SendSmsApiAccountBalanceAsync();
            _logger.LogInformation("Business Logic: SendSmsApiAccountBalanceAsync End");

            return smsApiMessageResultModel;
        }

        public async Task<SmsServiceMessageResultModel> SendSmsApiAsync(SmsServiceMessageModel sms)
        {
            _logger.LogInformation("Business Logic: SendSmsApiAsync Begin");
            var smsApiMessageResultModel = await _smsApi.SendSmsApiAsync(sms);
            var result = await _messageTracking.AddSmsApiAsync(sms, smsApiMessageResultModel);
            _logger.LogInformation("Business Logic: SendSmsApiAsync Begin");

            return smsApiMessageResultModel;
        }

        public async Task<List<SmsLog>> GetSmsApiDataAsync()
        {
            _logger.LogInformation("Business Logic - Message: SendSmsApiDataAsync() Begin");

            var smsApiMessageResultModel = await _smsApi.SendSmsApiDataAsync();

            _logger.LogInformation("Business Logic - Message: SendSmsSmsApiDataAsync() End");
            return smsApiMessageResultModel;
        }

        public async Task<double> GetSmsApiTotalPriceAsync()
        {
            _logger.LogInformation("Business Logic - Message: SendSmsApiTotalPriceAsync() Begin");

            var smsBulkMessageResultModel = await _smsApi.GetSmsApiTotalCostAsync();

            _logger.LogInformation("Business Logic - Message: SendSmsApiTotalPriceAsync() End");
            return smsBulkMessageResultModel;
        }
        #endregion

        #region Twilio

        public async Task<SmsServiceMessageResultModel> GetSmsTwilioCostAsync()
        {
            _logger.LogInformation("Business Logic - Message: GetSmsTwilioCostAsync() Begin");

            var smsTwilioMessageResultModel = await _smsTwilio.SendSmsTwilioCostAsync();

            _logger.LogInformation("Business Logic - Message: GetSmsTwilioCostAsync() End");
            return smsTwilioMessageResultModel;
        }
        public async Task<SmsServiceMessageResultModel> GetSmsTwilioAccountBalanceAsync()
        {
            _logger.LogInformation("Business Logic - Message: GetSmsTwilioAccountBalanceAsync() Begin");

            var smsTwilioMessageResultModel = await _smsTwilio.SendSmsTwilioAccountBalanceAsync();
            
            _logger.LogInformation("Business Logic - Message: GetSmsTwilioAccountBalanceAsync() End");
            return smsTwilioMessageResultModel;
        }
        public async Task<SmsServiceMessageResultModel> SendSmsTwilioAsync(SmsServiceMessageModel sms)
        {
            _logger.LogInformation("Business Logic - Message: SendSmsTwilioAsync() Begin");

            var smsTwilioMessageResultModel = await _smsTwilio.SendSmsTwilioAsync(sms);
            var result = await _messageTracking.AddSmsTwilioAsync(sms, smsTwilioMessageResultModel);

            _logger.LogInformation("Business Logic - Message: SendSmsTwilioAsync() End");
            return smsTwilioMessageResultModel;
        }

        public async Task<SmsServiceMessageResultModel> SendSmsTwilioWppAsync(SmsServiceMessageModel sms)
        {
            _logger.LogInformation("Business Logic - Message: SendSmsTwilioWppAsync() Begin");

            var smsTwilioMessageResultModel = await _smsTwilio.SendSmsTwilioWppAsync(sms);
            var result = await _messageTracking.AddSmsTwilioWppAsync(sms, smsTwilioMessageResultModel);

            _logger.LogInformation("Business Logic - Message: SendSmsTwilioWppAsync() End");
            return smsTwilioMessageResultModel;
        }

        public async Task<List<SmsLog>> GetSmsTwilioDataAsync()
        {
            _logger.LogInformation("Business Logic - Message: SendSmsTwilioWppAsync() Begin");

            var smsTwilioMessageResultModel = await _smsTwilio.SendSmsTwilioDataAsync();

            _logger.LogInformation("Business Logic - Message: SendSmsTwilioWppAsync() End");
            return smsTwilioMessageResultModel;
        }

        public async Task<double> GetSmsTwilioTotalPriceAsync()
        {
            _logger.LogInformation("Business Logic - Message: SendSmsTwilioWppAsync() Begin");

            var smsTwilioMessageResultModel = await _smsTwilio.SendSmsTwilioTotalCostAsync();

            _logger.LogInformation("Business Logic - Message: SendSmsTwilioWppAsync() End");
            return smsTwilioMessageResultModel;
        }
        #endregion
    }
    }
