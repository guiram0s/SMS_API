using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmsAPI.Entitites
{
    public class SmsLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Datetime { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Result { get; set; }
        [Required]
        public string Rem { get; set; }//ir buscar ao appsettings
        [Required]
        public string Provider { get; set; }
        public string? Price { get; set; }
        [Required]
        public string MessageId { get; set; }
    }
}
