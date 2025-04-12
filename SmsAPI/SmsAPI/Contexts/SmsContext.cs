using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmsAPI.Entitites;
using SmsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsAPI.Contexts
{
    public class SmsContext : DbContext
    {
        public DbSet<SmsLog> SmsLogs { get; set; }
        //public SmsBulkMessageModel _sms ;
        public SmsContext(DbContextOptions<SmsContext> options )
            : base(options) 
        {
            //Database.EnsureCreated();
        }

        

    }
}
