using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GCFinalProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GCFinalProject.Controllers
{
    public class PlayerController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public PlayerController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Player()
        {
            var UserID = HttpContext.Session.GetInt32("test");
            return View();
        }
    }
}