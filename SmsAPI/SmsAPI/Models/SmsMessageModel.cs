using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsAPI.Models
{
    public class SmsMessageModel
    {
        public string Telefone { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;

    }
}
