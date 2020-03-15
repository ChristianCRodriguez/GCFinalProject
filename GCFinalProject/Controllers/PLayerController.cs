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
            Global.CurrentPlayerScore = 0;
            return View();
        }

        public IActionResult Status()
        {
            MathGameDBContext db = new MathGameDBContext();
            var playerList = db.Player.ToList();
            var userList = db.AspNetUsers.ToList();
            var UserID = HttpContext.Session.GetInt32("current");

            foreach (Player u in db.Player)

            {
                if (u.PlayerId == UserID)
                    ViewBag.PlayerScore = u.PlayerScore;
                  
            }

            return View("Player");
        }
    }
}