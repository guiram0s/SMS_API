using Microsoft.AspNetCore.Mvc;
using SmsAPI.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmsAPI.Controllers
{
    [ApiController]
    [Route("api/testdatabase")]
    public class DummyController :ControllerBase
    {
        private readonly SmsContext _stx;
        public DummyController(SmsContext stx)
        {
            _stx = stx ?? throw new ArgumentNullException(nameof(stx));
        }
        [HttpGet]
        public IActionResult TestDatabse()
        {
            return Ok();
        }
    }
}
