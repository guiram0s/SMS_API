using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmsAPI.Models
{
    public class SmsServiceMessageModel
    {
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only Numbers allowed.")]
        [MaxLength(12)]
        public string tel { get; set; }
        [Required(ErrorMessage = "Message Cannot be empty.")]
        [MaxLength(72)]
        public string msg { get; set; }
    }
}
