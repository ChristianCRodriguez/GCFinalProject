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

        //Used to get the current user logged in
        public string GetPlayer()
        {
            var currentPlayerID = db.AspNetUsers.SingleOrDefault(u => u.UserName == User.Identity.Name).Id;
            return currentPlayerID;
        }

        public IActionResult Index()
        {
            return View();
        }

        //CreatePlayer takes the email used when registering to find the new user created in AspNetUsers
        //To then create a new entry in our Player Table and to link them via AspNetUsers.ID and Player.PlayerID
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

        //Gathers data necessary to be displayed on the PlayerStatus Page/View
        public IActionResult Player()
        {
            var user = GetPlayer();
            Global.QuizDifficulty = null;
            Global.QuizScore = 0;
            Global.QuizCategory = null;

            PlayerStatus ps = new PlayerStatus();
            ps.Leaderboard = db.Player.OrderByDescending(u => u.PlayerScore).Take(10).ToList();
            ps.CurrentAvatar = db.Avatar.SingleOrDefault(u => u.PlayerId == user && u.IsActive == true);
            ps.CurrentPlayer = db.Player.SingleOrDefault(u => u.PlayerId == user);
            return View(ps);
        }

        //In the event someone has already had monster in the past, this method will send them to the correct view.
        public IActionResult IsFirstTimeLogginIn()
        {
            var currentUser = GetPlayer();
            //Checks to see if current user currently has or has had an avatar in the past
            if (db.Avatar.SingleOrDefault(u => u.PlayerId == currentUser) == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Player");
            }
        }

        //Generates your first monster
        public IActionResult FirstTime(string monsterName)
        {
            var name = monsterName.Trim();
            var playerID = db.AspNetUsers.SingleOrDefault(u => u.UserName == User.Identity.Name).Id;

            if (db.Avatar.SingleOrDefault(u => u.PlayerId == playerID) == null)
            {
                db.Player.SingleOrDefault(u => u.PlayerId == playerID).IsFirstTimeLoggingIn = false;

                DateTime date = DateTime.Now;
                Avatar newAvatar = new Avatar();

                newAvatar.AvatarEnergy = 960;
                newAvatar.AvatarName = name;
                newAvatar.IsActive = true;
                newAvatar.PlayerId = playerID;
                newAvatar.ExpireDate = new DateTime(date.Year, date.Month, date.AddDays(1).Day, date.Hour, date.Minute, date.Second);
                newAvatar.LastFeedDate = date;
                newAvatar.MoodId = 4;
                db.Avatar.Add(newAvatar);
                db.SaveChanges();
            }

            return RedirectToAction("Player");
        }
    }
}