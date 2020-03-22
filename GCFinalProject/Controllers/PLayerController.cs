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
            string newAccount = "NewAccount";
            DateTime today = DateTime.Now;
            Player player = new Player();
            player.PlayerId = db.AspNetUsers.SingleOrDefault(u => u.Email == email).Id;
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
            MathGameDBContext db = new MathGameDBContext();
            var playerList = db.Player.ToList();
            var userList = db.AspNetUsers.ToList();


            double maxScore = 0;
            foreach (Player u in db.Player)

            {
                
                if (u.PlayerScore > maxScore)
                {
                    maxScore = u.PlayerScore;
                    ViewBag.LeaderScore = maxScore;
                    //ViewBag.Name = UsrList.Where(i => i.Id == u.PlayerId);

                     var name =
                     from AspNetUsers in userList 
                     where u.PlayerId == AspNetUsers.Id
                     select AspNetUsers.NormalizedUserName;
                     ViewBag.Name = "Rana";
                }

             }
            return View();
        }
        



        //public IActionResult Status()
        //{
        //    MathGameDBContext db = new MathGameDBContext();
        //    var playerList = db.Player.ToList();
        //    var userList = db.AspNetUsers.ToList();
        //    var UserID = HttpContext.Session.GetInt32("current");

        //    foreach (Player u in db.Player)

        //    {
        //        if (u.PlayerId == UserID)
        //            ViewBag.PlayerScore = u.PlayerScore;



        //    }

        //    return View("Player");
        //}
        public IActionResult IsFirstTimeLogginIn()
        {
            return View();
        }

        //public IActionResult GetLeader()
        //{
        //    MathGameDBContext db = new MathGameDBContext();
        //    var playerList = db.Player.ToList();
        //   // var userList = db.AspNetUsers.ToList();
        //   // var UserID = HttpContext.Session.GetInt32("current");
        //    double maxScore = 0;
        //    foreach (Player u in db.Player)

        //    {

        //        if (u.PlayerScore > maxScore)
        //         maxScore = u.PlayerScore;
        //        ViewBag.LeaderScore = maxScore;

        //    }

        //    return View("Player");
        //}

    }
}