using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmsApiWeb.Models;

namespace SmsApiWeb.Pages
{
    public class MessagesModel : PageModel
    {
        public string Message { get; set; }
        public List<SmsLog> smsLogList = new List<SmsLog>();

        public async void OnPost()
        {
            var service = Int32.Parse(Request.Form["serviceMessages"]);
            var content = "0";
            var content2 = "0";
            var content3 = "0";
            var content4 = "0";
            var content5 = "0";
            var content6 = "0";

            switch (service)
            {
                case 0:
                    {
                        try
                        {
                            HttpClient client = new HttpClient();
                            client.BaseAddress = new Uri("https://localhost:5001/api/sms/");
                            HttpResponseMessage response = client.GetAsync("SmsTwilio/GetDataTwilio").Result;
                            content = await response.Content.ReadAsStringAsync();
                            var twilioList = JsonConvert.DeserializeObject<List<SmsLog>>(content);
                            //------------------------------------------------------
                            HttpClient client2 = new HttpClient();
                            client2.BaseAddress = new Uri("https://localhost:5001/api/sms/");
                            HttpResponseMessage response2 = client2.GetAsync("SmsApi/GetDataApi").Result;
                            content2 = await response2.Content.ReadAsStringAsync();
                            var SmsApiList = JsonConvert.DeserializeObject<List<SmsLog>>(content2);
                            //--------------------------------------------------------------
                            HttpClient client3 = new HttpClient();
                            client3.BaseAddress = new Uri("https://localhost:5001/api/sms/");
                            HttpResponseMessage response3 = client3.GetAsync("SmsBulk/GetDataBulk").Result;
                            content3 = await response3.Content.ReadAsStringAsync();
                            var SmsBulkList = JsonConvert.DeserializeObject<List<SmsLog>>(content3);

                            var finalList = new List<SmsLog>();

                            finalList.AddRange(twilioList);
                            finalList.AddRange(SmsApiList);
                            finalList.AddRange(SmsBulkList);

                            smsLogList = finalList;


                            HttpClient client4 = new HttpClient();
                            client4.BaseAddress = new Uri("https://localhost:5001/api/sms/");
                            HttpResponseMessage response4 = client4.GetAsync("SmsTwilio/GetTotalCostTwilio").Result;
                            content4 = await response4.Content.ReadAsStringAsync();
                            var num = Convert.ToDouble(content4);

                            HttpClient client5 = new HttpClient();
                            client5.BaseAddress = new Uri("https://localhost:5001/api/sms/");
                            HttpResponseMessage response5 = client5.GetAsync("SmsApi/GetTotalCostApi").Result;
                            content5 = await response5.Content.ReadAsStringAsync();
                            var num2 = Convert.ToDouble(content5);

                            HttpClient client6 = new HttpClient();
                            client6.BaseAddress = new Uri("https://localhost:5001/api/sms/");
                            HttpResponseMessage response6 = client6.GetAsync("SmsBulk/GetTotalCostBulk").Result;
                            content6 = await response6.Content.ReadAsStringAsync();
                            var num3 = Convert.ToDouble(content6);
                            var final = num+num2+num3;
                            final = Convert.ToDouble(final);
                            var result = String.Format("{0:0.00}", final);
                            Message = "Preço total: " + result + "€";
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            Message = "Error " + ex.Message;
                        }

                        break;
                    }
                case 1:
                    {
                        try
                        {
                            HttpClient client = new HttpClient();

                            client.BaseAddress = new Uri("https://localhost:5001/api/sms/");

                            HttpResponseMessage response = client.GetAsync("SmsTwilio/GetDataTwilio").Result;

                            content = await response.Content.ReadAsStringAsync();

                            smsLogList = JsonConvert.DeserializeObject<List<SmsLog>>(content);

                            HttpClient client2 = new HttpClient();
                            client2.BaseAddress = new Uri("https://localhost:5001/api/sms/");
                            HttpResponseMessage response2 = client.GetAsync("SmsTwilio/GetTotalCostTwilio").Result;
                            content2 = await response2.Content.ReadAsStringAsync();
                            var num = Convert.ToDouble(content2);
                            content2 = String.Format("{0:0.00}", num);
                            Message = "Preço total(Twilio): "+content2+"€";
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

                            HttpResponseMessage response = client.GetAsync("SmsApi/GetDataApi").Result;

                            content = await response.Content.ReadAsStringAsync();

                            smsLogList = JsonConvert.DeserializeObject<List<SmsLog>>(content);

                            HttpClient client2 = new HttpClient();
                            client2.BaseAddress = new Uri("https://localhost:5001/api/sms/");
                            HttpResponseMessage response2 = client.GetAsync("SmsApi/GetTotalCostApi").Result;
                            content2 = await response2.Content.ReadAsStringAsync();
                            var num = Convert.ToDouble(content2);
                            content2 = String.Format("{0:0.00}", num);
                            Message = "Preço total(SmsApi): " + content2 + "€";
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            Message = "Error " + ex.Message;
                        }

                        break;
                    }
                case 3:
                    {
                        try
                        {
                            HttpClient client = new HttpClient();

                            client.BaseAddress = new Uri("https://localhost:5001/api/sms/");

                            HttpResponseMessage response = client.GetAsync("SmsBulk/GetDataBulk").Result;

                            content = await response.Content.ReadAsStringAsync();

                            smsLogList = JsonConvert.DeserializeObject<List<SmsLog>>(content);

                            HttpClient client2 = new HttpClient();
                            client2.BaseAddress = new Uri("https://localhost:5001/api/sms/");
                            HttpResponseMessage response2 = client.GetAsync("SmsBulk/GetTotalCostBulk").Result;
                            content2 = await response2.Content.ReadAsStringAsync();
                            var num = Convert.ToDouble(content2);
                            content2 = String.Format("{0:0.00}", num);
                            Message = "Preço total(SmsBulk): " + content2 + "€";
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
