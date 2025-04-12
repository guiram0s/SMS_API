using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmsAPI.BL;
using SmsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmsAPI.Controllers
{
    [ApiController]
    [Route("api/sms/[controller]")]

    public class SmsBulkController : Controller
    {
        private IMessages _message;
        private readonly ILogger<SmsBulkController> _logger;
        public SmsBulkController(IMessages message, ILogger<SmsBulkController> logger)
        {
            _logger = logger;
            _message = message;
        }
        [HttpGet]
        public async Task<IActionResult> GetSmsBulkAccountBalance()
        {
            _logger.LogInformation("SmsBulkController: SendSmsBulkAccountBalance Begin");
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError(JsonConvert.SerializeObject(ModelState));
                    return BadRequest(ModelState);
                }

                var result = await _message.GetSmsBulkAccountBalanceAsync();

                if (result.isSucess)
                {
                    _logger.LogInformation(result.MessageResult);
                    _logger.LogInformation("SmsBulkController: SendSmsBulkAccountBalance end");
                    return Ok(result.MessageResult);
                }

                return StatusCode(StatusCodes.Status424FailedDependency, result.MessageResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"SmsBulkController: SendSmsBulkAccountBalance() ended. [HttpGet] - 500InternalServerError - Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
           

        }

        [HttpPost]
        public async Task<IActionResult> SendSmsBulk(SmsServiceMessageModel smsMessage)
        {
            _logger.LogInformation("SmsBulkController: SendSmsBulk Begin");
            try
            {
                _logger.LogInformation(JsonConvert.SerializeObject(smsMessage));
                if (!ModelState.IsValid)
                {
                    _logger.LogError(JsonConvert.SerializeObject(ModelState));
                    return BadRequest(ModelState);
                }

                var result = await _message.SendSmsBulkAsync(smsMessage);

                if (result.isSucess)
                {
                    _logger.LogInformation(result.MessageResult);
                    _logger.LogInformation("SmsBulkController: SendSmsBulk end");
                    return Ok(result.MessageResult);
                }

                return StatusCode(StatusCodes.Status424FailedDependency, result.MessageResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"SmsBulkController: SendSmsBulk() ended. [HttpPost] - 500InternalServerError - Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetDataBulk")]
        public async Task<IActionResult> GetSmsBulkData()
        {
            _logger.LogInformation("SmsBulkController: GetSmsBulkData() Begin");
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError(JsonConvert.SerializeObject(ModelState));
                    return BadRequest(ModelState);
                }

                var result = await _message.GetSmsBulkDataAsync();

                if (result != null)
                {
                    _logger.LogDebug(result.ToString());
                    _logger.LogInformation("SmsBulkController: GetSmsBulkData() end");
                    return Ok(result);
                }
                return StatusCode(424, result.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError($"SmsBulkController: GetSmsBulkData() ended. [HttpGet] - 500InternalServerError - Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetTotalCostBulk")]
        public async Task<IActionResult> GetSmsBulkTotalCostData()
        {
            _logger.LogInformation("SmsBulkController: GetSmsBulkTotalCostData() Begin");
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError(JsonConvert.SerializeObject(ModelState));
                    return BadRequest(ModelState);
                }

                var result = await _message.GetSmsBulkTotalPriceAsync();

                if (result != null)
                {
                    _logger.LogDebug(result.ToString());
                    _logger.LogInformation("SmsBulkController: GetSmsBulkTotalCostData() end");
                    return Ok(result);
                }
                return StatusCode(424, result.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError($"SmsBulkController: GetSmsBulkTotalCostData() ended. [HttpGet] - 500InternalServerError - Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
