using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010;
using Twilio.Rest.Api.V2010.Account;
using System.Net;
using Newtonsoft.Json;

namespace TwilioTest
{
    class Program
    {
        static void Main(string[] args)
        {
            double amount, curAmount;
            int exchangeType;
            string url = "http://www.floatrates.com/daily/usd.json";
            string json = new WebClient().DownloadString(url);
            var currency = JsonConvert.DeserializeObject<dynamic>(json);


            Console.Write("Enter Your Amount:");
            amount = Convert.ToSingle(Console.ReadLine());
            Console.Write("\nPress(1) for Dollar->Euro \nPress(2) for Euro->Dollar \nEnter exchange type: ");
            exchangeType = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("*************************");
            if (exchangeType == 1)
            {
                curAmount = amount * Convert.ToSingle(currency.eur.rate);
                Console.WriteLine("{0:N2}{1}", curAmount, currency.eur.code);
            }
            else if (exchangeType == 2)
            {
                curAmount = amount * Convert.ToSingle(currency.eur.inverseRate);
                Console.WriteLine("{0:N2}{1}", curAmount, "USD");
            }

            Console.ReadLine();
        }
    }
}
