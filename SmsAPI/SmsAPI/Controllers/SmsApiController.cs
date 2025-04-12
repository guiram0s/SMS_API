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

namespace SmsAPI.Controllers
{
    [ApiController]
    [Route("api/sms/[controller]")]

    public class SmsApiController : Controller
    {
        private IMessages _message;
        private readonly ILogger<SmsApiController> _logger;
        public SmsApiController(IMessages message, ILogger<SmsApiController> logger)
        {
            _logger = logger;
            _message = message;
        }

        [HttpGet]
        public async Task<IActionResult> GetSmsApiAccountBalance()
        {
            _logger.LogInformation("SmsApiController: SendSmsApiAccountBalance Begin"); 
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError(JsonConvert.SerializeObject(ModelState));
                    return BadRequest(ModelState);
                }

                var result = await _message.GetSmsApiAccountBalanceAsync();

                if (result.isSucess)
                {
                    _logger.LogInformation(result.MessageResult);
                    _logger.LogInformation("SmsApiController: SendSmsApiAccountBalance end");
                    return Ok(result.MessageResult);
                }

                return StatusCode(StatusCodes.Status424FailedDependency, result.MessageResult);
            }
            catch(Exception ex)
            {
                _logger.LogError($"SmsApiController: SendSmsApiAccountBalance() ended. [HttpGet] - 500InternalServerError - Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> SendSmsApi(SmsServiceMessageModel smsMessage)
        {
            try
            {
                _logger.LogInformation("SmsApiController: SendSmsApi Begin");
                _logger.LogInformation(JsonConvert.SerializeObject(smsMessage));
                if (!ModelState.IsValid)
                {
                    _logger.LogError(JsonConvert.SerializeObject(ModelState));
                    return BadRequest(ModelState);
                }

                var result = await _message.SendSmsApiAsync(smsMessage);

                if (result.isSucess)
                {
                    _logger.LogInformation(result.MessageResult);
                    _logger.LogInformation("SmsApiController: SendSmsApi end");
                    return Ok(result.MessageResult);
                }
                return StatusCode(StatusCodes.Status424FailedDependency, result.MessageResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"SmsApiController: SendSmsApi() ended. [HttpPost] - 500InternalServerError - Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetDataApi")]
        public async Task<IActionResult> GetSmsApiData()
        {
            _logger.LogInformation("SmsApiController: GetSmsApiData() Begin");
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError(JsonConvert.SerializeObject(ModelState));
                    return BadRequest(ModelState);
                }

                var result = await _message.GetSmsApiDataAsync();

                if (result != null)
                {
                    _logger.LogDebug(result.ToString());
                    _logger.LogInformation("SmsApiController: GetSmsApiData() end");
                    return Ok(result);
                }
                return StatusCode(424, result.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError($"SmsApiController: GetSmsApiData() ended. [HttpGet] - 500InternalServerError - Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetTotalCostApi")]
        public async Task<IActionResult> GetSmsApiTotalCostData()
        {
            _logger.LogInformation("SmsApiController: GetSmsApiTotalCostData() Begin");
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError(JsonConvert.SerializeObject(ModelState));
                    return BadRequest(ModelState);
                }

                var result = await _message.GetSmsApiTotalPriceAsync();
                // 
                if (result != null)
                {
                    _logger.LogDebug(result.ToString());
                    _logger.LogInformation("SmsApiController: GetSmsApiTotalCostData() end");
                    return Ok(result);
                }
                return StatusCode(424, result.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError($"SmsApiController: GetSmsApiTotalCostData() ended. [HttpGet] - 500InternalServerError - Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
