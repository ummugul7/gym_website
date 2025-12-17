using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using proje.Models;

namespace proje.Controllers
{
    public class AiController : Controller
    {
        private readonly string apiKey = "AIzaSyC2aWR0O_8eX8FyO1X4m4CbWRdY0Ftkm-Y";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetSuggestion(AiViewModel model)
        {
            try
            {
                string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-3-pro-preview:streamGenerateContent?key={apiKey}";

                var requestBody = new
                {
                    contents = new[]
                    {
                        new
                        {
                            role = "user",
                            parts = new[]
                            {
                                new { text = $"Act as a professional fitness coach and nutritionist. " +
                                             $"User Profile: Age {model.Age}, Height {model.Height}cm, Weight {model.Weight}kg, Gender {model.Gender}. " +
                                             $"Based on this data, please create a personalized 7-day workout routine and a customized meal plan. " +
                                             $"Format the response using only clean HTML tags (such as <h3>, <p>, <ul>, <li>). " +
                                             $"Provide the instructions in Turkish language so the user can understand easily." }
                            }
                        }
                    },
                    generationConfig = new
                    {
                        thinkingConfig = new { thinkingLevel = "HIGH" }
                    }
                };

                using var client = new HttpClient();
                var jsonRequest = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    using var doc = JsonDocument.Parse(responseString);
                    // Dönen array içerisindeki metni ayıklıyoruz
                    var text = doc.RootElement[0]
                        .GetProperty("candidates")[0]
                        .GetProperty("content")
                        .GetProperty("parts")[0]
                        .GetProperty("text")
                        .GetString();

                    ViewBag.AiResponse = text;
                }
                else
                {
                    ViewBag.AiResponse = $"Error: {response.StatusCode} - {responseString}";
                }
            }
            catch (Exception ex)
            {
                ViewBag.AiResponse = $"An unexpected error occurred: {ex.Message}";
            }

            return View("Index");
        }
    }
}