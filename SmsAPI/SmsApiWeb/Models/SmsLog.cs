using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsApiWeb.Models
{
    public class SmsLog
    {
       
        public int Id { get; set; }

        public string Datetime { get; set; }
  
        public string Phone { get; set; }
     
        public string Status { get; set; }
      
        public string Result { get; set; }
    
        public string Rem { get; set; }//ir buscar ao appsettings
       
        public string Provider { get; set; }
        public string? Price { get; set; }
       
        public string MessageId { get; set; }
    }

}
