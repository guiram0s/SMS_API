using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmsApiWeb.Models
{
    public class MessageInfo
    {
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only Numbers allowed.")]
        public string tel { get; set; } = string.Empty;
        [Required(ErrorMessage = "Message Cannot be empty.")]
        [MaxLength(72)]
        public string msg { get; set; } = string.Empty;

    }
}
