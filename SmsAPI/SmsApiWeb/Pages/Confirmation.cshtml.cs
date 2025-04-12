using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmsApiWeb.Pages
{
    public class ConfirmationModel : PageModel
    {
        public string Message { get; set; }
        public void OnGetMessage()
        {
            
            Message = "Your Message has been sent.";
        }
    }
}
