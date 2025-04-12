using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmsAPI.BL;
using SmsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;

namespace SmsAPI.Controllers
{
    [ApiController]
    [Route("api/sms/[controller]")]
    public class SmsTwilioController : Controller
    {
        private IMessages _message;
        private readonly ILogger<SmsTwilioController> _logger;
        public SmsTwilioController(IMessages message, ILogger<SmsTwilioController> logger)
        {
            _logger = logger;
            _message = message;
        }

        [HttpPost]
        public async Task<IActionResult> SendSmsTwilio(SmsServiceMessageModel smsMessage)
        {
            
            _logger.LogInformation("SmsTwilioController: SendSmsTwilio() Begin");
            try
            {
                _logger.LogInformation(JsonConvert.SerializeObject(smsMessage));

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning(JsonConvert.SerializeObject(ModelState));
                    return BadRequest(ModelState);
                }
                if (!smsMessage.tel.StartsWith("+"))
                {
                    smsMessage.tel = "+" + smsMessage.tel;
                }
                var result = await _message.SendSmsTwilioAsync(smsMessage);

                if (result.isSucess)
                {
                    _logger.LogInformation(result.MessageResult);
                    _logger.LogInformation("SmsTwilioController: SendSmsTwilio() end");
                    return Ok(result.MessageResult);
                }

                return StatusCode(StatusCodes.Status424FailedDependency, result.MessageResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"SmsTwilioController: SendSmsTwilio() ended. [HttpPost] - 500InternalServerError - Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        //-------------------------------------------------------------------------------------------------------
        [HttpPost("SendThroughWpp")]
        public async Task<IActionResult> SendSmsTwilioWpp(SmsServiceMessageModel smsMessage)
        {

            _logger.LogInformation("SmsTwilioController: SendSmsTwilioWpp() Begin");
            try
            {
                _logger.LogInformation(JsonConvert.SerializeObject(smsMessage));

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning(JsonConvert.SerializeObject(ModelState));
                    return BadRequest(ModelState);
                }
                if (!smsMessage.tel.StartsWith("+"))
                {
                    smsMessage.tel = "whatsapp:+" + smsMessage.tel;
                }
                var result = await _message.SendSmsTwilioWppAsync(smsMessage);

                if (result.isSucess)
                {
                    _logger.LogInformation(result.MessageResult);
                    _logger.LogInformation("SmsTwilioController: SendSmsTwilioWpp() end");
                    return Ok(result.MessageResult);
                }

                return StatusCode(StatusCodes.Status424FailedDependency, result.MessageResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"SmsTwilioController: SendSmsTwilio() ended. [HttpPost] - 500InternalServerError - Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSmsTwilioAccountBalance()
        {
            _logger.LogInformation("SmsTwilioController: SendSmsTwilioAccountBalance() Begin");
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError(JsonConvert.SerializeObject(ModelState));
                    return BadRequest(ModelState);
                }

                var result = await _message.GetSmsTwilioAccountBalanceAsync();

                if (result.isSucess)
                {
                    _logger.LogInformation(result.MessageResult);
                    _logger.LogInformation("SmsTwilioController: SendSmsTwilioAccountBalance() end");
                    return Ok(result.MessageResult);
                }
                return StatusCode(424, result.MessageResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"SmsTwilioController: SendSmsTwilioAccountBalance() ended. [HttpGet] - 500InternalServerError - Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetPriceTwilio")]
        public async Task<IActionResult> GetSmsTwilioCost()
        {
            _logger.LogInformation("SmsTwilioController: GetSmsTwilioCost() Begin");
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError(JsonConvert.SerializeObject(ModelState));
                    return BadRequest(ModelState);
                }

                var result = await _message.GetSmsTwilioCostAsync();

                if (result.isSucess)
                {
                    _logger.LogInformation(result.MessageResult);
                    _logger.LogInformation("SmsTwilioController: GetSmsTwilioCost() end");
                    return Ok(result.MessageResult);
                }
                return StatusCode(424, result.MessageResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"SmsTwilioController: GetSmsTwilioCost() ended. [HttpGet] - 500InternalServerError - Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetDataTwilio")]
        public async Task<IActionResult> GetSmsTwilioData()
        {
            _logger.LogInformation("SmsTwilioController: GetSmsTwilioData() Begin");
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError(JsonConvert.SerializeObject(ModelState));
                    return BadRequest(ModelState);
                }

                var result = await _message.GetSmsTwilioDataAsync();

                if (result!=null)
                {
                    _logger.LogDebug(result.ToString());
                    _logger.LogInformation("SmsTwilioController: GetSmsTwilioData() end");
                    return Ok(result);
                }
                return StatusCode(424, result.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError($"SmsTwilioController: GetSmsTwilioData() ended. [HttpGet] - 500InternalServerError - Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetTotalCostTwilio")]
        public async Task<IActionResult> GetSmsTwilioTotalCostData()
        {
            _logger.LogInformation("SmsTwilioController: GetSmsTwilioTotalCostData() Begin");
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError(JsonConvert.SerializeObject(ModelState));
                    return BadRequest(ModelState);
                }

                var result = await _message.GetSmsTwilioTotalPriceAsync();

                if (result != null)
                {
                    _logger.LogDebug(result.ToString());
                    _logger.LogInformation("SmsTwilioController: GetSmsTwilioTotalCostData() end");
                    return Ok(result);
                }
                return StatusCode(424, result.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError($"SmsTwilioController: GetSmsTwilioTotalCostData() ended. [HttpGet] - 500InternalServerError - Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
  

        [HttpGet("callback")]
        public IActionResult CallbackTwilio()
        {
            var response = HttpContext.Request.Query;
            return Ok();
        }
    }
}
