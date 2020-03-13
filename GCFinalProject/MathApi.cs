using GCFinalProject.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Configuration;
using System.Threading.Tasks;

namespace GCFinalProject
{
    public class MathApi
    {
        private JsonDocument jdoc;
        private readonly string siteLink = "https://studycounts.com";
        public MathApi()
        {

        }

        public async Task<Question> GetQuestion(string category, string apiKey, string difficulty = "beginner")
        {
            Question question = new Question();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", apiKey);
                using (var response = await httpClient.GetAsync(siteLink + category +"?" + difficulty.ToLower()))
                {
                    string test = await response.Content.ReadAsStringAsync();
                    jdoc = JsonDocument.Parse(test);
                    question = JsonSerializer.Deserialize<Question>(test);

                }
            }
            return question;
        }
    }
}