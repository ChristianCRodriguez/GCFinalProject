using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
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
        private JsonDocument jdoc;
        private Dictionary<int?, string> mathCategories = new Dictionary<int?, string>()
        {
            { 0, "/api/v1/arithmetic/simple.json"},
            { 1, "/api/v1/arithmetic/fractions.json"},
            { 2, "/api/v1/arithmetic/exponents-and-radicals.json"},
            { 3, "/api/v1/arithmetic/simple-trigonometry.json"},
            { 4, "/api/v1/arithmetic/matrices.json"},
            { 5, "/api/v1/algebra/linear-equations.json"},
            { 6, "/api/v1/algebra/equations-containing-radicals.json"},
            { 7, "/api/v1/algebra/equations-containing-absolute-values.json"},
            { 8, "/api/v1/algebra/quadratic-equations.json"},
            { 9, "/api/v1/algebra/higherorder-polynomial-equations.json"},
            { 10, "/api/v1/algebra/equations-involving-fractions.json"},
            { 11, "/api/v1/algebra/exponential-equations.json"},
            { 12, "/api/v1/algebra/logarithmic-equations.json"},
            { 13, "/api/v1/algebra/trigonometric-equations.json"},
            { 14, "/api/v1/algebra/matrices-equations.json"},
            { 15, "/api/v1/calculus/polynomial-differentiation.json"},
            { 16, "/api/v1/calculus/trigonometric-differentiation.json"},
            { 17, "/api/v1/calculus/exponents-differentiation.json"},
            { 18, "/api/v1/calculus/polynomial-integration.json"},
            { 19, "/api/v1/calculus/trigonometric-integration.json"},
            { 20, "/api/v1/calculus/exponents-integration.json"},
            { 21, "/api/v1/calculus/polynomial-definite-integrals.json"},
            { 22, "/api/v1/calculus/trigonometric-definite-integrals.json"},
            { 23, "/api/v1/calculus/exponents-definite-integrals.json"},
            { 24, "/api/v1/calculus/first-order-differential-equations.json"},
            { 25, "/api/v1/calculus/second-order-differential-equations.json"}
        };

        public string GetPlayer()
        {
            var currentPlayerID = db.AspNetUsers.SingleOrDefault(u => u.UserName == User.Identity.Name).Id;
            return currentPlayerID;
        }

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
                Global.QuizScore++;
                return "Correct";
            }
            else
            {
                return "False";
            }
        }

        public IActionResult QuizComplete()
        {
            string playerID = GetPlayer();
            double initialScore = Global.QuizScore;
            double newScore = 0;
            if(Global.QuizDifficulty == "advanced")
            {
                newScore = initialScore * 2;
            }
            else if(Global.QuizDifficulty == "intermediate")
            {
                newScore = initialScore * 1.5;
            }
            else
            {
                newScore = initialScore;
            }

            db.Player.SingleOrDefault(u => u.PlayerId == playerID).PlayerScore += newScore;
            db.SaveChanges();

            ResetQuiz();
            return View("~/Views/GamePlay/QuizComplete.cshtml", newScore);
        }

        public async Task<IActionResult> QuizQuestion(string difficulty, int? category = null)
        {
            Global.QuizDifficulty = Global.QuizDifficulty == null ? difficulty : Global.QuizDifficulty;
            Global.QuizCategory = Global.QuizCategory == null ? category : Global.QuizCategory;

            difficulty = difficulty == null ? Global.QuizDifficulty : difficulty;
            category = category == null ? Global.QuizCategory : category;
            
            Question question = new Question();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", _config["MathApiKey"]);
                using (var response = await httpClient.GetAsync($"https://studycounts.com{mathCategories[category]}?difficulty={difficulty}"))
                {
                    string questionResponse = await response.Content.ReadAsStringAsync();
                    //jdoc = JsonDocument.Parse(questionResponse);
                    question = JsonSerializer.Deserialize<Question>(questionResponse);
                }
            }

            return View("~/Views/GamePlay/Quiz.cshtml",question);
        }

        public void ResetQuiz()
        {
            Global.QuizDifficulty = null;
            Global.QuizScore = 0;
            Global.QuizCategory = null;
        }
    }
}