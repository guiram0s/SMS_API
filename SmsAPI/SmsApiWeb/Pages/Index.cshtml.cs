using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SmsApiWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http;

namespace SmsApiWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public MessageInfo MessageInfo { get; set; }
        public string Message { get; private set; }

        public void OnGet()
        {

        }
        
        public async Task<RedirectToPageResult> OnPost()
        {
            var message = MessageInfo.msg;
            var number = MessageInfo.tel;
            var service = Int32.Parse(Request.Form["service"]);
            var content = "0";
            switch (service)
            {
                case 0:
                    {
                        try
                        {

                            var request = await "https://localhost:5001/api/sms/SmsTwilio".WithHeader("Content-Type", "application/json").PostJsonAsync(MessageInfo);
                            content = request.ResponseMessage.ToString();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            Message = "Error " + ex.Message;
                            return new RedirectToPageResult("Error", "Message");

                        }
                        break;
                    }
                case 1:
                    {
                        try
                        {
                            var request = await "https://localhost:5001/api/sms/SmsApi".WithHeader("Content-Type", "application/json").PostJsonAsync(MessageInfo);
                            content = request.ResponseMessage.ToString();

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            Message = "Error " + ex.Message;
                            return new RedirectToPageResult("Error", "Message");
                        }
                       
                        break;
                    }
                case 2:
                    {
                        try
                        {
                            if (number.Length > 9)
                            {
                                Message = "Inserir numero com 9 caracteres";
                                return new RedirectToPageResult("Error","Message");
                            }
                            var request = await "https://localhost:5001/api/sms/SmsBulk".WithHeader("Content-Type", "application/json").PostJsonAsync(MessageInfo);
                            content = request.ResponseMessage.ToString();


                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            Message = "Error " + ex.Message;
                            return new RedirectToPageResult("Error", "Message");
                        }
         
                        break;
                    }
            }
            Message ="Your message has been sent";
            return new RedirectToPageResult("Confirmation","Message"); 
        }


    }
}
