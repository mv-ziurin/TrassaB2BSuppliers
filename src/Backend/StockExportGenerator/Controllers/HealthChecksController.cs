using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StockExportGenerator.Controllers
{
    [Route("api/[controller]")]
    public class HealthChecksController : Controller
    {
        [HttpGet]
        public IActionResult Ping()
        {
            return Ok(new { Message = "Pong" });
        }

    }
}
