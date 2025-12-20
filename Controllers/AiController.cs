using Microsoft.AspNetCore.Mvc;
using Mscc.GenerativeAI;
using Mscc.GenerativeAI.Types;
using proje.Models;
using System.Text;
using System.Text.Json;

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
                var googleAi = new GoogleAI(apiKey);
                var textModel = googleAi.GenerativeModel(Model.Gemini25Flash);

                // Prompt'a "Strictly only inner HTML tags" talimatı eklendi
                string prompt = $@"You are a professional fitness trainer. 
        User Data: {model.Age} years old, {model.Height}cm tall, {model.Weight}kg, {model.Gender}.
        {(model.Photo != null ? "Analyze the uploaded photo and provide:" : "Based on the info, provide:")}
        1- Daily macronutrient requirements (Protein, Carbs, Fats).
        2- A concise weekly workout program.
        Please provide the response in clean HTML format. Use ONLY tags like <h4>, <p>, <ul>, <li>. 
        Do not include <html>, <body>, <head> or markdown code blocks.";

                var textRequest = new GenerateContentRequest(prompt);
                // ... (Fotoğraf işleme kısmı aynı kalsın)

                var textResponse = await textModel.GenerateContent(textRequest);

                // TEMİZLEME İŞLEMİ VE DOĞRU AKTARIM
                string cleanedResponse = textResponse.Text;
                cleanedResponse = cleanedResponse.Replace("```html", "").Replace("```", "").Trim();

                // BURASI DEĞİŞTİ: textResponse.Text yerine cleanedResponse gönderilmeli
                ViewBag.AiResponse = cleanedResponse;

                return View("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Bir hata oluştu: " + ex.Message;
                return View("Index");
            }
        }
    }
}