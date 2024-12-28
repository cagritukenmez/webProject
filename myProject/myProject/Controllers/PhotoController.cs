using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace myProject.Controllers
{
    [ApiController] // Bu satır API controller'ı işaret eder, ancak MVC controller'ı için gereksizdir
    [Route("api/[controller]")]
    public class PhotoController : Controller // ControllerBase yerine Controller sınıfı kullanılmalı
    {
        public class PhotoResponse
        {
            public string OutputImage1 { get; set; }
            public string OutputImage2 { get; set; }
        }

        [HttpPost("UploadPhoto")]
        public async Task<IActionResult> UploadPhoto(IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
            {
                return BadRequest("Lütfen bir fotoğraf yükleyin.");
            }

            using var memoryStream = new MemoryStream();
            await photo.CopyToAsync(memoryStream);
            var byteArray = memoryStream.ToArray();

            using var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://hairstyle-changer-pro.p.rapidapi.com/facebody/editing/hairstyle-pro"),
                Headers =
                {
                    { "x-rapidapi-key", "41e6f10b85msh464209aa709edc0p1719b5jsn5c006fed8967" },
                    { "x-rapidapi-host", "hairstyle-changer-pro.p.rapidapi.com" },
                },
                Content = new ByteArrayContent(byteArray)
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            using var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("API isteği sırasında bir hata oluştu.");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var photoResponse = JsonConvert.DeserializeObject<PhotoResponse>(jsonResponse);

            if (photoResponse == null || string.IsNullOrEmpty(photoResponse.OutputImage1) || string.IsNullOrEmpty(photoResponse.OutputImage2))
            {
                return BadRequest("API'den geçerli bir fotoğraf yanıtı alınamadı.");
            }

            // Fotoğrafları Result view'ine gönder
            return View("Result", photoResponse); // Burada View() kullanılabilir
        }
    }
}
