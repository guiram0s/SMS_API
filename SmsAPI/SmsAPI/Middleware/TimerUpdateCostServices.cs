using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SmsAPI.Services.Messages;
using SmsAPI.Contexts;
using Flurl;
using System.Net.Http.Headers;
using Flurl.Http;
using Flurl.Http.Configuration;
using Newtonsoft.Json;
using System.IO;

namespace SmsAPI.Middleware
{
    public class TimerUpdateCostServices : IHostedService,IDisposable
    {
        private Timer _timer = null!;
        private readonly System.Net.Http.IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public ILogger<TimerUpdateCostServices> _logger;
        private int executionCount = 0;
        public TimerUpdateCostServices( ILogger<TimerUpdateCostServices> logger, System.Net.Http.IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
            _logger.LogInformation("Middleware: TimerServices Begin");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(int.Parse(_configuration.GetSection("UpdateCostService").GetValue<string>("TimerIntervalsec"))));
            return Task.CompletedTask;
           
        }
        public async void DoWork(object state)
        {
            
            Console.WriteLine("Passei Aqui!");

            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Get;
            var httpClient = _httpClientFactory.CreateClient();

            var urlSend = Path.Combine(_configuration.GetSection("API").GetValue<string>("ApiUrl"), _configuration.GetSection("API").GetValue<string>("ApiTwilioUrl"));
            var httpResponseMessage = await httpClient.GetAsync(urlSend);
            var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
            _logger.LogInformation(responseString);
            httpResponseMessage.EnsureSuccessStatusCode();
            httpRequestMessage.Dispose();
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
