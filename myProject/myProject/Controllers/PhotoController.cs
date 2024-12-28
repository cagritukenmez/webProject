using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace myProject.Controllers
{
    public class PhotoController : Controller
    {
        private const string API_KEY = "693a981769msh95695d401172ee2p188bb6jsn2e321249a267";
        private const string API_HOST = "hairstyle-changer-pro.p.rapidapi.com";

        public class ApiResponse
        {
            public int error_code { get; set; }
            public string task_id { get; set; }
            public string task_type { get; set; }
        }

        public class ResultResponse
        {
            public int error_code { get; set; }
            public string output_image { get; set; }
            public string output_url { get; set; }
        }

        // Index sayfasını göster

        // Fotoğraf yükleme işlemi
        [HttpPost]
        public async Task<IActionResult> UploadPhoto(IFormFile photo)
        {
            try
            {
                if (photo == null || photo.Length == 0)
                {
                    TempData["Error"] = "Lütfen bir fotoğraf yükleyin.";
                    return RedirectToAction("Index","BerberSalonu");
                }

                using var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://hairstyle-changer-pro.p.rapidapi.com/facebody/editing/hairstyle-pro"),
                    Headers =
                    {
                        { "x-rapidapi-key", API_KEY },
                        { "x-rapidapi-host", API_HOST },
                    }
                };

                var multipartContent = new MultipartFormDataContent();

                // Task Type
                var taskTypeContent = new StringContent("async");
                taskTypeContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "task_type"
                };
                multipartContent.Add(taskTypeContent);

                // Image
                using var memoryStream = new MemoryStream();
                await photo.CopyToAsync(memoryStream);
                var imageContent = new ByteArrayContent(memoryStream.ToArray());
                imageContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "image",
                    FileName = photo.FileName
                };
                multipartContent.Add(imageContent);

                // Hair Style
                var hairStyleContent = new StringContent("BuzzCut");
                hairStyleContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "hair_style"
                };
                multipartContent.Add(hairStyleContent);

                // Color
                var colorContent = new StringContent("platinumBlonde");
                colorContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "color"
                };
                multipartContent.Add(colorContent);

                // Image Size
                var sizeContent = new StringContent("2");
                sizeContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "image_size"
                };
                multipartContent.Add(sizeContent);

                request.Content = multipartContent;

                using var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] = $"API isteği başarısız: {responseContent}";
                    return RedirectToAction("Index", "BerberSalonu");
                }

                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);

                if (apiResponse?.task_id != null)
                {
                    // İşlemin tamamlanması için bekle
                    await Task.Delay(5000);

                    // Sonuçları al
                    var resultRequest = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri($"https://hairstyle-changer-pro.p.rapidapi.com/facebody/editing/hairstyle-pro?task_id={apiResponse.task_id}"),
                        Headers =
                        {
                            { "x-rapidapi-key", API_KEY },
                            { "x-rapidapi-host", API_HOST },
                        }
                    };

                    using var resultResponse = await client.SendAsync(resultRequest);
                    var resultContent = await resultResponse.Content.ReadAsStringAsync();

                    if (!resultResponse.IsSuccessStatusCode)
                    {
                        TempData["Error"] = $"Sonuç alma başarısız: {resultContent}";
                        return RedirectToAction("Index", "BerberSalonu");
                    }

                    var result = JsonConvert.DeserializeObject<ResultResponse>(resultContent);
                    return View("Result", result);
                }

                TempData["Error"] = "Task ID alınamadı.";
                return RedirectToAction("Index", "BerberSalonu");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Bir hata oluştu: {ex.Message}";
                return RedirectToAction("Index", "BerberSalonu");
            }
        }
    }
}