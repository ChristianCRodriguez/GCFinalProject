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
    public class GamePlayController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private MathGameDBContext db = new MathGameDBContext();
        public GamePlayController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        public IActionResult Index()
        {
            //call api
            //keep track of correct answer IF ELSE
            //if passing give them food/poison based on percentage
            //auto feed after each quiz--based on diff mult total possible by percent right
            //display avatar face on bottom-call database
            //display quiz on top
            //layer background
            //ex give controls to mouse in jquery
            //select difficult to image
            //timer on quiz based on difficulty and questions
            //extra music
            //fancy highligt when at 100  

            return View();

        }

        [HttpPost]
        public string ValidateQuizAnswer(int playerAnswer, int correctAnswer)
        {
            if (playerAnswer == correctAnswer)
            {
                Global.CurrentPlayerScore++;
                return "Correct";
            }
            else
            {
                return "False";
            }
        }

        public IActionResult QuizComplete()
        {
            int score = Global.CurrentPlayerScore;
            //add score to player
            //add time to avatar
            ResetQuiz();
            return View("~/Views/GamePlay/QuizComplete.cshtml", score);
        }

        public async Task<IActionResult> QuizQuestion(string difficulty)
        {
#warning Need to figure out logic for question from view
            Global.CurrentPlayerLevel = difficulty;
            difficulty = difficulty == "" ? Global.CurrentPlayerLevel : difficulty;
            MathApi test = new MathApi();
            Question bare = await test.GetQuestion(MathCategories.SimpleArithmetic, _config["MathApiKey"], difficulty);
            return View("~/Views/GamePlay/Quiz.cshtml",bare);
        }

        public void ResetQuiz()
        {
            Global.CurrentPlayerLevel = "Get Default Player Difficutly";
            Global.CurrentPlayerScore = 0;
        }
    }
}