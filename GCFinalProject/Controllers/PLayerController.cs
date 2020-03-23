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
        private MathGameDBContext db = new MathGameDBContext();

        public PlayerController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreatePlayer(string email)
        {
            var registeredUser = db.AspNetUsers.SingleOrDefault(u => u.Email == email);
            string newAccount = "NewAccount";
            DateTime today = DateTime.Now;
            Player player = new Player();
            player.PlayerId = registeredUser.Id;
            player.PlayerUserName = registeredUser.UserName;
            player.PlayerScore = 0;
            player.LastUpdatedDay = today;
            player.CreatedDay = today;
            player.IsFirstTimeLoggingIn = true;
            player.DifficultyLevel = "Beginner";

            db.Player.Add(player);

            db.SaveChanges();
            return View("~/Views/Home/Index.cshtml", newAccount);
        }

        public IActionResult Player()
        {
            Global.QuizDifficulty = null;
            Global.QuizScore = 0;
            Global.QuizCategory = null;

            PlayerStatus ps = new PlayerStatus();

            ps.Leaderboard = db.Player.OrderByDescending(u => u.PlayerScore).Take(10).ToList();


            //var playerList = db.Player.ToList();
            //var userList = db.AspNetUsers.ToList();


            //double maxScore = 0;
            //foreach (Player u in db.Player)
            //{
            //    if (u.PlayerScore > maxScore)
            //    {
            //        maxScore = u.PlayerScore;
            //        ViewBag.LeaderScore = maxScore;
            //        //ViewBag.Name = UsrList.Where(i => i.Id == u.PlayerId);

            //         var name =
            //         from AspNetUsers in userList 
            //         where u.PlayerId == AspNetUsers.Id
            //         select AspNetUsers.NormalizedUserName;
            //         ViewBag.Name = "Rana";
            //    }
            // }
            return View(ps);
        }

        public IActionResult IsFirstTimeLogginIn()
        {
            return View();
        }

        public IActionResult FirstTime()
        {
            var playerID = db.AspNetUsers.SingleOrDefault(u => u.UserName == User.Identity.Name).Id;
            db.Player.SingleOrDefault(u => u.PlayerId == playerID).IsFirstTimeLoggingIn = false;
            db.SaveChanges();
            return RedirectToAction("Player");
        }
    }
}