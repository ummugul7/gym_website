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
            if (model.Photo == null || model.Photo.Length == 0) return BadRequest("Fotoğraf gerekli.");

            // 1. Fotoğrafı Base64 formatına çevir
            using var ms = new MemoryStream();
            await model.Photo.CopyToAsync(ms);
            var base64Image = Convert.ToBase64String(ms.ToArray());

            // 2. Gemini Yapılandırması
            var googleAi = new GoogleAI(apiKey);
            var visionModel = googleAi.GenerativeModel(Model.Gemini25Flash);

            // 3. Prompt (AI'ya talimat)
            string prompt = $@"Bir profesyonel spor eğitmenisin. 
            Kullanıcı: {model.Age} yaşında, {model.Height}cm boyunda, {model.Weight}kg ağırlığında ve {model.Gender}. 
            Gönderilen fotoğrafı analiz et, vücut yağ oranını tahmin et ve kişiye özel:
            1- Günlük alması gereken makro besinler.
            2- Haftalık antrenman programı.
            Lütfen yanıtı düzgün bir HTML formatında ver.";

            // 4. AI'ya gönder
            var request = new GenerateContentRequest(prompt);
            request.Contents[0].Parts.Add(new InlineData { MimeType = "image/jpeg", Data = base64Image });

            var response = await visionModel.GenerateContent(request);

            // Sonucu View'a gönder
            ViewBag.AiResponse = response.Text;
            return View("Index");
        }
    }
}