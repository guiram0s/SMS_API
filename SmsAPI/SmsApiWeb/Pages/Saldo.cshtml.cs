using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmsApiWeb.Pages
{
    public class SaldoModel : PageModel
    {
        public string Message { get; set; }
        public async void OnPost()
        {
            var service = Int32.Parse(Request.Form["serviceSaldo"]);
            var content = "0";
            switch (service)
            {
                case 0:
                    {
                        try
                        {

                            HttpClient client = new HttpClient();
                            client.BaseAddress = new Uri("https://localhost:5001/api/sms/");
                            HttpResponseMessage response = client.GetAsync("SmsTwilio").Result;
                            content = await response.Content.ReadAsStringAsync();
                            var subString2 = content.Substring(content.Length - 3);
                            var subString1 = Regex.Replace(content, "[^0-9.]", "") + " ";

                            content = subString1 + subString2;
                            Message = "Twilio: " + content;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            Message = "Error "+ex.Message;
                        }

                        break;
                    }
                case 1:
                    {
                        try
                        {
                            HttpClient client = new HttpClient();
                            client.BaseAddress = new Uri("https://localhost:5001/api/sms/");
                            HttpResponseMessage response = client.GetAsync("SmsApi").Result;
                            content = await response.Content.ReadAsStringAsync();
                            var text2 = content.Substring(content.Length - 32);
                            content = Regex.Replace(text2, "[^0-9.]", "") + " " + "EUR";
                            Message="SmsApi: " + content;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            Message = "Error " + ex.Message;
                        }

                        break;
                    }
                case 2:
                    {
                        try
                        {
                            HttpClient client = new HttpClient();
                            client.BaseAddress = new Uri("https://localhost:5001/api/sms/");
                            HttpResponseMessage response = client.GetAsync("SmsBulk").Result;
                            content = await response.Content.ReadAsStringAsync();
                            var subString1 = content.Substring(3);
                            content = subString1.Substring(0, 7) + " EUR";
                            Message = "SmsBulk: "+content;

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            Message = "Error " + ex.Message;
                        }

                        break;
                    }
            }
        }
    }
}
