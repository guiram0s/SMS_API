using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Twilio.TwiML;
using Twilio.AspNet.Mvc;

namespace SendandReceive.Controllers
{
    public class SmsController : TwilioController
    {
        // GET: Sms
        public ActionResult SendSms()
        {
            var accountSid = ConfigurationManager.AppSettings["TwillioAccontSid"];
            var authToken = ConfigurationManager.AppSettings["TwillioAuthToken"];
            TwilioClient.Init(accountSid, authToken);

            var to = new PhoneNumber(ConfigurationManager.AppSettings["MyPhoneNumber"]);
            var from = new PhoneNumber("+351914258363");

            var message = MessageResource.Create(
                to: to,
                from: from,
                body: "Message");
            return Content(message.Sid);
        }

        public ActionResult ReceiveSms()
        {
            var response = new MessagingResponse();
            response.Message("Received");
            return TwiML(response);
        }
    }
}