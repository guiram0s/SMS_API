using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsAPI.Models
{
    public class SmsServiceMessageResultModel
    {
        public bool isSucess { get; set; }
        public string MessageResult { get; set; }
        public string Price { get; set; }
        public string MsgId { get; set; }
    }
}
