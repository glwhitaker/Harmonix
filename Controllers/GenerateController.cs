using Microsoft.AspNetCore.Mvc;
using Harmonix.DTOs;
using Harmonix.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace Harmonix.Controllers
{
    public class GenerateController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public GenerateController(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> generateMoodPlaylist(string query)
        {
            // Get the OpenAI API key from the environment
            var openAPIKey = _configuration["OpenAI:APIKey"];

            // Set up the HttpClient with the OpenAI API key
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {openAPIKey}");

            // Define request payload
            var payload = new
            {
                model = "gpt-4",
                messages = new object[]
                {
                    new { role = "system", content = $"Given the user's input of {query}, generate a playlist of 25 songs from Spotify that match the provided {query}. The playlist should include a curated selection of tracks by various artists, ensuring a cohesive listening experience for the user. Return a list of songs with the format: title - artist."},
                    new { role = "user", content = query}
                },
                temperature = 0,
                max_tokens = 999
            };
            string jsonPayload = JsonConvert.SerializeObject(payload);
            HttpContent httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            // Send the request
            var responseMessage = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", httpContent);
            var responseMessageJson = responseMessage.Content.ReadAsStringAsync();

            // Return a response
            var response = JsonConvert.DeserializeObject<OpenAIResponse>(await responseMessageJson);
            Playlist _playlist;
            if (response != null)
            {
                _playlist = new Playlist(query, response);
                ViewBag.Playlist = _playlist;
            }

            return View("Index");
        }
    }
}
